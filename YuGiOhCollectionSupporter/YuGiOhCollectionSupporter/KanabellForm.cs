using AngleSharp.Dom;
using MigraDoc.DocumentObjectModel.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using YuGiOhCollectionSupporter.データ;

namespace YuGiOhCollectionSupporter
{
	public partial class KanabellForm : Form
	{
		public enum ERank { A,B,C,D,在庫なし,その他}
		public class KanabellCard
		{
			string Rare="";
			string 分類1="";
			string 分類2="";
			string Name = "";
			ERank Rank = ERank.その他;
			int Price = 0;

			public KanabellCard(string rare, string bunrui1, string bunrui2, string name, ERank rank, int price)
			{
				Rare = rare;
				分類1 = bunrui1;
				分類2 = bunrui2;
				Name = name;
				Rank = rank;
				Price = price;
			}

		}


		public KanabellForm()
		{
			InitializeComponent();
		}

		public async Task<List<KanabellCard>> GetHtml(List<string> errorlist)
		{
			//アクセスしてデータを取ってくる
			Program.WriteLog("販売価格取得中", LogLevel.必須項目);

			var html = await Program.GetHtml(textBox1.Text + textBox2.Text);
			if (html == null)
			{
				Program.WriteLog($"エラー発生。指定URLは存在しません。", LogLevel.エラー);
				return new List<KanabellCard>();
			}

			//まずシリーズを取得
			var SeriesURLList = new List<string>();
			var DivNameNode = html.QuerySelector("div[id='search__detail-list']");
			var ListLiNodes = DivNameNode.QuerySelectorAll("li");

			foreach ( var node in ListLiNodes )
			{
				var aNode = node.QuerySelector("a");
				string url = aNode.GetAttribute("href").Trim();
				SeriesURLList.Add(textBox1.Text + url);
			}

			var KanabellCardList = new List<KanabellCard>();


			foreach (var seriesURL in SeriesURLList)
            {
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
						return KanabellCardList;
					}

					continue;
				}

				goto_continue:;

				//次へのページがなくなるまで続く
				while (true)
				{
					//カード取得
					//カードがあるテーブルを取得
					var DivNameNode3 = html2.QuerySelector("div[id='ListSell']");
					var table = DivNameNode3.QuerySelector(" > table");
					
//					var tr = table.QuerySelectorAll("> tbody > tr");
					var tr2 = table.Children[0].Children;   //上のQuerySelectorAllで子だけを指定するのがどうしてもうまくいかなかった
					foreach (var row in tr2)
					{
//						var td = row.QuerySelectorAll("> td");
						var td2 = row.Children;
						foreach (var cell in td2)
						{
							var carddata = getCardData(cell);
							KanabellCardList.Add(carddata);
						}
					}


					//次のURLを調べる
					var DivNameNode2 = html2.QuerySelector("div[class='CardNav CardNavTop clearfix']");
					var PageList = DivNameNode2.QuerySelectorAll("p>span>a");

					foreach (var page in PageList)
					{
						if (page.TextContent.Trim() == "次へ")
						{
							await Task.Delay(1000); //負荷軽減のため１秒待機;

							html2 = await Program.GetHtml(textBox1.Text + page.GetAttribute("href").Trim());

							goto next;
						}
					}

					//次へがなくなったら次のシリーズ
					break;

					next:;
				}

			}
			return KanabellCardList;
		}

		public KanabellCard getCardData(AngleSharp.Dom.IElement cell)
		{
			//カードのあるセルからテーブルを取得

			var rarestr = "";
			var seriesstr1 = "";
			var seriesstr2 = "";
			var namestr = "";
			ERank rank = ERank.在庫なし;
			int price = 0;

			var cardnodeTable = cell.QuerySelector("table");
			foreach (var row2 in cardnodeTable.QuerySelectorAll("tr"))
			{

				var infonode = row2.QuerySelector("td[class='ListSellHeadRar']");
				if (infonode != null)
				{
					rarestr = infonode.QuerySelector("p>span").TextContent.Trim();  //レアリティ抽出
					var seriesstr = infonode.QuerySelector("p").TextContent.Trim(); //シリーズ抽出　でも未加工

					var serieslist = seriesstr.Split('>');
					seriesstr1 = serieslist[0].Replace(rarestr,"").Trim();	//レアリティ文字も混ざっちゃうので消す
					seriesstr2 = serieslist[1].Trim();
					continue;
				}

				var namenode = row2.QuerySelector("th>div>a");
				if (namenode != null)
				{
					namestr = namenode.TextContent.Trim();
					continue;
				}

				var pricenode = row2.QuerySelector("div[class='clearfix']");
				if(pricenode != null)
				{
					if(pricenode.QuerySelector("span[class='None']") != null)
					{
						rank = ERank.在庫なし;
						continue;
					}

					var table = pricenode.QuerySelector("table");

					var firstnode = table.QuerySelector("tr");  //プライスは最初の１つだけでいい

					var ranknode = firstnode.QuerySelector("td>img");
					string rank_graphic_URL = ranknode.GetAttribute("src").Trim();
					rank = GetERank(rank_graphic_URL);

					var pricenode2 = firstnode.QuerySelector("div[class*='card_list_cell']");
					string pricestr = pricenode2.QuerySelector("span").TextContent.Trim();
					//円を除く
					pricestr = pricestr.Replace("円","");
					price = int.Parse(pricestr);
					continue;
				}
			}

			return new KanabellCard(rarestr, seriesstr1,seriesstr2, namestr, rank, price);
		}

		public ERank GetERank(string URL)
		{
			if (URL.Contains("a.gif")) return ERank.A;
			if (URL.Contains("b.gif")) return ERank.A;
			if (URL.Contains("c.gif")) return ERank.A;
			if (URL.Contains("d.gif")) return ERank.A;
			if (URL.Contains("card_none.gif")) return ERank.在庫なし;
			return ERank.その他;
		}

		public void Relation(KanabellCard card, CardDataBase DB)
		{
			//カードと関連付ける
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var sw = new System.Diagnostics.Stopwatch();
			sw.Start();

			InvalidMenuItem();

			var errorlist = new List<string>();
			var task = GetHtml(errorlist);

			var pricelist = task.Result;
			Program.Save(PriceDataBase.SaveDataPath, pricelist);


			ValidMenuItem();

			sw.Stop();
			TimeSpan ts = sw.Elapsed;

			string msg = $"販売情報の取得が完了しました。\n全データ件数{pricelist.Count}" + $"エラー件数:{errorlist.Count}件\n" + Program.ToJson(errorlist, Newtonsoft.Json.Formatting.None) + $"\nかかった時間:{ts.Hours}時間 {ts.Minutes}分 {ts.Seconds}秒 {ts.Milliseconds}ミリ秒";
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
				button2.Enabled = false;
				button3.Enabled = false;

			}));

		}

		public void ValidMenuItem()
		{
			Invoke(new Action(() =>
			{
				textBox1.Enabled = true;
				textBox2.Enabled = true;
				button1.Enabled = true;
				button2.Enabled = true;
				button3.Enabled = true;
			}));

		}

	}
}
