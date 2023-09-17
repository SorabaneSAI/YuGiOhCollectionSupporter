using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.Ports;

namespace YuGiOhCollectionSupporter
{
	public partial class CardListUI : UserControl
	{
		CardDataBase CardDB;	//こちらはパック表示でも変更しない
		PackData Pack;
		CardDataBase PackCardDB = new CardDataBase();	//パック表示のとき変更される
		Form1 form;
		bool あいうえお順Flag;
		int Page =1;
		const int MAX_NUM = 100;

		Color red = Color.FromArgb(255, 128, 128);
		Color yellow = Color.FromArgb(255, 255, 128);
		Color green = Color.FromArgb(128, 255, 128);

		public CardListUI(CardDataBase cardDB, PackData pack, Form1 form1,bool あいうえお順フラグ)
		{
			InitializeComponent();
			Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
			Dock = DockStyle.Fill;	//これやばいらしい？
			Init(cardDB, pack,form1,あいうえお順フラグ,1);
		}

		public void Init(CardDataBase cardDB, PackData pack, Form1 form1,bool あいうえお順フラグ,int page)
		{
			form = form1;
			CardDB = cardDB;
			あいうえお順Flag = あいうえお順フラグ;
			Page = page;
			if (あいうえお順フラグ)
			{
				Pack = null;
				linkLabel1.Visible = false;
				label1.Visible = false;
				label4.Visible = false;

				PackCardDB = cardDB;
			}
			else if (true)
			{
				Pack = pack;
				linkLabel1.Text = Pack.Name;
				label1.Text = Pack.TypeName;
				label4.Text = Pack.BirthDay.ToString("yyyy/MM/dd");

				PackCardDB.CardList.Clear();


				//variationがそのパックのカードで、そのvariationのみの状態のカードを新しく作成し、それ用のリストに追加
				foreach (var card in cardDB.CardList)
                {
					List<CardVariation> VariationList = new List<CardVariation>();
					foreach (var variation in card.ListVariations)
                    {
						if(variation.発売パック.URL == pack.URL)
                        {
							VariationList.Add(variation);

						}
					}
					var tmpcard = new CardData(card, VariationList);
					PackCardDB.CardList.Add(tmpcard);

				}

				//略号ソートのためにパックデータの完全なものが必要
				foreach (var card in PackCardDB.CardList)
				{
					foreach (var variation in card.ListVariations)
					{
						variation.発売パック = form.PackDB.SearchPackData(variation.発売パック.URL);
					}
				}

				//順番はめちゃくちゃになってるので略号順にソート
				PackCardDB.CardList.Sort((a, b) => new CardVariation().Compare(a.ListVariations[0], b.ListVariations[0]));


			}

			

			collectDataUI1.Init(form.getAllCardNumHave(PackCardDB), PackCardDB.getAllCardNum(),form.getCardHaveNumCode(PackCardDB).Item1, form.getCardHaveNumCode(PackCardDB).Item2,
				form.getCardHaveNumRarity(PackCardDB).Item1,form.getCardHaveNumRarity(PackCardDB).Item2);    //埋め込むのに引数のないコンストラクタが必要なので初期化


			dataGridView1.Rows.Clear();

			//パックならレアリティ違いも全部表示
			//スタートするべきカード番号と終わるべきカード番号を決定
			int start_i = (page - 1) * MAX_NUM;
			int end_i = page * MAX_NUM;
			int pagenum = 1;
			
			//パック順なら各ページに何個置けるか計算してそのページの最初と最後のカードを決定
			if(!あいうえお順フラグ)
			{
//				for (int m = 0; m < page; m++)
				{
					bool IsStartEndDeside = false;
					start_i = 0;
					int p = 1;
					int sum = 0;
					for (int n = 0; n < PackCardDB.CardList.Count; n++)
					{
						sum +=PackCardDB.CardList[n].ListVariations.Count;
						if (IsStartEndDeside == false)
							end_i = n+1;

						if (sum >=100)
						{
							if (p == page)
							{
								IsStartEndDeside = true;
							}
							{
								//次のページ計算
								p++;
								if(IsStartEndDeside == false)
									start_i = n + 1;
								sum = 0;
								pagenum++;
							}
						}
					}
				}
			}

			for (int i=start_i; i< end_i; i++ )
            {
				if (i >= PackCardDB.CardList.Count) break;
				var card = PackCardDB.CardList[i];
				var twincarddata = form.getTwinCardData(card);	//UserDataは完全だが、CardDataは削られてる
				string ryakugou = "";
				string rarity = "";

				(int rarity_havenum, int rarity_allnum) = twincarddata.getCardHaveNumRarity();
				(int code_havenum, int code_allnum) = twincarddata.getCardHaveNumCode();

				int r = 0;  //ローカル関数で使うため
				var variation = card.ListVariations[0];
				if (あいうえお順フラグ)
				{
					//１つしか存在しない略号、レアリティは文字にする
					if (rarity_allnum == 1)
						rarity = card.ListVariations[0].rarity.Initial;
					else
						rarity = $"{rarity_havenum} / {rarity_allnum}";
					if (code_allnum == 1)
						ryakugou = card.ListVariations[0].略号.get略号Full();
					else
						ryakugou = $"{code_havenum} / {code_allnum}";

					AddDataGridView();
				}
				else
				{
					rarity_allnum = 1;
					code_allnum = 1;

					for (r = 0; r < card.ListVariations.Count; r++)
					{
						variation = card.ListVariations[r];

						rarity_havenum = twincarddata.get所持フラグ(variation) ? 1 : 0;
						code_havenum = rarity_havenum;

						rarity = variation.rarity.Initial;
						ryakugou = variation.略号.get略号Full();

						AddDataGridView();
					}


				}


				//ローカル関数
				void AddDataGridView()
				{
					//コンボボックスの初期値は最初に設定しないと変更できない
					int num = dataGridView1.Rows.Add(twincarddata.IsCardNameHave(), card.名前, ryakugou, rarity,
						twincarddata.usercarddata.UserVariationDataList[0].所持フラグ, (twincarddata.getRank(variation)).ToString(),
						twincarddata.usercarddata.同名枚数.ToString());
					dataGridView1.Rows[num].Tag = CardDB.getCard(card.ID); //行のタグにカード情報を埋め込む  cardは変更されてる可能性があるのでCardDBから同じのを持ってくる

					var dgvcell = (DataGridViewImageCell)dataGridView1.Rows[num].Cells["type"];
					dgvcell.Value = getCanvasCardColor(card, dgvcell);

					var quickcell = dataGridView1.Rows[num].Cells["クイック"];

					if (rarity_allnum == 1)    //１枚しか存在しない場合はクイックチェックが可能に
					{
						quickcell.Value = (rarity_havenum == 1);
						quickcell.Tag = variation;
					}
					else
					{
						dataGridView1.Rows[num].Cells["クイック"].Value = null;
						dataGridView1.Rows[num].Cells["クイック"] = new DataGridViewTextBoxCell();  //テキストボックスを消すためにタイプを変更
						dataGridView1.Rows[num].Cells["クイック"].Value = "";
						dataGridView1.Rows[num].Cells["クイック"].ReadOnly = true;
					}

					dataGridView1.Rows[num].Cells["Is同名予備カード枚数十分"].Tag = variation;

					//１枚ならクイックランクセット
					var quickrankcell = (DataGridViewComboBoxCell)dataGridView1.Rows[num].Cells["Qランク"];

					if (rarity_allnum == 1)    //１枚しか存在しない場合はクイックセットが可能に
					{
	//					quickrankcell.ValueMember = ().ToString();
						quickrankcell.Tag = variation;  //Tagに仕込み
					}
					else
					{
						dataGridView1.Rows[num].Cells["Qランク"].Value = null;
						dataGridView1.Rows[num].Cells["Qランク"] = new DataGridViewTextBoxCell();
						dataGridView1.Rows[num].Cells["Qランク"].Value = "";
						dataGridView1.Rows[num].Cells["Qランク"].ReadOnly = true;
						dataGridView1.Rows[num].Cells["Qランク"].Style.BackColor = Color.Gray;
						dataGridView1.Rows[num].Cells["Qランク"].Tag = variation;
					}

					//値段情報あったら書く
					if (rarity_allnum == 1)
					{
						if (variation.KanabellList == null || variation.KanabellList.Count ==0)
						{
							dataGridView1.Rows[num].Cells["Q値段"].Style.BackColor = Color.Gray;
						}
						else if (!(variation.KanabellList[0].Rank == EKanabellRank.不明))
						{
							var pricecell = dataGridView1.Rows[num].Cells["Q値段"];
							pricecell.Value = variation.KanabellList[0].Rank.ToString() + " " + variation.KanabellList[0].Price.ToString();
							pricecell.Tag = variation.KanabellList[0].URL;
						}
					}
					else
					{
						dataGridView1.Rows[num].Cells["Q値段"].Style.BackColor = Color.Gray;
					}

//					UpdateCellColor(num, twincarddata, variation);

					//パックでは二個目の名前を消す
					if (!あいうえお順フラグ && r>0)
					{
						dataGridView1.Rows[num].Cells["名前"].Value = "";
						dataGridView1.Rows[num].Cells["Is同名予備カード枚数十分"].Value = null;
						dataGridView1.Rows[num].Cells["Is同名予備カード枚数十分"] = new DataGridViewTextBoxCell();
						dataGridView1.Rows[num].Cells["Is同名予備カード枚数十分"].Value = "";
						dataGridView1.Rows[num].Cells["Is同名予備カード枚数十分"].ReadOnly = true;
						dataGridView1.Rows[num].Cells["Is同名予備カード枚数十分"].Style.BackColor = Color.Gray;
						dataGridView1.Rows[num].Cells["Is同名予備カード枚数十分"].Tag = variation;
					}
				}

			}

			UpdateDataGridViewColor();

			//DGVの内容物に合わせてサイズを大きくする
			dataGridView1.Size = new Size(dataGridView1.Size.Width, dataGridView1.ColumnHeadersHeight + (dataGridView1.Rows.Count+ 1) * dataGridView1.RowTemplate.Height);


			flowLayoutPanel1.Controls.Clear();
			flowLayoutPanel2.Controls.Clear();
			//ページボタンを追加する
			int CenterButtonNum = 5;
			int MAXPage = (CardDB.CardList.Count+99) / MAX_NUM;

			if(!あいうえお順Flag)
			{
				MAXPage = pagenum;
			}
			//１ページボタン追加
			if (page > CenterButtonNum/2+1)
            {
				AddPageButtons(1,false);
			}
			if (page > CenterButtonNum / 2 + 2)
			{
				AddSpaceLabels();
			}

			//現在のボタン±２個追加
			for (int i = page- CenterButtonNum/2; i <= page +CenterButtonNum/2; i++)
            {
				if (i < 1 || i > MAXPage) continue;
				AddPageButtons(i, i==page);

			}

			//最終ページボタン追加
			if (page < MAXPage - CenterButtonNum / 2 - 1)
			{
				AddSpaceLabels();
			}
			if (page < MAXPage - CenterButtonNum / 2 )
			{
				AddPageButtons(MAXPage,false);
			}


		}


