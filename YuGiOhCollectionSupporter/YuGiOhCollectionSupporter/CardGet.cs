using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YuGiOhCollectionSupporter
{
	public class CardGet : NonDispBrowser
	{
		public string PackName;
		public PackData packdata;
		public Form1 form;
		public bool ExecutedFlag = false;   //実行済みならもっかいやらない

		public CardGet(string packname, PackData pd, Form1 f)
		{
			PackName = packname;
			packdata = pd;
			form = f;
		}

		void DeletePack()
		{
			form.Invoke(new Action(() =>
			{
				form.DeletePack(PackName);
				form.label1.Text = "残りパック数:" + form.PackList.Count;
				form.label1.Update();
			}));
		}

		void AddLog(string text, LogLevel LV)
		{
			//他スレッド操作
			form.Invoke(new Action(() =>
			{
				form.AddLog(text, LV);
			}));
		}

		//読み込み語に処理開始
		protected override void OnDocumentCompleted(
					  WebBrowserDocumentCompletedEventArgs ev)
		{
			// ページにフレームが含まれる場合にはフレームごとに
			// このメソッドが実行されるため実際のURLを確認する
			//			if (ev.Url == this.Url)
			try
			{
				if (ExecutedFlag == true) return;
				ExecutedFlag = true;
				HtmlDocument doc = Document;
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
										PackData result = form.CardDB.PackDB.Find(n => n.Name == PackName);	//旧データを探す
										if (packdata.AddCardDataBase(card, result) != 0)
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
											CardData card = GetCardData(略号, CardName, rare, URL, PackName);
											PackData result = form.CardDB.PackDB.Find(n => n.Name == PackName); //旧データを探す
											if (packdata.AddCardDataBase(card, result) != 0)
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
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				DeletePack();
				Dispose();
			}
		}

		public CardData GetCardData(string 略号, string CardName, string Rare, string URL, string PackName)
		{
//			Application.DoEvents();

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
