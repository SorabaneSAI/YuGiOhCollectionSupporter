using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YuGiOhCollectionSupporter
{
	public partial class LogForm : Form
	{
		public LogForm()
		{
			InitializeComponent();
		}

		private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			Hide();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			lock (dataGridView1)
			{
				dataGridView1.Rows.Clear();
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			lock (dataGridView1)
			{
				//レベルの低いログを非表示にする
				for (int i = 0; i < dataGridView1.Rows.Count; i++)
				{
					var row = dataGridView1.Rows[i];
					if ((int)(LogLevel)row.Cells[0].Value < comboBox1.SelectedIndex)
						dataGridView1.Rows[i].Visible = false;
					else
						dataGridView1.Rows[i].Visible = true;
				}
			}

		}
	}
}
