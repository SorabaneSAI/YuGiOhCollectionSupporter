using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
		public void getData()
		{
			label.Visible = true;  
			
			//まずカードリストの名前とURLを得る
			UpdateLabel("遊戯王カードwikiに接続");
			webbrowser.NavigateAndWait(config.getCardListURL());

			HtmlDocument doc = webbrowser.Document;

			//<div class="contents">を探す
			foreach (HtmlElement e in doc.GetElementsByTagName("div"))
			{
				if (!string.IsNullOrEmpty(e.GetAttribute("className")) && e.GetAttribute("className") == "contents")
				{
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

										/*
										//改行があったら後ろは余計なものあり
										int 改行場所 = text.IndexOf("\r\n");
										if (改行場所 >= 0)
										{
											text = text.Substring(0, 改行場所);
											//これがシリーズになる
											SeriesName.Add(text);
											Console.WriteLine("シリーズ：" + text);
											break;
										}
										*/

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
				}
			}
			Application.DoEvents();

			label.Visible = false;	//最後に非表示
		}
	}
}
