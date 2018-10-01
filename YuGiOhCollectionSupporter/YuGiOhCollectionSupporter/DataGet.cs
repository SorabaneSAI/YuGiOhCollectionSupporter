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
	public class DataGet
	{
		Config config;
		public NonDispBrowser webbrowser = new NonDispBrowser();
		Label label;
		List<string> SeriesName = new List<string>();
		Form1 form;

		public DataGet(Form1 f)
		{
			form = f;
			config = f.config;
			label = f.label1;
		}

		void UpdateLabel(string txt)
		{
			label.Text = txt;
			label.Update();
		}

		//hnumが3はシリーズ、4はパックの種類
		string getRegexStr(string hnum)
		{
			string MAX_STR = "100";	//適当
			return "<div class=jumpmenu><a href=\"#navigator\">.{1,"+MAX_STR+"}?</a></div><h" + hnum + " id=.{1,"+MAX_STR+ "}?>(?:<A title=.*?\">)?(.{1," + MAX_STR+ "}?)<(.{1,5000}?</a>)</li></ul>";
		}

		//遊戯王カードwikiのカードリストから、全カードの情報を入手
		public void getAllData()
		{
			label.Visible = true;  
			
			//まずカードリストの名前とURLを得る
			UpdateLabel("遊戯王カードwikiに接続");
			form.AddLog("遊戯王カードwikiに接続 :"+ config.getCardListURL(), LogLevel.全部);
			webbrowser.NavigateAndWait(config.getCardListURL());

			HtmlDocument doc = webbrowser.Document;

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
					string 正規表現 = getRegexStr("3")+ "(.*?)"+"(?="+getRegexStr("3")+")";
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
						form.AddLog("シリーズ :" + m.Groups[1] + "\n", LogLevel.情報);
						string pack_type = "Normal";

						//m.Groups[2]を解体
						MatchCollection mc2 = Regex.Matches(m.Groups[2].Value, "<a title=.*?href=\"(.*?)\".*?>(.*?)</a>", RegexOptions.IgnoreCase);
						foreach (Match m2 in mc2)
						{
							form.AddLog("パック :" + m2.Groups[2].Value + "\nURL :" + m2.Groups[1].Value + "\n", LogLevel.情報);
							getPackData(m2.Groups[1].Value, m2.Groups[2].Value, pack_type, m.Groups[1].Value);
							if (form.ProgramEndFlag == true)
							{
								webbrowser.Dispose();
								return;
							}
						}

						//m.Groups[3]を解体
						MatchCollection mc3 = Regex.Matches(m.Groups[3].Value, getRegexStr("4")+ ".*?", RegexOptions.IgnoreCase);
						if (mc3.Count == 0) continue;
						foreach (Match m3 in mc3)
						{
							//パックタイプ名がリンクになってることを考慮
							MatchCollection mc4 = Regex.Matches(m3.Groups[1].Value, "<a title=.*?href=\"(.*?)\".*?>(.*?)", RegexOptions.IgnoreCase);
							if(mc4.Count != 0)
								pack_type = mc4[0].Groups[2].Value;
							else
								pack_type = m3.Groups[1].Value;
							form.AddLog("パックタイプ :" + pack_type + "\n", LogLevel.情報);
							//mc3[0].Groups[2]を解体
							MatchCollection mc5 = Regex.Matches(m3.Groups[2].Value, "<a title=.*?href=\"(.*?)\".*?>(.*?)</a>", RegexOptions.IgnoreCase);
							foreach (Match m5 in mc5)
							{
								form.AddLog("パック :" + m5.Groups[2].Value + "\nURL :" + m5.Groups[1].Value + "\n", LogLevel.情報);
								getPackData(m5.Groups[1].Value, m5.Groups[2].Value, pack_type, m.Groups[1].Value);
								if (form.ProgramEndFlag == true)
								{
									webbrowser.Dispose();
									return;
								}
							}
						}
					}

				}
			}

			label.Visible = false;	//最後に非表示
		}

		//パックのページに移動し、カードのリンクを得る
		public void getPackData(string PackURL, string PackName, string PackType, string SeriesName)
		{
			Application.DoEvents();
			if (form.ProgramEndFlag == true)
			{
				webbrowser.Dispose();
				return;
			}

			UpdateLabel(PackName+"に接続");
			form.AddLog(PackName +"に接続 :" + PackURL, LogLevel.情報);
			webbrowser.NavigateAndWait(PackURL);

			HtmlDocument doc = webbrowser.Document;
			foreach (HtmlElement e in doc.GetElementsByTagName("div"))
			{
				if (!string.IsNullOrEmpty(e.GetAttribute("id")) && e.GetAttribute("id") == "body")
				{
					//概要を取得
					string 元文章 = e.InnerText;
					string 正規表現 = "(.*)↑.{0,10}収録カードリスト";
					MatchCollection mc = Regex.Matches(元文章, 正規表現, RegexOptions.IgnoreCase | RegexOptions.Singleline);

					if (mc.Count == 0)
					{
						form.AddLog("パック読み込みエラー\n", LogLevel.エラー);
						return;
					}

					string パック概要 = mc[0].Groups[1].Value;
					form.AddLog("パック概要:"+ パック概要.Replace("\r\n", "") + "\n", LogLevel.全部);

					//パックのテーブルを取得
					元文章 = e.InnerHtml.Replace("\r\n", "");
					正規表現 = "<div class=jumpmenu><a href=\"#navigator\">(.*?)(?=<div class=jumpmenu>)";
					MatchCollection mc2 = Regex.Matches(元文章, 正規表現, RegexOptions.IgnoreCase);

					if (mc2.Count == 0)
					{
						form.AddLog("パック読み込みエラー\n", LogLevel.エラー);
						return;
					}

					//なぜか</li>がないので<li>で代用
					//</A> <SPAN style="FONT-SIZE: 10px; DISPLAY: inline-block; LINE-HEIGHT: 130%; TEXT-INDENT: 0px"><A title="Super (20d)" href="http://yugioh-wiki.net/index.php?Super">Super</A>,<A title="Secret (131d)" href="http://yugioh-wiki.net/index.php?Secret">Secret</A></SPAN>

					元文章 = mc2[0].Groups[1].Value.Replace("\r\n", "");
					正規表現 = "<li>(.*?)<a title=.*?href=\"(.*?)\">《(.*?)》.*?(<span.*?</span>).*?(?=<li>)";
					MatchCollection mc3 = Regex.Matches(元文章, 正規表現, RegexOptions.IgnoreCase);
					foreach (Match m in mc3)
					{
						string log = "略号 :" + m.Groups[1].Value + "  カード :《" + m.Groups[3].Value + "》  レア :";

						log += "  URL :" + m.Groups[2].Value;

						if (m.Groups.Count == 4) //レア度ありならさらに分解
						{
							元文章 = m.Groups[4].Value;
							正規表現 = ">(.{1,30}?)</a>.*?";
							MatchCollection mc4 = Regex.Matches(元文章, 正規表現, RegexOptions.IgnoreCase);
							foreach (Match m4 in mc4)
							{
								foreach (var cc in m4.Captures)
								{
									log += cc + "  ";
								}
							}
						}

						form.AddLog(log, LogLevel.情報);
					}

				}
			}
		}
	}
}
