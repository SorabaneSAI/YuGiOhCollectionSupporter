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
		NonDispBrowser webbrowser = new NonDispBrowser();
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
			form.AddLog("遊戯王カードwikiに接続 :"+ config.getCardListURL());
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
					//あとの２つはゼロ幅の肯定的先読みアサーションなのでいらない
					foreach (Match m in mc)
					{
						if (m.Groups[1].Value.IndexOf("閲覧に際しての注意事項") != -1 ||
							m.Groups[1].Value.IndexOf("関連リンク") != -1 ||
							m.Groups[1].Value.IndexOf("備考") != -1)
							continue;

						SeriesName.Add(m.Groups[1].Value);
						form.AddLog("シリーズ :" + m.Groups[1] + "\n" + m.Value + "\n\n");
						string pack_type = "Normal";

						//m.Groups[2]を解体
						MatchCollection mc2 = Regex.Matches(m.Groups[2].Value, "<a title=.*?href=\"(.*?)\".*?>(.*?)</a>", RegexOptions.IgnoreCase);
						foreach (Match m2 in mc2)
						{
							form.AddLog("パック :" + m2.Groups[2].Value + "\nURL :" + m2.Groups[1].Value + "\n");
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
							form.AddLog("パックタイプ :" + pack_type + "\n");
							//mc3[0].Groups[2]を解体
							MatchCollection mc5 = Regex.Matches(m3.Groups[2].Value, "<a title=.*?href=\"(.*?)\".*?>(.*?)</a>", RegexOptions.IgnoreCase);
							foreach (Match m5 in mc5)
							{
								form.AddLog("パック :" + m5.Groups[2].Value + "\nURL :" + m5.Groups[1].Value + "\n");
							}
						}
						/*
						for (int i = 2; i < m.Groups.Count-2; i++)
						{
							if (i % 2 == 0)
							{
								//<a title=   </a>に囲まれた文字を取得
								MatchCollection mc2 = Regex.Matches(m.Groups[i].Value, "<a title=.*?href=\"(.*?)\".*?>(.*?)</a>", RegexOptions.IgnoreCase);
								foreach (Match m2 in mc2)
								{
									//↑と†は関係ないのでスキップ
									if (m2.Value.IndexOf("↑") != -1 || m2.Value.IndexOf("†") != -1)
										continue;
									form.AddLog("パック :" + m2.Groups[2].Value + "\nURL :" + m2.Groups[1].Value + "\n");
								}
							}
							else
							{
								pack_type = m.Groups[i].Value;
								form.AddLog("パックタイプ :" + pack_type + "\n");
							}
						}
						*/
					}

					/*
					//<ul class="list2" style="padding-left:16px;margin-left:16px"><li><a href="#x88b8682"> 閲覧に際しての注意事項 </a></li>
					//を探す(こっちしか階層構造になってない)
					foreach (HtmlElement e2 in e.GetElementsByTagName("ul"))
					{
						if (!string.IsNullOrEmpty(e2.GetAttribute("className")) && e2.GetAttribute("className") == "list2")
						{
							// リンク文字列とそのURLの列挙
							foreach (HtmlElement e3 in e2.GetElementsByTagName("li"))
							{
								if (!string.IsNullOrEmpty(e3.InnerText))
								{
									if (e3.InnerText.IndexOf("閲覧に際しての注意事項") != -1)
										continue;

									foreach (HtmlElement e4 in e3.GetElementsByTagName("A"))
									{
										string href = e4.GetAttribute("href"); // HREF属性の値
										string text = e4.InnerText; // リンク文字列

										//↑と†は関係ないのでスキップ
										if (text == "↑" || text == "†")
											continue;
										Console.WriteLine("リンク：" + href);
										Console.WriteLine("文字列：" + text + "\n");

										//liのなかにある拾うべきaは最初の一つだけ
										break;
									}
								}
							}
							break;
						}
					}
					break;
					*/
				}
			}
			Application.DoEvents();

			label.Visible = false;	//最後に非表示
		}

		public void getPackData(string PackURL)
		{

		}
	}
}
