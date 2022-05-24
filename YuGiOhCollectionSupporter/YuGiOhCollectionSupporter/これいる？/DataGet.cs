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
				form.logform.AddLog(text,LV);
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

						//削除
					}
				}
				UpdateLabel("全パック取得完了");
				form.Invoke(new Action(() =>
				{
					form.PackDataList = PackDataList;
	//				form.PackGotFlag = true;
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
