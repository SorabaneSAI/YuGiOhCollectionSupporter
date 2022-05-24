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
				form.logform.AddLog(text, LV);
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
						//削除
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


			string log = "略号 :" + 略号 + "  カード :《" + CardName + "》 レア :" + Rare + "  URL :" + URL;
			AddLog(log, LogLevel.情報);

			return null;//new CardData(CardName,"" ,"",略号文字, 地域名, 略号番号, 略号番号桁数, Rare, PackName);

			//削除
				
			
		}

	}
}