		public void UpdateCellColor(int num,TwinCardData twincarddata, CardVariation variation)
		{
			(int code_havenum, int code_allnum) = twincarddata.getCardHaveNumCodebyCode(variation.略号);  //これ下のレアリティと同じじゃね？？
			(int rarity_havenum, int rarity_allnum) = twincarddata.getCardHaveNumRarity();

			Color c;


			if (code_havenum == 0) c = red;
			else if (code_havenum == code_allnum) c = green;
			else c = yellow;

			dataGridView1.Rows[num].Cells["名前"].Style.BackColor = c;
			dataGridView1.Rows[num].Cells["略号"].Style.BackColor = c;

			if (rarity_havenum == 0) c = red;
			else if (rarity_havenum == rarity_allnum) c = green;
			else c = yellow;

			if(!あいうえお順Flag)
			{
				c = twincarddata.get所持フラグ(variation) == true ? green : red;
			}
			dataGridView1.Rows[num].Cells["レアリティ"].Style.BackColor = c;

			if (dataGridView1.Rows[num].Cells["クイック"].Style.BackColor != Color.Gray)
				dataGridView1.Rows[num].Cells["クイック"].Style.BackColor = c;
			var cell = dataGridView1.Rows[num].Cells["Is同名予備カード枚数十分"];
			cell.Style.BackColor = ((bool)cell.Value) ? green : red;


		}

