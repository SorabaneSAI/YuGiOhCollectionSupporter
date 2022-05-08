using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YuGiOhCollectionSupporter
{
	//遊戯王カードwikiからカードのデータをロードする
	public class DataGet : NonDispBrowser
	{
		public Config config;
		public Label label;
		public List<string> SeriesName = new List<string>();
		public Form1 form;
		public bool ExecutedFlag = false;   //実行済みならもっかいやらない

		public List<PackData> PackDataList = new List<PackData>();

		public DataGet(Form1 f)
		{
			form = f;
			config = f.config;
			label = f.label1;
		}

		void UpdateLabel(string txt)
		{
			label.Invoke(new Action(() =>
			{
				label.Text = txt;
				label.Update();
			}));
		}

		void AddLog(string text, LogLevel LV)
		{
			//他スレッド操作
			form.Invoke(new Action(() =>
			{
				form.AddLog(text,LV);
			}));
		}


		//hnumが3はシリーズ、4はパックの種類
		string getRegexStr(string hnum)
		{
			string MAX_STR = "100"; //適当
			return "<div class=jumpmenu><a href=\"#navigator\">.{1," + MAX_STR + "}?</a></div><h" + hnum + " id=.{1," + MAX_STR + "}?>(?:<A title=.*?\">)?(.{1," + MAX_STR + "}?)<(.{1,5000}?</a>)</li></ul>";
		}

		//読み込み語に処理開始
		protected override void OnDocumentCompleted(
					  WebBrowserDocumentCompletedEventArgs ev)
		{
			if (ExecutedFlag == true) return;
			// ページにフレームが含まれる場合にはフレームごとに
			// このメソッドが実行されるため実際のURLを確認する
			if (ev.Url == this.Url)
			{
				ExecutedFlag = true;
				HtmlDocument doc = Document;

				//<div class="body">を探す
				foreach (HtmlElement e in doc.GetElementsByTagName("div"))
				{
					if (!string.IsNullOrEmpty(e.GetAttribute("id")) && e.GetAttribute("id") == "body")
					{

						//< div class="jumpmenu"><a href = "#navigator" > &uarr;</a></div><h3 id = "content_1_2" > 第10期シリーズ < a class="anchor_super" id="nbb37b14" href="http://yugioh-wiki.net/index.php?%A5%AB%A1%BC%A5%C9%A5%EA%A5%B9%A5%C8#nbb37b14" title="nbb37b14">&dagger;</a></h3>
						//から第10期シリーズを取り出す h3ならシリーズ h4ならパックのリスト
						//なぜかプログラム中では""がとれたり、順番が変わったりする

						//h3のシリーズのあとは、h4のパックの種類が連続することがある
						string 元文章 = e.InnerHtml.Replace("\r\n", "");
						string 正規表現 = getRegexStr("3") + "(.*?)" + "(?=" + getRegexStr("3") + ")";
						MatchCollection mc = Regex.Matches(元文章, 正規表現, RegexOptions.IgnoreCase);

						//m.Groups[0]はValue
						//m.Groups[1]はシリーズ名
						//m.Groups[2]には通常パックが含まれる
						//m.Groups[3]には残りの全パックが含まれる
						foreach (Match m in mc)
						{
							if (m.Groups[1].Value.IndexOf("閲覧に際しての注意事項") != -1 ||
								m.Groups[1].Value.IndexOf("関連リンク") != -1 ||
								m.Groups[1].Value.IndexOf("備考") != -1)
								continue;

							SeriesName.Add(m.Groups[1].Value);
							AddLog("シリーズ :" + m.Groups[1] + "\n", LogLevel.情報);
							string pack_type = "Normal";

							//m.Groups[2]を解体
							MatchCollection mc2 = Regex.Matches(m.Groups[2].Value, "<a title=.*?href=\"(.*?)\".*?>(.*?)</a>", RegexOptions.IgnoreCase);
							foreach (Match m2 in mc2)
							{
								PackData pack = new PackData(m2.Groups[1].Value, m2.Groups[2].Value, pack_type, m.Groups[1].Value);
								AddLog("パック :" + pack.Name + "\nURL :" + pack.URL + "\n", LogLevel.情報);
								PackDataList.Add(pack);

								if (form.ProgramEndFlag == true)
								{
									Dispose();
									return;
								}
							}

							//m.Groups[3]を解体
							MatchCollection mc3 = Regex.Matches(m.Groups[3].Value, getRegexStr("4") + ".*?", RegexOptions.IgnoreCase);
							if (mc3.Count == 0) continue;
							foreach (Match m3 in mc3)
							{
								//パックタイプ名がリンクになってることを考慮
								MatchCollection mc4 = Regex.Matches(m3.Groups[1].Value, "<a title=.*?href=\"(.*?)\".*?>(.*?)", RegexOptions.IgnoreCase);
								if (mc4.Count != 0)
									pack_type = mc4[0].Groups[2].Value;
								else
									pack_type = m3.Groups[1].Value;
								AddLog("パックタイプ :" + pack_type + "\n", LogLevel.情報);
								//mc3[0].Groups[2]を解体
								MatchCollection mc5 = Regex.Matches(m3.Groups[2].Value, "<a title=.*?href=\"(.*?)\".*?>(.*?)</a>", RegexOptions.IgnoreCase);
								foreach (Match m5 in mc5)
								{
									PackData pack = new PackData(m5.Groups[1].Value, m5.Groups[2].Value, pack_type, m.Groups[1].Value);
									AddLog("パック :" + pack.Name + "\nURL :" + pack.URL + "\n", LogLevel.情報);
									PackDataList.Add(pack);

									if (form.ProgramEndFlag == true)
									{
										Dispose();
										return;
									}
								}
							}
						}

					}
				}
				UpdateLabel("全パック取得完了");
				form.Invoke(new Action(() =>
				{
					form.PackDataList = PackDataList;
					form.PackGotFlag = true;
				}));

			}
		}

		//遊戯王公式サイトのカードリストから、全カードの情報を入手
		public void getAllData()
		{
			try
			{

				form.Invoke(new Action(() =>
				{
					label.Visible = true;
					form.label1.Visible = true;
					form.toolStripMenuItem1.Enabled = false;
					form.データ取得ToolStripMenuItem.Enabled = false;
				}));

				//まずカードリストの名前とURLを得る
				UpdateLabel("遊戯王公式サイトに接続");
				AddLog("遊戯王公式サイトに接続 :" + config.getCardListURL(), LogLevel.全部);

				Navigate(config.getCardListURL());
				
			}
			catch (Exception e)
			{
				MessageBox.Show("エラーで終了:"+ e.Message + "\n"+ e.StackTrace,	"エラー",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
			return ;
		}

	}
}
