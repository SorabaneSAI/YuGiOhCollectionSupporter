using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YuGiOhCollectionSupporter.UI
{
	public partial class HomePDFUI : UserControl
	{
		Form1 form1;

		public HomePDFUI(Form1 form)
		{
			InitializeComponent();

			form1 = form;

			comboBox1.SelectedIndex = 2;
//			comboBox2.SelectedIndex = 0;
			comboBox3.SelectedIndex = 3;
//			comboBox4.SelectedIndex = 1;

		}

		private void button1_Click(object sender, EventArgs e)
		{
			button1.Tag = this;

			form1.button_Click(sender, e);
		}
	}
}