		//上が個別のセルを変更していたが、よく考えたら全部変える必要あったわってことで代わり
		public void UpdateDataGridViewColor()
		{
			foreach (DataGridViewRow row in dataGridView1.Rows)
			{
				TwinCardData twincarddata = form.getTwinCardData((CardData)row.Tag);
				CardVariation variation = row.Cells["Qランク"].Tag as CardVariation;	//仕込んであったわ
				(int code_havenum, int code_allnum) = twincarddata.getCardHaveNumCodebyCode(variation.略号);  //これ下のレアリティと同じじゃね？？
				(int rarity_havenum, int rarity_allnum) = twincarddata.getCardHaveNumRarity();

				Color c = getColorByHaveNum(code_havenum, code_allnum);
				row.Cells["名前"].Style.BackColor = c;
				row.Cells["略号"].Style.BackColor = c;

				c = getColorByHaveNum(rarity_havenum, rarity_allnum);
				if (!あいうえお順Flag)
				{
					c = twincarddata.get所持フラグ(variation) == true ? green : red;
				}
				row.Cells["レアリティ"].Style.BackColor = c;

				if (row.Cells["クイック"].Style.BackColor != Color.Gray)
					row.Cells["クイック"].Style.BackColor = c;
//				var cell = row.Cells["Is同名予備カード枚数十分"];
//				cell.Style.BackColor = ((bool)cell.Value) ? green : red;


			}
		}

