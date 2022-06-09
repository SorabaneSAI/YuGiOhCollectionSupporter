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
    public partial class CardForm : Form
    {
        CardData carddata;
        public CardForm(CardData data)
        {
            InitializeComponent();
            carddata = data;
            linkLabel1.Text = data.名前;
            label1.Text = data.読み;
            label2.Text = data.英語名;

            foreach (var pair in data.ValuePairs)
            {
                label3.Text += $"[{pair.Key}:{pair.Value}]  ";
            }

            if(data.ペンデュラム効果 == "")
            {
                flowLayoutPanel1.Controls.Remove(label4);
                flowLayoutPanel1.Controls.Remove(textBox1);
            }
            else
            {
                textBox1.Text = data.ペンデュラム効果;
            }

            textBox2.Text = data.テキスト;

            label6.Text = data.種族;

            checkBox1.Checked = data.表示フラグ;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            carddata.表示フラグ = checkBox1.Checked;
            Program.SaveCardData();
        }
    }
}
