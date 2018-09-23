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

		public DataGet(Config cfg,  Label lbl)
		{
			config = cfg;
			label = lbl;
		}

		void UpdateLabel(string txt)
		{
			label.Text = txt;
			label.Update();
		}

		//遊戯王カードwikiのカードリストから、全カードの情報を入手
		public void getAllData()
		{
			label.Visible = true;  
			
			//まずカードリストの名前とURLを得る
			UpdateLabel("遊戯王カードwikiに接続");
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

					string MAX_STR = "50";	//あんまり長いと別のが引っかかる
					System.Text.RegularExpressions.MatchCollection mc =
						System.Text.RegularExpressions.Regex.Matches(e.InnerHtml.Replace("\r\n",""), "<div class=jumpmenu><a href=\"#navigator\">.{1,"+ MAX_STR + 
						"}</a></div><h3 id=.{1,"+ MAX_STR + "}>(.{1,"+ MAX_STR + "})<(.*?)</a></li></ul>", RegexOptions.IgnoreCase);

					//m.Groups[0]はValue
					foreach (System.Text.RegularExpressions.Match m in mc)
					{
						Console.WriteLine(m.Value+"\n"+ m.Groups[1]+"\n\n");
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
