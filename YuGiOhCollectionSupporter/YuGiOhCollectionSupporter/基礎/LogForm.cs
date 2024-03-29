﻿using System;
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
	public enum LogLevel
	{
		全部, 情報, 警告, エラー, 必須項目
	}
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

		public void AddLog(string text, LogLevel LV)
        {
			lock (dataGridView1)
			{
				dataGridView1.Rows.Add(LV, text);
				//ログに加えるが、現在のログレベル以下なら非表示にする
				int index = dataGridView1.Rows.GetLastRow(DataGridViewElementStates.Visible);
				var row = dataGridView1.Rows[index];
				if ((int)(LogLevel)row.Cells[0].Value < comboBox1.SelectedIndex)
					row.Visible = false;
				dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.GetLastRow(DataGridViewElementStates.Visible);

				//増えてきたら情報ログを消してく
				if (dataGridView1.Rows.Count > 1000)
				{
					for (int i = 0; i < dataGridView1.Rows.Count; i++)
					{
						var r = dataGridView1.Rows[i];
						if ((LogLevel)r.Cells[0].Value == LogLevel.情報)
						{
							dataGridView1.Rows.RemoveAt(i);
							break;
						}
					}
				}
			}
		}

    }
}
