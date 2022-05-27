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
using System.Globalization;

namespace YuGiOhCollectionSupporter
{
	static class Program
	{
		public static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private static HttpClient hc = new HttpClient();    //これは使い回すのが正しいらしい

		[STAThread]
		static void Main()
		{
			log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));
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
				Log.Error("例外発生",e);
				MessageBox.Show(e.Message,	"エラー",MessageBoxButtons.OK,MessageBoxIcon.Error);

			}
			catch (TaskCanceledException e)
			{
				// タスクがキャンセルされたとき（一般的にタイムアウト）
				Log.Error("例外発生", e);
				MessageBox.Show(e.Message, "タイムアウトなどでタスクがキャンセルされました", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception e)
			{
				Log.Error("例外発生", e);
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
				Log.Warn("ファイル作成");
				Save(path, data);
			}
			catch(System.IO.IOException e)
            {
				Log.Error("例外発生", e);
				MessageBox.Show(e.Message, "謎のエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
			}
		}

		public static string ToJson(object obj)
        {
			return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

		public static DateTimeOffset ConvertDate(string date,string format)
        {
			DateTimeOffset output;
			if (DateTimeOffset.TryParseExact(date, format, null, DateTimeStyles.AssumeLocal, out output))
            {
				return output.Date;
            }
			Log.Error("日付変換失敗");
			MessageBox.Show("ConvertDateで日付変換失敗", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			Application.Exit();
			return output;
		}

		public static string ToString(DateTimeOffset date)
        {
			return date.ToString(@"yyyy\/MM\/dd");
        }

		//nodeがnullでなければ中身を返す nullなら無をかえす
		public static string getTextContent(AngleSharp.Dom.IElement node)
		{
			return (node != null ? node.TextContent.Trim() : "");
		}

	}
}
