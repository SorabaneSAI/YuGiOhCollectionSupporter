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
	public partial class PackUI : UserControl
	{
		CardDataBase CardDB;
		PackData Pack;

		public PackUI(TreeNode treenode, Form1 form)
		{
			InitializeComponent();
			Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
			Dock = DockStyle.Fill;	//これやばいらしい？
			Init(treenode, form);
		}

		public void Init(TreeNode treenode,Form1 form)
		{
			
			if (form.あいうえお順ToolStripMenuItem.CheckState == CheckState.Indeterminate)
			{
				CardDB = ((TreeNodeAIUEOTag)treenode.Tag).CardDB;
				Pack = null;
				linkLabel1.Visible = false;
				label1.Visible = false;
				label4.Visible = false;
			}
			else if (form.パック順ToolStripMenuItem.CheckState == CheckState.Indeterminate)
			{
				Pack = null;
				linkLabel1.Text = Pack.Name;
				label1.Text = Pack.TypeName;
				label4.Text = Pack.SeriesName;
			}

			tableLayoutPanel1.Controls.Add(new CollectDataUI(CardDB), 0, 3);

			Color red = Color.FromArgb(255, 128, 128);
			Color yellow = Color.FromArgb(255, 255, 128);
			Color green = Color.FromArgb(128, 255, 128);


			foreach (var card in CardDB.CardList)
            {
				int num = dataGridView1.Rows.Add(card.IsCardNameHave(), card.名前, $"{card.getCardNumCodeHave()} / {card.getCardNumCode()}", $"{card.getCardNumRarityHave()} / {card.getCardNumRarity()}");
				dataGridView1.Rows[num].Tag = card; //行のタグにカード情報を埋め込む
				dataGridView1.Rows[num].Cells["名前"].Style.BackColor = card.IsCardNameHave() ? green : red;
				Color c;
				if (card.getCardNumCodeHave() == 0) c = red;
				else if (card.getCardNumCodeHave() == card.getCardNumCode()) c = green;
				else c = yellow;

				dataGridView1.Rows[num].Cells["略号"].Style.BackColor = c;

				if (card.getCardNumRarityHave() == 0) c = red;
				else if (card.getCardNumRarityHave() == card.getCardNumRarity()) c = green;
				else c = yellow;

				dataGridView1.Rows[num].Cells["レアリティ"].Style.BackColor = c;

				//カードのタイプから色を決定

				var dgvcell = (DataGridViewImageCell)dataGridView1.Rows[num].Cells["type"];
				//描画先とするImageオブジェクトを作成する
				Bitmap canvas = new Bitmap(dgvcell.Size.Width, dgvcell.Size.Height);
				//ImageオブジェクトのGraphicsオブジェクトを作成する
				Graphics g = Graphics.FromImage(canvas);

				//(0,0)の位置に30x10サイズで塗りつぶされた長方形を描画する
				g.FillRectangle(Brushes.Red, 0, 0, 20, 20);

				//Graphicsオブジェクトのリソースを解放する
				g.Dispose();

				dgvcell.Value = canvas;
			}

			dataGridView1.Size = new Size(dataGridView1.Size.Width, dataGridView1.ColumnHeadersHeight + CardDB.CardList.Count * dataGridView1.RowTemplate.Height);

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
				var data = (CardData)dgv.Rows[e.RowIndex].Tag;
				var form = new CardForm(data);
				form.Show();
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
			dataGridView1.ClearSelection();
		}

		//なぜかセルをクリックするとDGVの先頭にスクロールしてしまうので現在のスクロールポジションを返して回避
        protected override Point ScrollToControl(Control activeControl)
        {
			return AutoScrollPosition;
        }
    }
}
