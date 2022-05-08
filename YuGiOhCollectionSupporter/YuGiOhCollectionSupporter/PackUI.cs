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
		CardDataBase cdb;
		PackData Pack;

		public PackUI()
		{
			InitializeComponent();
		}

		public void Init(CardDataBase CardDB,PackData pack)
		{
			/*
			cdb = CardDB;
			Pack = pack;
			label2.Text = pack.Name;
			label1.Text = pack.TypeName;
			label3.Text = pack.getHaveCardNum_Name() + "/" + pack.getAllCardNum_Name() + "枚 ("+ pack.getHaveCardNum_Rare() + "/" + pack.getAllCardNum_Rare() + ")";
			Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
			Dock = DockStyle.Fill;

			for (int i = 0; i < pack.CardDB.Count; i++)
			{
				CardData card = pack.CardDB[i];
				int num = dataGridView1.Rows.Add(card.get略号Full(),card.名前,card.Rare);

				dataGridView1.Rows[num].Cells["所持フラグ"].Value = card.所持フラグ;
				dataGridView1.Rows[num].Cells["名前"].Style.BackColor = card.所持フラグ == true ? Color.FromArgb(128, 255, 128) : Color.FromArgb(255, 128, 128);
				if (card.所持フラグ)
				{
					dataGridView1.Rows[num].Cells["所持状態変更"].Value = "未所持化";
					dataGridView1.Rows[num].Cells["所持状態変更"].Style.BackColor = Color.FromArgb(255, 128, 128);
				}
				else
				{
					dataGridView1.Rows[num].Cells["所持状態変更"].Value = "所持化";
					dataGridView1.Rows[num].Cells["所持状態変更"].Style.BackColor = Color.FromArgb(128, 255, 128);
				}
			}
			*/
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

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			HomeUI homeUI = new HomeUI();
			homeUI.Init(((Form1)ParentForm).CardDB);
			((Form1)ParentForm).splitContainer1.Panel2.Controls.Add(homeUI);
			homeUI.BringToFront();
			homeUI.Dock = DockStyle.Fill;
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
						var row = dgv.Rows[e.RowIndex];
						var card = packdata.CardDB[j];
						if (row.Cells["略号"].Value.ToString().Equals(card.get略号Full()) && row.Cells["名前"].Value.ToString().Equals(card.名前) &&
							row.Cells["レアリティ"].Value.ToString().Equals(card.Rare))
						{
							if ((bool)row.Cells["所持フラグ"].Value == false )
								Change所持フラグ(true, card, row.Cells);
							else
								Change所持フラグ(false, card, row.Cells);
							CardDataBase.Save(cdb);
							dataGridView1.Refresh();
							break;
						}
					}
					break;
				}
			}
				*/
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
	}
}
