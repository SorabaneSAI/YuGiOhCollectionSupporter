using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 遊戯王カードコレクトサポーター
{
	public partial class ConfigForm : Form
	{
		public ConfigForm(Config cfg)
		{
			InitializeComponent();

			textBox1.Text = cfg.URL;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Config config = new Config();
			config.URL = textBox1.Text;

			Config.Save(config);

			Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}