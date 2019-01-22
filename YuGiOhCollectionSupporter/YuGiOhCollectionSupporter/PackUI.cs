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
		public PackUI()
		{
			InitializeComponent();
		}

		public void Init(CardDataBase CardDB,PackData pack)
		{
			label2.Text = pack.Name;
			label1.Text = pack.TypeName;
			label3.Text = pack.getHaveCardNum_Name() + "/" + pack.getAllCardNum_Name() + "枚 ("+ pack.getHaveCardNum_Rare() + "/" + pack.getAllCardNum_Rare() + ")";
			Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
			Dock = DockStyle.Fill;

			for (int i = 0; i < pack.CardDB.Count; i++)
			{
				CardData card = pack.CardDB[i];
				int num = dataGridView1.Rows.Add(card.get略号Full(),card.名前,card.Rare);

				//ボタンの色は変えられない
				dataGridView1.Columns["所持"].DefaultCellStyle.BackColor = Color.FromArgb(128, 255, 128);
				dataGridView1.Columns["未所持"].DefaultCellStyle.BackColor = Color.FromArgb(255, 128, 128);

				DataGridViewButtonCell button1 = (DataGridViewButtonCell)dataGridView1.Rows[num].Cells["所持"];
				button1.Value = "所持";

				DataGridViewButtonCell button2 = (DataGridViewButtonCell)dataGridView1.Rows[num].Cells["未所持"];
				button2.Value = "未所持";

			}

		}
	}
}
