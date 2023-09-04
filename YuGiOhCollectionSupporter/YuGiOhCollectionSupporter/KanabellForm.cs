using AngleSharp.Dom;
using AngleSharp.Html.Dom;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace YuGiOhCollectionSupporter
{
	public partial class KanabellForm : Form
	{
		Form1 form;

		public enum EKanabellRank { A,B,C,D,在庫なし,その他}
		public class KanabellCard
		{
			public string Rare ="";
			public string 分類1 ="";
			public string 分類2="";
			public string Name = "";
			public EKanabellRank Rank = EKanabellRank.その他;
			public int Price = 0;
			public string URL = "";

			public string Note="";

			//この２つは②で追加される
			public string 備考詳細 = "";
			public string 略号Full = "";
			public KanabellCard(string rare, string bunrui1, string bunrui2, string name, EKanabellRank rank, int price, string uRL)
			{
				Rare = rare;
				分類1 = bunrui1;
				分類2 = bunrui2;
				Name = name;
				Rank = rank;
				Price = price;
				URL = uRL;

				//最後のカッコを探す
				if (Name.Last() == ')')
				{
					int Bra = Name.LastIndexOf('(');
					if (Bra == -1)
					{
						Program.WriteLog($")があるのに(がない\n {Program.ToJson(this)}", LogLevel.エラー);
					}
					else
					{
						Note = Name.Substring(Bra + 1, Name.Length - Bra - 2);    //カッコの中身
						Name = Name.Substring(0, Bra); //カッコを除いた名前
					}
				}


			}

		}


		public KanabellForm(Form1 form)
		{
			InitializeComponent();
			this.form = form;

			for (int i = 0; i <= 10; i++)
			{
				comboBox1.Items.Add((i*10).ToString()+"%");
				comboBox2.Items.Add((i*10).ToString()+"%");
			}
		}

		public async Task<List<KanabellCard>> GetHtml(List<string> errorlist)
		{
			//アクセスしてデータを取ってくる
			string str = "販売価格取得中";
			Program.WriteLog(str, LogLevel.必須項目);
			form.UpdateLabel(str);

			var html = await Program.GetHtml(textBox1.Text + textBox2.Text);
			if (html == null)
			{
				Program.WriteLog($"エラー発生。指定URLは存在しません。", LogLevel.エラー);
				return new List<KanabellCard>();
			}

			//まずシリーズを取得
			var SeriesURLList = new List<string>();
			var SeriesNameList = new List<string>();
			var DivNameNode = html.QuerySelector("div[id='search__detail-list']");
			var ListLiNodes = DivNameNode.QuerySelectorAll("li[class*='yugioh_ocg-list-items']");	//ラッシュデュエルは含まない

			foreach ( var node in ListLiNodes )
			{
				var aNode = node.QuerySelector("a");
				var name = aNode.TextContent.Trim();
				//スルーするページかチェック
				foreach (var throughdata in form.ThroughPageDataList)
				{
					if (name == throughdata.Word)
					{
						goto goto_through;
					}
				}
				string url = aNode.GetAttribute("href").Trim();
				SeriesNameList.Add(name);
				SeriesURLList.Add(textBox1.Text + url);

			goto_through:;
			}

			var KanabellCardList = new List<KanabellCard>();
			for (int k = 0; k < SeriesURLList.Count; k++)
            {
				string seriesURL = SeriesURLList[k];
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
				int num = 0;
				while (true)
				{
					num++;
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

							str = $"{SeriesNameList[k]} {k}/{SeriesURLList.Count}の{num}ページ目  {Program.ToJson(KanabellCardList.Last())}";
							Program.WriteLog(str, LogLevel.情報);
							form.UpdateLabel(str);

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
			EKanabellRank rank = EKanabellRank.在庫なし;
			int price = 0;
			string url = "";

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
					url = textBox1.Text + namenode.GetAttribute("href").Trim();
					continue;
				}

				var pricenode = row2.QuerySelector("div[class='clearfix']");
				if(pricenode != null)
				{
					if(pricenode.QuerySelector("span[class='None']") != null)
					{
						rank = EKanabellRank.在庫なし;
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

			return new KanabellCard(rarestr, seriesstr1,seriesstr2, namestr, rank, price,url);
		}

		public EKanabellRank GetERank(string URL)
		{
			if (URL.Contains("a.gif")) return EKanabellRank.A;
			if (URL.Contains("b.gif")) return EKanabellRank.B;
			if (URL.Contains("c.gif")) return EKanabellRank.C;
			if (URL.Contains("d.gif")) return EKanabellRank.D;
			if (URL.Contains("card_none.gif")) return EKanabellRank.在庫なし;
			return EKanabellRank.その他;
		}


		private async void button1_Click(object sender, EventArgs e)
		{
			var sw = new System.Diagnostics.Stopwatch();
			sw.Start();

			InvalidMenuItem();

			var errorlist = new List<string>();
			var pricelist = await GetHtml(errorlist);

			form.PriceDB.PriceDataList = pricelist;
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
				button4.Enabled = false;
				button5.Enabled = false;
				button6.Enabled = false;
				comboBox1.Enabled = false;
				comboBox2.Enabled = false;

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
				button4.Enabled = true;
				button5.Enabled = true;
				button6.Enabled = true;
				comboBox1.Enabled = true;
				comboBox2.Enabled = true;
			}));

		}

		private void button4_Click(object sender, EventArgs e)
		{
			RarityPairForm f = new RarityPairForm(form.RarityPairDataList,form);
			f.ShowDialog(this);
			f.Dispose();
			Program.Save(form.RarityPairSavePath, form.RarityPairDataList);

		}

		private void button5_Click(object sender, EventArgs e)
		{
			ThroughPageForm f = new ThroughPageForm(form.ThroughPageDataList, form);
			f.ShowDialog(this);
			f.Dispose();
			Program.Save(form.ThroughPageSavePath, form.ThroughPageDataList);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			MakePair.MakeDictionary(form,this);
		}

		private async void button6_Click(object sender, EventArgs e)
		{
			int percent = comboBox2.SelectedIndex - comboBox1.SelectedIndex;
			if(!(percent > 0 && percent <=10))
			{
				MessageBox.Show("範囲が不正です", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			//メッセージボックスを表示する
			DialogResult result = MessageBox.Show($"捜索には{(int)(percent*0.1* form.PriceDB.PriceDataList.Count / 60)}分程度かかることが予測されます。\n本当に始めますか？", "",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Exclamation,
				MessageBoxDefaultButton.Button2);

			if (result == DialogResult.No) return;

			var sw = new System.Diagnostics.Stopwatch();
			sw.Start();

			InvalidMenuItem();

			var errorlist = new List<string>();
			await get略号(errorlist, form.PriceDB.PriceDataList);

			Program.Save(PriceDataBase.SaveDataPath, form.PriceDB.PriceDataList);


			ValidMenuItem();

			sw.Stop();
			TimeSpan ts = sw.Elapsed;

			string msg = $"販売情報の取得が完了しました。\n全データ件数{(int)(percent * 0.1 * form.PriceDB.PriceDataList.Count)}" + $"エラー件数:{errorlist.Count}件\n" + Program.ToJson(errorlist, Newtonsoft.Json.Formatting.None) + $"\nかかった時間:{ts.Hours}時間 {ts.Minutes}分 {ts.Seconds}秒 {ts.Milliseconds}ミリ秒";
			Program.WriteLog(msg, LogLevel.必須項目);
			MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private async Task get略号(List<string> errorlist ,List<KanabellCard> KanabellCardList)
		{
			string str = "カードページ取得中";
			Program.WriteLog(str, LogLevel.必須項目);
			form.UpdateLabel(str);

			int StartNum = (int)(KanabellCardList.Count * comboBox1.SelectedIndex * 0.1);
			int EndNum   = (int)(KanabellCardList.Count * comboBox2.SelectedIndex * 0.1);

			for (int k = StartNum; k < EndNum; k++)
			{
				string URL = KanabellCardList[k].URL;
				if (URL == "") continue;	//①を実行していなければ空白 っていうか要素がないか

				await Task.Delay(1000); //負荷軽減のため１秒待機;

				var html2 = await Program.GetHtml(URL);

				//取得できなかったら３回挑戦
				if (html2 == null)
				{
					for (var i = 0; i < 3; i++)
					{
						await Task.Delay(1000); //負荷軽減のため１秒待機;
						html2 = await Program.GetHtml(URL);
						if (html2 != null)
						{
							goto goto_continue;
						}
					}

					errorlist.Add(URL);
					Program.WriteLog($"エラー発生。ログファイル参照。[{URL}]", LogLevel.エラー);

					if (errorlist.Count > 10)
					{
						Program.WriteLog($"エラーが多すぎるので強制終了", LogLevel.エラー);
						MessageBox.Show("エラーが多すぎたため、データ収集を終了します。（ログ参照）", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return ;
					}

					continue;
				}

			goto_continue:;

				AnalyzeHtml略号(KanabellCardList[k], html2);

				str = $"{k}/{(EndNum-StartNum)}  {KanabellCardList[k].略号Full} {KanabellCardList[k].備考詳細} ";
				Program.WriteLog(str, LogLevel.情報);
				form.UpdateLabel(str);


			}
		}

		//略号、備考を追記する
		private void AnalyzeHtml略号(KanabellCard kanabell, IHtmlDocument html)
		{
			var divNode = html.QuerySelector("div[id*='card_detail']");
			//			var trNodeList = divNode.QuerySelector("table").Children[0].Children;
			var tdNodeList = divNode.QuerySelectorAll("td");

			kanabell.備考詳細 = tdNodeList[0].TextContent.Trim();	//最初が備考のはず・・・

			foreach (var node in tdNodeList)
            {
				if(node.TextContent.Contains("シリアル番号"))
				{
					var twin = node.TextContent.Split(':');
					kanabell.略号Full = twin[1].Trim();

					if(kanabell.略号Full == "")
					{
						kanabell.略号Full = "なし";
					}

					break;
				}
            }
        }
	}
}
