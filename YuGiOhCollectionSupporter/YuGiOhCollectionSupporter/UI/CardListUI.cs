using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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


			Color red = Color.FromArgb(255, 128, 128);
			Color yellow = Color.FromArgb(255, 255, 128);
			Color green = Color.FromArgb(128, 255, 128);

			dataGridView1.Rows.Clear();
			for (int i=(page-1)* MAX_NUM; i< (page-1)* MAX_NUM + MAX_NUM; i++ )
            {
				if (i >= PackCardDB.CardList.Count) break;
				var card = PackCardDB.CardList[i];
				var twincarddata = form.getTwinCardData(card);
				string ryakugou = "";
				string rarity = "";

				(int rarity_havenum, int rarity_allnum) = twincarddata.getCardHaveNumRarity();
				(int code_havenum, int code_allnum) = twincarddata.getCardHaveNumCode();
				//１つしか存在しない略号、レアリティは文字にする
				if (rarity_allnum == 1)   
					rarity = card.ListVariations[0].rarity.Initial;
				else
					rarity = $"{rarity_havenum} / {rarity_allnum}";
				if(code_allnum == 1)
					ryakugou = card.ListVariations[0].略号.get略号Full();
				else
					ryakugou = $"{code_havenum} / {code_allnum}";


				int num = dataGridView1.Rows.Add(twincarddata.IsCardNameHave(), card.名前, ryakugou, rarity, twincarddata.usercarddata.UserVariationDataList[0].所持フラグ);
				dataGridView1.Rows[num].Tag = CardDB.getCard(card.ID); //行のタグにカード情報を埋め込む  cardは変更されてる可能性があるのでCardDBから同じのを持ってくる
				dataGridView1.Rows[num].Cells["名前"].Style.BackColor = twincarddata.IsCardNameHave() ? green : red;
				Color c;
				if (code_havenum == 0) c = red;
				else if (code_havenum == code_allnum) c = green;
				else c = yellow;

				dataGridView1.Rows[num].Cells["略号"].Style.BackColor = c;

				if (rarity_havenum == 0) c = red;
				else if (rarity_havenum == rarity_allnum) c = green;
				else c = yellow;

				dataGridView1.Rows[num].Cells["レアリティ"].Style.BackColor = c;

				var dgvcell = (DataGridViewImageCell)dataGridView1.Rows[num].Cells["type"];
				dgvcell.Value = getCanvasCardColor(card, dgvcell);

				var quickcell = dataGridView1.Rows[num].Cells["クイック"];

				if (rarity_allnum == 1)    //１枚しか存在しない場合はクイックチェックが可能に
                {
					quickcell.Value = (rarity_havenum == 1);
					quickcell.Tag = card.ListVariations[0];
					quickcell.Style.BackColor = c;
				}
				else
                {
					dataGridView1.Rows[num].Cells["クイック"].Value = null;
					dataGridView1.Rows[num].Cells["クイック"] = new DataGridViewTextBoxCell();  //テキストボックスを消すためにタイプを変更
					dataGridView1.Rows[num].Cells["クイック"].Value = "";
					dataGridView1.Rows[num].Cells["クイック"].ReadOnly = true;
					dataGridView1.Rows[num].Cells["クイック"].Style.BackColor = Color.FromArgb(171, 171, 173);
				}
			}

			//DGVの内容物に合わせてサイズを大きくする
			dataGridView1.Size = new Size(dataGridView1.Size.Width, dataGridView1.ColumnHeadersHeight + Math.Min(CardDB.CardList.Count, MAX_NUM) * dataGridView1.RowTemplate.Height);


			flowLayoutPanel1.Controls.Clear();
			flowLayoutPanel2.Controls.Clear();
			//ページボタンを追加する
			int CenterButtonNum = 5;
			int MAXPage = (CardDB.CardList.Count+99) / MAX_NUM;

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
			if (dgv.Columns[e.ColumnIndex].Name == "名前")
            {
				//DGVに埋め込まれてるカードデータを直接使う予定だったが、パックリストによるDGVはデータを削るので、Formから探す
				//				int id = ((CardData)dgv.Rows[e.RowIndex].Tag).ID;
				//				var f = new CardForm(form.CardDB.getCard(id));
				//と思ったがやっぱ簡易所持チェックのことを考えると削らないほうが都合がいい
				var data = (CardData)dgv.Rows[e.RowIndex].Tag;
				var f = new CardForm(data,form);
				f.Show();
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

        private async void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
			//チェックボックスの列かどうか調べる
			if (dataGridView1.Columns[e.ColumnIndex].Name == "クイック" && dataGridView1[e.ColumnIndex, e.RowIndex].GetType() == typeof(DataGridViewCheckBoxCell))
			{
				var variation = (CardVariation)dataGridView1[e.ColumnIndex, e.RowIndex].Tag;
				if (variation == null) return;  //init中でvalueを書き換えるときの誤爆を防ぐ苦肉の策

				var twincarddata = form.getTwinCardData((CardData)dataGridView1.Rows[e.RowIndex].Tag);
				twincarddata.set所持フラグ(variation, (bool)dataGridView1[e.ColumnIndex, e.RowIndex].Value);

				dataGridView1.Enabled = false;
				Init(CardDB, Pack, form,あいうえお順Flag,Page);
				await Program.SaveUserDataAsync();
				dataGridView1.Enabled = true;
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

	}
}
