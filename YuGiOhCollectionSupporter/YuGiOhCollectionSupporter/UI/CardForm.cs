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
        Form1 form; //パックをクリックした時用
        public CardForm(CardData data,Form1 form)
        {
            InitializeComponent();
            Init(data,form);
        }

        public void Init(CardData data, Form1 form)
        {
            carddata = data;
            this.form = form;
            linkLabel1.Text = data.名前;
            label1.Text = data.読み;
            label2.Text = data.英語名;

            label3.Text = "";
            foreach (var pair in data.ValuePairs)
            {
                label3.Text += $"[{pair.Key}:{pair.Value}]  ";
            }

            if (data.ペンデュラム効果 == "")
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

            dataGridView1.Rows.Clear();
            //DGVに略号とレアリティを追加していく
            Color red = Color.FromArgb(255, 128, 128);
            Color yellow = Color.FromArgb(255, 255, 128);
            Color green = Color.FromArgb(128, 255, 128);

            foreach (var variation in data.ListVariations)
            {
                foreach (var rarity in variation.ListRarity)
                {
                    int num = dataGridView1.Rows.Add(variation.略号.get略号Full(), variation.発売パック.Name, rarity.Initial, rarity.所持フラグ);
                    dataGridView1.Rows[num].Cells["パック名"].Tag = variation.発売パック.URL; //タグに情報を埋め込む
                    dataGridView1.Rows[num].Cells["所持"].Tag = rarity; //タグに情報を埋め込む
                    dataGridView1.Rows[num].Cells["レアリティ"].ToolTipText = rarity.Name;   //マウスホバーでレアの正式名称

                    Color c;
                    if (variation.getCardNumRarityHave() == 0) c = red;
                    else if (variation.getCardNumRarityHave() == variation.getCardNumRarity()) c = green;
                    else c = yellow;

                    dataGridView1.Rows[num].Cells["略号"].Style.BackColor = c;
                    dataGridView1.Rows[num].Cells["パック名"].Style.BackColor = c;

                    if (rarity.所持フラグ) c = green;
                    else c = red;
                    dataGridView1.Rows[num].Cells["レアリティ"].Style.BackColor = c;
                    dataGridView1.Rows[num].Cells["所持"].Style.BackColor = c;

                }
            }
            //DGVの内容物に合わせてサイズを大きくする
            dataGridView1.Size = new Size(dataGridView1.Size.Width, dataGridView1.ColumnHeadersHeight + dataGridView1.RowCount * dataGridView1.RowTemplate.Height);
        }

        private async void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            carddata.表示フラグ = checkBox1.Checked;

            checkBox1.Enabled = false;
            dataGridView1.Enabled = false;
            await Program.SaveCardDataAsync();
            checkBox1.Enabled = true;
            dataGridView1.Enabled = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(carddata.URL);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        //チェックボックスがクリックされたことを知るためのもの
        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                //コミットする
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        //チェックボックスがクリックされたかを知るその２
        private async void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //チェックボックスの列かどうか調べる
            if (dataGridView1.Columns[e.ColumnIndex].Name == "所持")
            {
                var rarity = (CardData.Rarity)dataGridView1[e.ColumnIndex, e.RowIndex].Tag;
                rarity.所持フラグ = (bool)dataGridView1[e.ColumnIndex, e.RowIndex].Value;

                checkBox1.Enabled = false;
                dataGridView1.Enabled = false;
                Init(carddata,form);
                await Program.SaveCardDataAsync();
                checkBox1.Enabled = true;
                dataGridView1.Enabled = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.Columns[e.ColumnIndex].Name == "パック名")
            {
                string url = (string)dataGridView1[e.ColumnIndex, e.RowIndex].Tag;
                var pack = form.PackDB.SearchPackData(url);
                var cardDB = new CardDataBase();   

                cardDB.CardList = form.CardDB.getPackCardList(pack);


                CardListUI packUI = new CardListUI(cardDB, pack, form,false);
 //               var data = (CardData)dgv.Rows[e.RowIndex].Tag;
                var f = new Form();
                f.Controls.Add(packUI);
                f.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                f.AutoScroll = true;
                f.Size = new Size(700,700);
                f.Text = "パック情報";
 //               f.FormBorderStyle = FormBorderStyle.Sizable;
                f.Show();
            }

        }
    }
}
