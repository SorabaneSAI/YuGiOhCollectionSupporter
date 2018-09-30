﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YuGiOhCollectionSupporter
{
	public enum LogLevel
	{
		全部,情報,警告,エラー,必須項目
	}

	public partial class Form1 : Form
	{
		public Config config = new Config();
		public LogForm logform = new LogForm();

		public Form1()
		{
			InitializeComponent();
			config = Config.Load();
			//			webBrowser1.Navigate(config.URL);
			AddLog(String.Format("遊戯王カードコレクションサポーター  バージョン:{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString()), LogLevel.必須項目);
		}

		//設定を開く
		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			ConfigForm f = new ConfigForm(config);
			f.ShowDialog(this);
			f.Dispose();

			config = Config.Load();
		}

		private void これについてToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutForm f = new AboutForm();
			f.ShowDialog(this);
			f.Dispose();
		}

		private void データ取得ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DataGet dataget = new DataGet(this);
			dataget.getAllData();
		}

		private void ログToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if(logform.Visible == false)
				logform.Show();
			else
				logform.Hide();
		}

		public void AddLog(string text, LogLevel LV)
		{
			logform.dataGridView1.Rows.Add(LV.ToString(),text);
		}
	}
}
