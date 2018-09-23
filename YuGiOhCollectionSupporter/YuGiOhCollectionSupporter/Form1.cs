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
	public partial class Form1 : Form
	{
		public Config config = new Config();

		public Form1()
		{
			InitializeComponent();
			config = Config.Load();
//			webBrowser1.Navigate(config.URL);
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
			DataGet dataget = new DataGet(config,label1);
			dataget.getData();
		}
	}
}