		//havenum == allnumなら緑、havenum==0なら赤、それ以外は黄色を返す
		public Color getColorByHaveNum(int havenum,int allnum)
		{
			if (havenum == 0) return red;
			else if (havenum == allnum) return green;
			else return yellow;
		}

		private Bitmap getCanvasCardColor(CardData card, DataGridViewImageCell cell)
        {
			//カードのタイプから色を決定
			bool ペンデュラム = card.種族.Contains("ペンデュラム");
			bool 通常 = card.種族.Contains("通常");
			bool 効果 = card.種族.Contains("効果");
			bool 儀式 = card.種族.Contains("儀式");
			bool 融合 = card.種族.Contains("融合");
			bool シンクロ = card.種族.Contains("シンクロ");
			bool エクシーズ = card.種族.Contains("エクシーズ");
			bool リンク = card.種族.Contains("リンク");

			bool 魔法 = false;
			bool 罠 = false;

			//どうも空欄でもtrueが返るらしい（魔法罠は種族が空欄）
			if (card.種族 == "")
			{
				string type;
				card.ValuePairs.TryGetValue("効果", out type);
				魔法 = type.Contains("魔法");
				罠 = type.Contains("罠");

				if (魔法 == false && 罠 == false) return null;
			}

			Color MainColor = Color.FromArgb(255, 238, 128);
			Color PendulumColor = Color.FromArgb(64, 244, 95);

			if (魔法)
			{
				MainColor = Color.FromArgb(64, 244, 95);
				ペンデュラム = false;
			}
			else if (罠)
			{
				MainColor = Color.FromArgb(255, 57, 240);
				ペンデュラム = false;
			}
			else if (リンク) MainColor = Color.FromArgb(34, 72, 255);
			else if (エクシーズ) MainColor = Color.FromArgb(0, 0, 0);
			else if (シンクロ) MainColor = Color.FromArgb(255, 255, 255);
			else if (融合) MainColor = Color.FromArgb(173, 91, 255);
			else if (儀式) MainColor = Color.FromArgb(0, 128, 255);
			else if (効果) MainColor = Color.FromArgb(255, 128, 0);
			else if (通常) MainColor = Color.FromArgb(255, 238, 128);

			//描画先とするImageオブジェクトを作成する
			Bitmap canvas = new Bitmap(cell.Size.Width, cell.Size.Height);
			//ImageオブジェクトのGraphicsオブジェクトを作成する
			Graphics g = Graphics.FromImage(canvas);

			//(0,0)の位置に30x10サイズで塗りつぶされた長方形を描画する
			g.FillRectangle(new SolidBrush(MainColor), 0, 0, 30, 10);
			if (ペンデュラム)
				g.FillRectangle(new SolidBrush(PendulumColor), 0, 10, 30, 10);
			else
				g.FillRectangle(new SolidBrush(MainColor), 0, 10, 30, 10);

			//Graphicsオブジェクトのリソースを解放する
			g.Dispose();

			return canvas;
		}

