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
		public CardDataBase cdb = new CardDataBase();

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

		void AddPack(string name)
		{
			form.Invoke(new Action(() =>
			{
				form.AddPack(name);
				form.label2.Text = "残りパック数:" + form.PackList.Count;
				form.label2.Update();
			}));
		}
		/*
		static async void Delay(int ms)
		{
			await Task.Delay(ms);
		}
		*/
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
			// ページにフレームが含まれる場合にはフレームごとに
			// このメソッドが実行されるため実際のURLを確認する
			if (ev.Url == this.Url)
			{
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
								AddLog("パック :" + m2.Groups[2].Value + "\nURL :" + m2.Groups[1].Value + "\n", LogLevel.情報);
								getPackData(m2.Groups[1].Value, m2.Groups[2].Value, pack_type, m.Groups[1].Value);
//								Delay(10000);
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
									AddLog("パック :" + m5.Groups[2].Value + "\nURL :" + m5.Groups[1].Value + "\n", LogLevel.情報);
									getPackData(m5.Groups[1].Value, m5.Groups[2].Value, pack_type, m.Groups[1].Value);
//									Delay(10000);
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
				/*
				UpdateLabel("セーブ中…");
				form.Invoke(new Action(() =>
				{
					form.CardDB = cdb;
				}));
				CardDataBase.Save(cdb);
				MessageBox.Show("カード情報の取得が終了しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
				*/
			}
		}

		//遊戯王カードwikiのカードリストから、全カードの情報を入手
		public void getAllData()
		{
			try
			{

				form.Invoke(new Action(() =>
				{
					label.Visible = true;
					form.label2.Visible = true;
					form.toolStripMenuItem1.Enabled = false;
					form.データ取得ToolStripMenuItem.Enabled = false;
				}));

				//まずカードリストの名前とURLを得る
				UpdateLabel("遊戯王カードwikiに接続");
				AddLog("遊戯王カードwikiに接続 :" + config.getCardListURL(), LogLevel.全部);

				Navigate(config.getCardListURL());
				
			}
			catch (Exception e)
			{
				MessageBox.Show("エラーで終了:"+ e.Message + "\n"+ e.StackTrace,	"エラー",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
			finally
			{
				/*
				form.Invoke(new Action(() =>
				{
					label.Visible = false;  //最後に非表示
					form.label2.Visible = false;  
					form.toolStripMenuItem1.Enabled = false;
					form.データ取得ToolStripMenuItem.Enabled = false;
				}));
				*/
			}
			return ;
		}

		//パックのページに移動し、カードのリンクを得る
		public void getPackData(string PackURL, string PackName, string PackType, string SeriesName)
		{
			if (form.ProgramEndFlag == true)
			{
				Dispose();
				return;
			}

			AddPack(PackName);
			UpdateLabel(PackName + "に接続");
			AddLog(PackName + "に接続 :" + PackURL, LogLevel.情報);

			CardGet card = new CardGet(PackName,cdb,label,form);
			card.Navigate(PackURL);
		}

	}
}
