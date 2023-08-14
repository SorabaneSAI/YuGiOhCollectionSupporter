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
	public partial class ConfigForm : Form
	{
		public ConfigForm(Config cfg)
		{
			InitializeComponent();

			textBox1.Text = cfg.URL;
			textBox2.Text = cfg.URL2;
			numericUpDown1.Value = cfg.CardID_MIN;
			numericUpDown2.Value = cfg.CardID_MAX;
			checkBox1.Checked = cfg.Is捜索打ち切り;
			numericUpDown3.Value = cfg.捜索打ち切り限界;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Config config = new Config();
			config.URL = textBox1.Text;
			config.URL2 = textBox2.Text;
			config.CardID_MIN = numericUpDown1.Value;
			config.CardID_MAX = numericUpDown2.Value;
			config.Is捜索打ち切り = checkBox1.Checked;
			config.捜索打ち切り限界 = numericUpDown3.Value;

			Config.Save(config);

			Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}