using AngleSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YuGiOhCollectionSupporter
{
	static class Program
	{
		private static HttpClient hc = new HttpClient();	//これは使い回すのが正しいらしい

		[STAThread]
		static void Main()
		{
			hc.DefaultRequestHeaders.Add("Accept-Language", "ja-JP");
			hc.Timeout = TimeSpan.FromSeconds(10.0);

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}

		public static async Task<AngleSharp.Html.Dom.IHtmlDocument> GetHtml(string URL)
		{

			try
			{

//				var config = Configuration.Default.WithJs();
//				var doc = await BrowsingContext.New(config).OpenAsync(URL);
			
				var response = await hc.GetAsync(URL);
				var sorce = await response.Content.ReadAsStringAsync();
				var parser = new AngleSharp.Html.Parser.HtmlParser();
				return parser.ParseDocument(sorce);
			}
			catch (HttpRequestException e)
			{
				// 404エラーや、名前解決失敗など
				MessageBox.Show(e.Message,	"エラー",MessageBoxButtons.OK,MessageBoxIcon.Error);

				/*
				// InnerExceptionも含めて、再帰的に例外メッセージを表示する
				Exception ex = e;
				while (ex != null)
				{
					Console.WriteLine("例外メッセージ: {0} ", ex.Message);
					ex = ex.InnerException;
				}
				*/
			}
			catch (TaskCanceledException e)
			{
				// タスクがキャンセルされたとき（一般的にタイムアウト）
				MessageBox.Show(e.Message, "タイムアウトなどでタスクがキャンセルされました", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "謎のエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			//			WebClient wc = new WebClient();	非推奨らしい

			return null;
		}

	}
}