		private void Change所持フラグ(bool flag,CardData card, DataGridViewCellCollection cells)
		{
			/*
			card.所持フラグ = flag;
			if (card.所持フラグ)
			{
				cells["所持状態変更"].Value = "未所持化";
				cells["所持状態変更"].Style.BackColor = Color.FromArgb(255, 128, 128);
			}
			else
			{
				cells["所持状態変更"].Value = "所持化";
				cells["所持状態変更"].Style.BackColor = Color.FromArgb(128, 255, 128);
			}
			cells["所持フラグ"].Value = card.所持フラグ;
			cells["名前"].Style.BackColor = card.所持フラグ == true ? Color.FromArgb(128, 255, 128) : Color.FromArgb(255, 128, 128);
			*/
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			DataGridView dgv = (DataGridView)sender;
			if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
			if (dgv.Columns[e.ColumnIndex].Name == "名前")
            {
				//DGVに埋め込まれてるカードデータを直接使う予定だったが、パックリストによるDGVはデータを削るので、Formから探す
				//				int id = ((CardData)dgv.Rows[e.RowIndex].Tag).ID;
				//				var f = new CardForm(form.CardDB.getCard(id));
				//と思ったがやっぱ簡易所持チェックのことを考えると削らないほうが都合がいい
				var data = (CardData)dgv.Rows[e.RowIndex].Tag;
				var f = new CardForm(data,form, button1.PerformClick);
				f.Show();
            }
			//リンクならカーナベルを開く
			{
				if (dataGridView1.Columns[e.ColumnIndex].Name == $"Q値段")
				{
					string url = (string)dataGridView1[e.ColumnIndex, e.RowIndex].Tag;
					System.Diagnostics.Process.Start(url);
				}
			}

		}

		private void button1_Click(object sender, EventArgs e)
		{
			/*
			DataGridView dgv = dataGridView1;
			for (int i = 0; i < cdb.PackDB.Count; i++)
			{
				var packdata = cdb.PackDB[i];
				if (packdata.Name.Equals(Pack.Name))
				{
					for (int j = 0; j < packdata.CardDB.Count; j++)
					{
						var row = dgv.Rows[j];
						var card = packdata.CardDB[j];
						if (row.Cells["略号"].Value.ToString().Equals(card.get略号Full()) && row.Cells["名前"].Value.ToString().Equals(card.名前) &&
							row.Cells["レアリティ"].Value.ToString().Equals(card.Rare))
						{
							Change所持フラグ(true, card, row.Cells);
						}
					}
					CardDataBase.Save(cdb);
					dataGridView1.Refresh();
					break;
				}
			}
			*/
		}

