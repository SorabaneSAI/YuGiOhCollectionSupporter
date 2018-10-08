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
		CardDataBase cdb = new CardDataBase();

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

		//遊戯王カードwikiのカードリストから、全カードの情報を入手
		public void getAllData()
		{
			try
			{

				form.Invoke(new Action(() =>
				{
					label.Visible = true;
					form.toolStripMenuItem1.Enabled = false;
					form.データ取得ToolStripMenuItem.Enabled = false;
				}));

				//まずカードリストの名前とURLを得る
				UpdateLabel("遊戯王カードwikiに接続");
				AddLog("遊戯王カードwikiに接続 :" + config.getCardListURL(), LogLevel.全部);

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
								if (form.ProgramEndFlag == true)
								{
									webbrowser.Dispose();
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
				UpdateLabel("セーブ中…");
				form.Invoke(new Action(() =>
				{
					form.CardDB = cdb;
				}));
				CardDataBase.Save(cdb);
				MessageBox.Show("カード情報の取得が終了しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception e)
			{
				MessageBox.Show("エラーで終了:"+ e.Message + "\n"+ e.StackTrace,	"エラー",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
			finally
			{
				form.Invoke(new Action(() =>
				{
					label.Visible = false;  //最後に非表示
					form.toolStripMenuItem1.Enabled = false;
					form.データ取得ToolStripMenuItem.Enabled = false;
				}));
			}
			return ;
		}

		//パックのページに移動し、カードのリンクを得る
		public void getPackData(string PackURL, string PackName, string PackType, string SeriesName)
		{
			if (form.ProgramEndFlag == true)
			{
				webbrowser.Dispose();
				return;
			}

			UpdateLabel(PackName + "に接続");
			AddLog(PackName + "に接続 :" + PackURL, LogLevel.情報);
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
						AddLog("パック読み込みエラー(" + PackName + ")\n", LogLevel.エラー);
						return;
					}

					string パック概要 = mc[0].Groups[1].Value;
					AddLog("パック概要:" + パック概要.Replace("\r\n", "") + "\n", LogLevel.全部);

					//概要からパック発売日時を取得(最初のyyyy年mm月dd日)
					string 正規表現Date = "(\\d{4}年\\d{1,2}月\\d{1,2}日)";
					MatchCollection mcdate = Regex.Matches(元文章.Replace("\r\n", ""), 正規表現Date, RegexOptions.IgnoreCase);

					if (mcdate.Count == 0)
					{
						AddLog("日時読み込みエラー(" + PackName + ")\n", LogLevel.エラー);
						return;
					}

					DateTime Date = DateTime.Parse(mcdate[0].Groups[1].Value);
					AddLog("パック発売日:" + Date.ToShortDateString() + "\n", LogLevel.全部);

					//パックのテーブルを取得
					元文章 = e.InnerHtml;
					正規表現 = "<div class=jumpmenu><a href=\"#navigator\">(.*?)(?=<div class=jumpmenu>)";
					MatchCollection mc2 = Regex.Matches(元文章, 正規表現, RegexOptions.IgnoreCase | RegexOptions.Singleline);

					if (mc2.Count == 0)
					{
						AddLog("パック読み込みエラー2(" + PackName + ")\n", LogLevel.エラー);
						return;
					}


					//ストラクチャーと仮定してテーブルがないか調査
					string 元文章3 = mc2[0].Groups[1].Value.Replace('\n', ' ');
					string 正規表現3 = "<tbody>(.*?)</tbody>";
					MatchCollection mc9 = Regex.Matches(元文章3, 正規表現3, RegexOptions.IgnoreCase);

					//もし０なら通常パックかもね
					if (mc9.Count == 0)
					{
						//なぜか</li>がないので<li>で代用
						//</A> <SPAN style="FONT-SIZE: 10px; DISPLAY: inline-block; LINE-HEIGHT: 130%; TEXT-INDENT: 0px"><A title="Super (20d)" href="http://yugioh-wiki.net/index.php?Super">Super</A>,<A title="Secret (131d)" href="http://yugioh-wiki.net/index.php?Secret">Secret</A></SPAN>
						string[] カード配列 = mc2[0].Groups[1].Value.Split('\n');
						正規表現 = "<li>(.*?)<a title=.*?href=\"(.*?)\">《(.*?)》(.*?)\r";
						foreach (string str in カード配列)
						{
							MatchCollection mc3 = Regex.Matches(str, 正規表現, RegexOptions.IgnoreCase);

							foreach (Match m in mc3)
							{
								string 略号 = m.Groups[1].Value;
								string CardName = m.Groups[3].Value;
								List<string> RareArray = new List<string>();
								string URL = m.Groups[2].Value;

								if (m.Groups.Count == 5) //レア度ありならさらに分解
								{

									元文章 = m.Groups[4].Value;
									string レア正規表現 = ">(.{1,30}?)</a>.*?";
									MatchCollection mc4 = Regex.Matches(元文章, レア正規表現, RegexOptions.IgnoreCase);
									foreach (Match m4 in mc4)
									{
										if (m4.Groups.Count > 1)
											RareArray.Add(m4.Groups[1].Value);
										else
											AddLog("レア度取得失敗", LogLevel.警告);
									}
								}
								if (RareArray.Count == 0)
									RareArray.Add("Normal");
								foreach (var rare in RareArray)
								{
									CardData card = GetCardData(略号, CardName, rare, URL, PackName);
									if(cdb.AddCardDataBase(card, form.CardDB) !=0)
										AddLog("重複データのためスキップ\n", LogLevel.情報);
								}
							}
						}
					}
					else
					{
						foreach (Match m9 in mc9)
						{
							string 元文章8 = m9.Value;
							string 正規表現8 = "<tr>(.*?)</tr>";
							MatchCollection mc8 = Regex.Matches(元文章8, 正規表現8, RegexOptions.IgnoreCase);

							if (mc8.Count == 0)
							{
								AddLog("パック読み込みエラー8(" + PackName + ")\n", LogLevel.エラー);
								return;
							}

							foreach (Match m8 in mc8)
							{
								string 元文章7 = m8.Value.Replace("\r", "");
								string 正規表現7 = "<td.*?>(.*?)</td>.*?href=\"(.*?)\">《(.*?)》</a>(.*?)</tr>";
								MatchCollection mc7 = Regex.Matches(元文章7, 正規表現7, RegexOptions.IgnoreCase);

								foreach (Match m7 in mc7)
								{
									string 略号 = m7.Groups[1].Value;
									string CardName = m7.Groups[3].Value;
									List<string> RareArray = new List<string>();
									string URL = m7.Groups[2].Value;

									string 元文章6 = m7.Groups[4].Value;
									string レア正規表現 = ">(.{1,30}?)</a>.*?";
									MatchCollection mc5 = Regex.Matches(元文章6, レア正規表現, RegexOptions.IgnoreCase);
									foreach (Match m5 in mc5)
									{
										if (m5.Groups.Count > 1)
											RareArray.Add(m5.Groups[1].Value);
										else
											AddLog("レア度取得失敗", LogLevel.警告);
									}
									if (RareArray.Count == 0)
										RareArray.Add("Normal");

									foreach (var rare in RareArray)
									{
										CardData card = GetCardData(略号, CardName, rare, URL,PackName);
										if (cdb.AddCardDataBase(card, form.CardDB) != 0)
											AddLog("重複データのためスキップ\n", LogLevel.情報);
									}
									//最初の１つだけでいい
									break;
								}
							}

							//最初の１つだけでいい
							break;
						}
					}
					//なんかエラー出るし１つでいい
					break;
				}
			}
		}

		public CardData GetCardData(string 略号, string CardName, string Rare, string URL, string PackName)
		{
			string 略号文字 = "";
			string 地域名 = "";
			int 略号番号 = 0;
			int 略号番号桁数 = 2;
			//略号を分解
			if (略号 != "")
			{
				string[] strarray = 略号.TrimEnd().Split('-');
				略号文字 = strarray[0];
				string 番号数字 = "";

				foreach (char c in strarray[1])
				{
					if (!Char.IsLetter(c))
						番号数字 += c;
					else
						地域名 += c;
				}
				略号番号 = int.Parse(番号数字);
				略号番号桁数 = 番号数字.Length;
			}

			string log = "略号 :" + 略号 + "  カード :《" + CardName + "》 レア :" + Rare + "  URL :" + URL;
			AddLog(log, LogLevel.情報);

			return new CardData(CardName, 略号, 地域名, 略号番号, 略号番号桁数, Rare, PackName);

			/*
			//フリガナ取得
			AddLog(CardName + "に接続 :" + URL, LogLevel.全部);
			webbrowser.NavigateAndWait(URL);
			HtmlDocument doc = webbrowser.Document;
			foreach (HtmlElement e in doc.GetElementsByTagName("div"))
			{
				if (!string.IsNullOrEmpty(e.GetAttribute("id")) && e.GetAttribute("id") == "body")
				{
					//概要を取得
					string 元文章 = e.InnerHtml;
					string 正規表現 = "《(.*?)(?<!<)/(.*?)》";
					MatchCollection mc = Regex.Matches(元文章, 正規表現, RegexOptions.IgnoreCase | RegexOptions.Singleline);

					//<h2 id="content_1_0">《<ruby><rb>星杯</rb><rp>(</rp><rt>せいはい</rt><rp>)</rp></ruby>を<ruby><rb>戴</rb><rp>(</rp><rt>いただ</rt><rp>)</rp></ruby>く<ruby><rb>巫女</rb><rp>(</rp><rt>みこ</rt><rp>)</rp></ruby>/Crowned by the World Chalice》
					//後ろから/を検索しようとするとスタッシュバスターの英名で引っかかる
					if (mc.Count == 0)
					{
						AddLog("カード読み込みエラー(" + CardName + ")\n", LogLevel.エラー);
						return null;
					}

					string 英語名 = mc[0].Groups[2].Value;

					string 正規表現2 = "(<rb>.*?</rb>)|(<rp>.*?</rp>)|(<ruby>)|(</ruby>)|(<rt>)|(</rt>)";
					string 読み = Regex.Replace(mc[0].Groups[1].Value, 正規表現2,"", RegexOptions.IgnoreCase);

					string 略号文字 = "";
					string 地域名 = "";
					int 略号番号=0;
					int 略号番号桁数=2;
					//略号を分解
					if (略号 != "")
					{
						string[] strarray = 略号.TrimEnd().Split('-');
						略号文字 = strarray[0];
						string 番号数字 = "";

						foreach (char c in strarray[1])
						{
							if (!Char.IsLetter(c))
								番号数字 += c;
							else
								地域名 += c;
						}
						略号番号 = int.Parse(番号数字);
						略号番号桁数 = 番号数字.Length;
					}

					string log = "略号 :" + 略号 + "  カード :《" + CardName + "》 ("+読み +"/"+ 英語名+") レア :" + Rare + "  URL :" + URL;
					AddLog(log, LogLevel.情報);

					return new CardData(CardName,  略号, 地域名, 略号番号, 略号番号桁数, Rare, PackName);
				}
			}
			return null;
			*/
		}
	}
}
