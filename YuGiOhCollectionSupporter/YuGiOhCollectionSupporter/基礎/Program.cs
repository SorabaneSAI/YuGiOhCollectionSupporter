using AngleSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Text;

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

		public static void Save<T>(string path, T data)
		{
			//jsonにシリアライズ
			string json = JsonConvert.SerializeObject(data,Formatting.Indented);
			using(StreamWriter sw = new StreamWriter(path,false, Encoding.UTF8))
            {
				sw.WriteLine(json);
            }
		}

		public static void Load<T>(string path,ref T data)
		{
			try
			{
				string json;
				using (StreamReader sr = new StreamReader(path,Encoding.UTF8))
				{
					json = sr.ReadToEnd();
					data = JsonConvert.DeserializeObject<T>(json);
				}
			}
			catch (System.IO.FileNotFoundException) //見つからなかったら作成　ほかは知らん
			{
//				File.Create(path);
				Save(path, data);
			}
			catch(System.IO.IOException e)
            {
				MessageBox.Show(e.Message, "謎のエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public static string ToJson(object obj)
        {
			return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

	}
}