		private void button2_Click(object sender, EventArgs e)
		{
			/*
			DataGridView dgv = dataGridView1;
			for (int i = 0; i < cdb.PackDB.Count; i++)
			{
				var packdata = cdb.PackDB[i];
				if (packdata.Name.Equals(Pack.Name))
				{
					for (int j = 0; j < packdata.CardDB.Count; j++)
					{
						var row = dgv.Rows[j];
						var card = packdata.CardDB[j];
						if (row.Cells["略号"].Value.ToString().Equals(card.get略号Full()) && row.Cells["名前"].Value.ToString().Equals(card.名前) &&
							row.Cells["レアリティ"].Value.ToString().Equals(card.Rare))
						{
							Change所持フラグ(false, card, row.Cells);
						}
					}
					CardDataBase.Save(cdb);
					dataGridView1.Refresh();
					break;
				}
			}
			*/
		}

		//セルを選択できないように
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
			dataGridView1.ClearSelection();
		}

		//なぜかセルをクリックするとDGVの先頭にスクロールしてしまうので現在のスクロールポジションを返して回避
        protected override Point ScrollToControl(Control activeControl)
        {
			return AutoScrollPosition;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
			Init(CardDB, Pack,form,あいうえお順Flag, Page);
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
			if (dataGridView1.IsCurrentCellDirty)
			{
				//コミットする
				dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
			}

		}

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
			//チェックボックスの列かどうか調べる
			if (e.RowIndex >= 0 && dataGridView1[e.ColumnIndex, e.RowIndex].GetType() == typeof(DataGridViewCheckBoxCell))
			{
				var variation = (CardVariation)dataGridView1[e.ColumnIndex, e.RowIndex].Tag;
				if (variation == null) return;  //init中でvalueを書き換えるときの誤爆を防ぐ苦肉の策

				var twincarddata = form.getTwinCardData((CardData)dataGridView1.Rows[e.RowIndex].Tag);

				if (dataGridView1.Columns[e.ColumnIndex].Name == "クイック")
				{
					twincarddata.set所持フラグ(variation, (bool)dataGridView1[e.ColumnIndex, e.RowIndex].Value);
				}
				/*
				else if(dataGridView1.Columns[e.ColumnIndex].Name == "Is同名予備カード枚数十分")//予備カード所持
				{
					twincarddata.usercarddata.同名枚数 = ((int)dataGridView1[e.ColumnIndex, e.RowIndex].Value);
				}
				*/
				//				UpdateCellColor(e.RowIndex,twincarddata,variation);
				UpdateDataGridViewColor();

			}

		}

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
			System.Diagnostics.Process.Start(Pack.URL);
		}

		public void AddPageButton(int num,FlowLayoutPanel panel,bool NowFlag)
        {
			Button button = new Button();
			button.Text = num.ToString();
			button.Tag = num;
			button.Size = new Size(50,40);
			if(NowFlag)
				button.BackColor = Color.Gray;
			button.Click += pagebutton_Click;
			panel.Controls.Add(button);
        }

		public void AddPageButtons(int num, bool NowFlag)
        {
			AddPageButton(num, flowLayoutPanel1, NowFlag);
			AddPageButton(num, flowLayoutPanel2, NowFlag);
		}

		public void pagebutton_Click(object sender, EventArgs e)
        {
			Button button = (Button)sender;
			Init(CardDB, Pack, form, あいうえお順Flag, (int)button.Tag);
		}

		public void AddSpaceLabel(FlowLayoutPanel panel)
        {
			Label label = new Label();
			label.Text = "・・・";
			label.Anchor = AnchorStyles.None;
			label.Size = new Size(30,20);
			panel.Controls.Add(label);
        }

		public void AddSpaceLabels()
		{
			AddSpaceLabel(flowLayoutPanel1);
			AddSpaceLabel(flowLayoutPanel2);
		}

		private void button2_Click_1(object sender, EventArgs e)
		{
			_ = Program.SaveUserDataAsync();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			var list = new List<int>();
			foreach(var card in PackCardDB.CardList)
			{
				list.Add(card.ID);
			}

			form.データ取得(false, true, list,false);
		}

		//１回のクリックでコンボボックス起動
		private void dataGridView1_CellEnter(object sender,	DataGridViewCellEventArgs e)
		{
			DataGridView dgv = (DataGridView)sender;

			if ((dgv.Columns[e.ColumnIndex].Name == "Qランク" || dgv.Columns[e.ColumnIndex].Name == "Is同名予備カード枚数十分") &&
			   dgv.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn)
			{
				SendKeys.Send("{F4}");
			}
		}

		//ここからコンボボックスの変更を察知
		private DataGridViewComboBoxEditingControl dataGridViewComboBox = null;
		private string CellName = "";
		
		//EditingControlShowingイベントハンドラ
		private void DataGridView1_EditingControlShowing(object sender,
			DataGridViewEditingControlShowingEventArgs e)
		{
			//表示されているコントロールがDataGridViewComboBoxEditingControlか調べる
			if (e.Control is DataGridViewComboBoxEditingControl)
			{
				DataGridView dgv = (DataGridView)sender;

				CellName = dgv.CurrentCell.OwningColumn.Name;
				//該当する列か調べる
				if (CellName == "Qランク" || CellName == "Is同名予備カード枚数十分")
				{
					//編集のために表示されているコントロールを取得
					this.dataGridViewComboBox =
						(DataGridViewComboBoxEditingControl)e.Control;
					//SelectedIndexChangedイベントハンドラを追加
					this.dataGridViewComboBox.SelectedIndexChanged +=
						new EventHandler(dataGridViewComboBox_SelectedIndexChanged);
				}
			}
		}

		//CellEndEditイベントハンドラ
		private void DataGridView1_CellEndEdit(object sender,
			DataGridViewCellEventArgs e)
		{
			//SelectedIndexChangedイベントハンドラを削除
			if (this.dataGridViewComboBox != null)
			{
				this.dataGridViewComboBox.SelectedIndexChanged -=
					new EventHandler(dataGridViewComboBox_SelectedIndexChanged);
				this.dataGridViewComboBox = null;
			}
		}

		//DataGridViewに表示されているコンボボックスの
		//SelectedIndexChangedイベントハンドラ
		private void dataGridViewComboBox_SelectedIndexChanged(object sender,
			EventArgs e)
		{
			//選択されたアイテムを表示
			DataGridViewComboBoxEditingControl cb =
				(DataGridViewComboBoxEditingControl)sender;

			if(CellName == "Qランク")
			{
				EKanabellRank rank;
				if (Program.TryParse((string)cb.EditingControlFormattedValue, out rank) == false)
				{
					Program.WriteLog("EKanabellRankへのキャストエラー　", LogLevel.エラー);
					return;
				}

				var carddata = (CardData)dataGridView1.Rows[cb.EditingControlRowIndex].Tag;
				var twincarddata = form.getTwinCardData(carddata);

				twincarddata.setRank((CardVariation)dataGridView1.Rows[cb.EditingControlRowIndex].Cells[CellName].Tag, rank);
			}
			else if(CellName == "Is同名予備カード枚数十分")
			{
				int num;
				if (int.TryParse((string)cb.EditingControlFormattedValue, out num) == false)
				{
					Program.WriteLog("intへのキャストエラー　", LogLevel.エラー);
					return;
				}

				var twincarddata = form.getTwinCardData((CardData)dataGridView1.Rows[cb.EditingControlRowIndex].Tag);

				twincarddata.usercarddata.同名枚数 = num;

			}
			CellName = "";
		}
	}
}
