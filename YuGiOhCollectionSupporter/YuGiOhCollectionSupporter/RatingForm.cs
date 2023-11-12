using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using YuGiOhCollectionSupporter.データ;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static YuGiOhCollectionSupporter.KanabellForm;

namespace YuGiOhCollectionSupporter
{


	public partial class RatingForm : Form
	{
		public RatingForm(Form1 form1)
		{
			InitializeComponent();
			this.form = form1;
		}

		Form1 form;


		public async Task<List<RatingCardData>> GetHtml(List<string> errorlist)
		{
			//アクセスしてデータを取ってくる
			string str = "カードレート取得中";
			Program.WriteLog(str, LogLevel.必須項目);
			form.UpdateLabel(str);

			string seriesURL = textBox1.Text + textBox2.Text;
			var RateCardDataList = new List<RatingCardData>();
			await Task.Delay(1000); //負荷軽減のため１秒待機;

			var html2 = await Program.GetHtml(seriesURL);

			//取得できなかったら３回挑戦
			if (html2 == null)
			{
				for (var i = 0; i < 3; i++)
				{
					await Task.Delay(1000); //負荷軽減のため１秒待機;
					html2 = await Program.GetHtml(seriesURL);
					if (html2 != null)
					{
						goto goto_continue;
					}
				}

				errorlist.Add(seriesURL);
				Program.WriteLog($"エラー発生。ログファイル参照。[{seriesURL}]", LogLevel.エラー);

				if (errorlist.Count > 10)
				{
					Program.WriteLog($"エラーが多すぎるので強制終了", LogLevel.エラー);
					MessageBox.Show("エラーが多すぎたため、データ収集を終了します。（ログ参照）", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return RateCardDataList;
				}

			}

		goto_continue:;

			//最後へのページがなくなるまで続く
			int num = 0;
			while (true)
			{
				num++;
				//カード取得
				//カードがあるテーブルを取得
				var DivNameNode3 = html2.QuerySelector("div[id='fukki_main']");
				var table = DivNameNode3.QuerySelector("table");
				var trlist = html2.QuerySelectorAll("tr");

			//ごみもまざってるtrから推定
				foreach (var tr in trlist)
				{
				//						var td = row.QuerySelectorAll("> td");
					var font = tr.QuerySelector("font[id*='avg_point']");
					//fontがnullならいらない行
					if (font == null) continue;

					string rate = font.TextContent;

					var b = tr.QuerySelector("b");
					var a = b.QuerySelector("a");
					string url = textBox1.Text + a.GetAttribute("href").Trim();
					string name = a.TextContent.Trim();

					var carddata = new RatingCardData(name, url ,rate);
					RateCardDataList.Add(carddata);
				}


				//次のURLを調べる
				var DivNameNode2 = html2.QuerySelector("div[class='paginator']");
				var PageList = DivNameNode2.QuerySelectorAll("ul>li>a");

				foreach (var page in PageList)
				{
					if (page.TextContent.Trim() == "次 >")
					{
						await Task.Delay(1000); //負荷軽減のため１秒待機;

						html2 = await Program.GetHtml(textBox1.Text + page.GetAttribute("href").Trim());

						str = $"{num}ページ目";
						Program.WriteLog(str, LogLevel.情報);
						form.UpdateLabel(str);

					}
					if (page.TextContent.Trim() == "最後 >>")
					{
						goto next;
					}
				}

				//最後がなかったらそこが最終ページ
				break;

			next:;

			}

			return RateCardDataList;
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			var sw = new System.Diagnostics.Stopwatch();
			sw.Start();

			InvalidMenuItem();

			var errorlist = new List<string>();
			var ratelist = await GetHtml(errorlist);

			form.RateDB.RateDataList = ratelist;
			Program.Save(PriceDataBase.SaveDataPath, ratelist);


			ValidMenuItem();

			sw.Stop();
			TimeSpan ts = sw.Elapsed;

			string msg = $"販売情報の取得が完了しました。\n全データ件数{ratelist.Count}" + $"エラー件数:{errorlist.Count}件\n" + Program.ToJson(errorlist, Newtonsoft.Json.Formatting.None) + $"\nかかった時間:{ts.Hours}時間 {ts.Minutes}分 {ts.Seconds}秒 {ts.Milliseconds}ミリ秒";
			Program.WriteLog(msg, LogLevel.必須項目);
			MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

		}

		public void InvalidMenuItem()
		{
			Invoke(new Action(() =>
			{
				textBox1.Enabled = false;
				textBox2.Enabled = false;
				button1.Enabled = false;
				form.販売価格調査ToolStripMenuItem.Enabled = false;
			}));

		}

		public void ValidMenuItem()
		{
			Invoke(new Action(() =>
			{
				textBox1.Enabled = true;
				textBox2.Enabled = true;
				button1.Enabled = true;

				form.販売価格調査ToolStripMenuItem.Enabled = true;
			}));

		}

	}

	public class RateDataBase
	{
		public List<RatingCardData> RateDataList = new List<RatingCardData>();
		public static string SaveDataPath = Form1.SaveFolder + "\\" + "RateDataBase.json";

	}
	public class RatingCardData
	{
		public string Name = "";
		public string URL = "";
		public string Rate = "0.0";

		public RatingCardData(string name, string url, string rate)
		{
			Name = name;
			URL = url;
			Rate = rate;
		}
	}

}
