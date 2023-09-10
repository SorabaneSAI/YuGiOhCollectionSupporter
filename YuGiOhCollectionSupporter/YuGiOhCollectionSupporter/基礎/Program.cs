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
using Newtonsoft.Json.Serialization;
using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Formatters.Binary;

namespace YuGiOhCollectionSupporter
{
	static class Program
	{
		private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private static HttpClient hc = new HttpClient();    //これは使い回すのが正しいらしい
		private static Form1 form1;

		[STAThread]
		static void Main()
		{
			log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));
			hc.DefaultRequestHeaders.Add("Accept-Language", "ja-JP");
			hc.Timeout = TimeSpan.FromSeconds(10.0);

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			form1 = new Form1();
			Application.Run(form1);
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

			}
			catch (TaskCanceledException e)
			{
				// タスクがキャンセルされたとき（一般的にタイムアウト）
				Log.Error("例外発生", e);
			}
			catch (Exception e)
			{
				Log.Error("例外発生", e);
				MessageBox.Show(e.Message, "謎のエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			//			WebClient wc = new WebClient();	非推奨らしい

			return null;
		}

		public static void Save<T>(string path, T data, DefaultContractResolver cr = null)
		{
			//jsonにシリアライズ
			string json = "";
			if(cr == null)
				json = JsonConvert.SerializeObject(data,Formatting.Indented);
			else
				json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { ContractResolver = cr });

			using (StreamWriter sw = new StreamWriter(path,false, Encoding.UTF8))
            {
				sw.WriteLine(json);
            }
		}

		public async static Task SaveAsync<T>(string path, T data, DefaultContractResolver cr = null)
		{
			try
			{

				await Task.Run(() => Save(path, data,cr));
			}
			catch(Exception ex)
            {
				Log.Error("例外発生", ex);
				MessageBox.Show(ex.Message, "謎のエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				form1.logform.AddLog(ex.Message, LogLevel.エラー);
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
		/*
		public static void SaveCardData()
        {
			Save(form1.CardDB.SaveDataPath, form1.CardDB);
		}
		*/
		public async static Task SaveCardDataAsync()
        {
			form1.label1.Visible = true;
			form1.UpdateLabel("カードデータセーブ中...");

			//これでUI固まらない
			await Task.Run(() =>
			{
				_ = SaveAsync(form1.CardDB.SaveDataPath, form1.CardDB, null/*new CardDataContractResolver(UserDataFlag)*/);
			});


			form1.label1.Visible = false;
		}

		public async static Task SaveUserDataAsync()
        {
			form1.label1.Visible = true;
			form1.UpdateLabel("ユーザーデータセーブ中...");

			//これでUI固まらない
			await Task.Run(() =>
			{
				_ = SaveAsync(form1.UserCardDB.SaveUserDataPath, form1.UserCardDB, null/*new CardDataContractResolver(UserDataFlag)*/);
			});


			form1.label1.Visible = false;
		}

		public static void SavePackData()
		{
			Save(form1.PackDB.SaveDataPath, form1.PackDB);
		}

		public static string ToJson(object obj, Formatting format = Formatting.Indented)
        {
			return JsonConvert.SerializeObject(obj, format);
        }

		public static DateTime ConvertDate(string date,string format)
        {
			DateTime output;
			if (DateTime.TryParseExact(date, format, null, DateTimeStyles.AssumeLocal, out output))
            {
				return output.Date;
            }
			Log.Error("日付変換失敗");
			MessageBox.Show("ConvertDateで日付変換失敗", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			Application.Exit();
			return output;
		}

		public static string ToString(DateTime date)
        {
			return date.ToString(@"yyyy\/MM\/dd");
        }

		//nodeがnullでなければ中身を返す nullなら無をかえす
		public static string getTextContent(AngleSharp.Dom.IElement node)
		{
			return (node != null ? node.TextContent.Trim() : "");
		}

		public static void WriteLog(string txt, LogLevel loglevel, Exception e = null)
        {
			form1.logform.AddLog(txt, loglevel);
			switch (loglevel)
			{
				case LogLevel.情報:
					Program.Log.Debug(txt);
					break;
				case LogLevel.警告:
					Program.Log.Warn(txt);
					break;
				case LogLevel.エラー:
					Program.Log.Error(txt);
					break;
				case LogLevel.必須項目:
					form1.UpdateLabel(txt);
					Program.Log.Info(txt);
					break;
				default:
					Program.Log.Error("不明なloglevel");
					MessageBox.Show("", "不明なloglevel", MessageBoxButtons.OK, MessageBoxIcon.Error);
					break;
			}
		}

		//文字列のnum番目の文字を取得
		public static string getTextElement(string txt,int num)
        {
			StringInfo si = new StringInfo(txt);
			return si.SubstringByTextElements(num,1);
		}

		//テキストの長さを取得
		public static int getTextLength(string txt)
        {
			StringInfo si = new StringInfo(txt);
			return si.LengthInTextElements;
		}

		//https://bluebirdofoz.hatenablog.com/entry/2022/10/15/234936
		/// <summary>
		/// 対象のディープコピーを行う
		/// シリアライズ(Serializable 属性)されていないクラスではエラー
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="src"></param>
		/// <returns></returns>
		public static T DeepCopy<T>(this T src)
		{
			using (MemoryStream stream = new MemoryStream())
			{
				var formatter = new BinaryFormatter();
				formatter.Serialize(stream, src);
				stream.Position = 0;

				return (T)formatter.Deserialize(stream);
			}
		}

		public static bool TryParse<TEnum>(string s, out TEnum wd) where TEnum : struct
		{
			return Enum.TryParse(s, out wd) && Enum.IsDefined(typeof(TEnum), wd);
		}
	}

}
