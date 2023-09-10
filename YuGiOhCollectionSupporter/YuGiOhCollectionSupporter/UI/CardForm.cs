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
        TwinCardData twincarddata;
        Form1 form; //パックをクリックした時用
        Action 更新したい画面用関数;
        public CardForm(CardData data,Form1 form, Action 更新用関数 = null)
        {
            InitializeComponent();
            Init(data,form);
            更新したい画面用関数 = 更新用関数;

		}

        public void Init(CardData data, Form1 form)
        {
            twincarddata = form.getTwinCardData(data);
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

            checkBox1.Checked = twincarddata.get表示フラグ();

            dataGridView1.Rows.Clear();
            //DGVに略号とレアリティを追加していく
            Color red = Color.FromArgb(255, 128, 128);
            Color yellow = Color.FromArgb(255, 255, 128);
            Color green = Color.FromArgb(128, 255, 128);

            foreach (var variation in twincarddata.carddata.ListVariations)
            {
                {
                    int num = dataGridView1.Rows.Add(variation.略号.get略号Full(), variation.発売パック.Name, variation.rarity.Initial, twincarddata.get所持フラグ(variation));
                    dataGridView1.Rows[num].Cells["パック名"].Tag = variation.発売パック.URL; //タグに情報を埋め込む
                    dataGridView1.Rows[num].Cells["所持"].Tag = variation; //タグに情報を埋め込む
                    dataGridView1.Rows[num].Cells["レアリティ"].ToolTipText = variation.rarity.Name;   //マウスホバーでレアの正式名称

                    Color c;
                    (int havenum, int allnum) = twincarddata.getCardHaveNumCodebyCode(variation.略号);
                    if (havenum == 0) c = red;
                    else if (havenum == allnum) c = green;
                    else c = yellow;

                    dataGridView1.Rows[num].Cells["略号"].Style.BackColor = c;
                    dataGridView1.Rows[num].Cells["パック名"].Style.BackColor = c;

                    if (twincarddata.get所持フラグ(variation)) c = green;
                    else c = red;
                    dataGridView1.Rows[num].Cells["レアリティ"].Style.BackColor = c;
                    dataGridView1.Rows[num].Cells["所持"].Style.BackColor = c;

                    for (int i = 0; i < 3; i++)
                    {
                        //ないとこはグレー
                        if(i >= variation.KanabellList.Count)
                        {
							dataGridView1.Rows[num].Cells[$"値段{i + 1}"].Style.BackColor = Color.Gray;
                            continue;
						}

						var kanabell = variation.KanabellList[i];
						if (kanabell.Rank == EKanabellRank.不明 || kanabell.Rank == EKanabellRank.在庫なし)
						{
							dataGridView1.Rows[num].Cells[$"値段{i+1}"].Style.BackColor = Color.Gray;
						}
						else
						{
							dataGridView1.Rows[num].Cells[$"値段{i + 1}"].Value = kanabell.Rank.ToString() + " " + kanabell.Price.ToString();
						}
					}
				}
			}
            //DGVの内容物に合わせてサイズを大きくする
            dataGridView1.Size = new Size(dataGridView1.Size.Width, dataGridView1.ColumnHeadersHeight + dataGridView1.RowCount * dataGridView1.RowTemplate.Height);
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            twincarddata.set表示フラグ(checkBox1.Checked) ;
            /*
            checkBox1.Enabled = false;
            dataGridView1.Enabled = false;
            await Program.SaveUserDataAsync();
			更新したい画面用関数?.Invoke();
			checkBox1.Enabled = true;
            dataGridView1.Enabled = true;
            */
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(twincarddata.carddata.URL);
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
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //チェックボックスの列かどうか調べる
            if (dataGridView1.Columns[e.ColumnIndex].Name == "所持")
            {
                var variation = (CardVariation)dataGridView1[e.ColumnIndex, e.RowIndex].Tag;
                twincarddata.set所持フラグ(variation,(bool)dataGridView1[e.ColumnIndex, e.RowIndex].Value);
				/*
                checkBox1.Enabled = false;
                dataGridView1.Enabled = false;
                Init(twincarddata.carddata,form);
                await Program.SaveUserDataAsync();
				更新したい画面用関数?.Invoke();
				checkBox1.Enabled = true;
                dataGridView1.Enabled = true;
                */
				Init(twincarddata.carddata, form);
			}
            //リンクならカーナベルを開く
            for (int i = 0; i < 3; i++)
            {
				if (dataGridView1.Columns[e.ColumnIndex].Name == $"値段{i+1}")
				{
					var variation = (CardVariation)dataGridView1[e.ColumnIndex, e.RowIndex].Tag;
                    if(variation?.KanabellList.Count > i)
                    {
                        string URL = variation.KanabellList[i].URL;
						System.Diagnostics.Process.Start(URL);
					}
				}
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
                f.Size = new Size(1000,800);
                f.Text = "パック情報";
 //               f.FormBorderStyle = FormBorderStyle.Sizable;
                f.Show();
            }

        }

        //なぜかセルをクリックするとDGVの先頭にスクロールしてしまうので現在のスクロールポジションを返して回避
        protected override Point ScrollToControl(Control activeControl)
        {
            return AutoScrollPosition;
        }

		private async void CardForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			await Program.SaveUserDataAsync();
			更新したい画面用関数?.Invoke();
		}

		//１回のクリックでコンボボックス起動
		private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
		{
			DataGridView dgv = (DataGridView)sender;

			if (dgv.Columns[e.ColumnIndex].Name == "ランク" &&
			   dgv.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn)
			{
				SendKeys.Send("{F4}");
			}
		}

		//ここからコンボボックスの変更を察知
		private DataGridViewComboBoxEditingControl dataGridViewComboBox = null;

		//EditingControlShowingイベントハンドラ
		private void DataGridView1_EditingControlShowing(object sender,
			DataGridViewEditingControlShowingEventArgs e)
		{
			//表示されているコントロールがDataGridViewComboBoxEditingControlか調べる
			if (e.Control is DataGridViewComboBoxEditingControl)
			{
				DataGridView dgv = (DataGridView)sender;

				//該当する列か調べる
				if (dgv.CurrentCell.OwningColumn.Name == "ランク")
				{
					//編集のために表示されているコントロールを取得
					this.dataGridViewComboBox =
						(DataGridViewComboBoxEditingControl)e.Control;
					//SelectedIndexChangedイベントハンドラを追加
					this.dataGridViewComboBox.SelectedIndexChanged +=
						new EventHandler(dataGridViewComboBox_SelectedIndexChanged);
				}
			}
		}

		//CellEndEditイベントハンドラ
		private void DataGridView1_CellEndEdit(object sender,
			DataGridViewCellEventArgs e)
		{
			//SelectedIndexChangedイベントハンドラを削除
			if (this.dataGridViewComboBox != null)
			{
				this.dataGridViewComboBox.SelectedIndexChanged -=
					new EventHandler(dataGridViewComboBox_SelectedIndexChanged);
				this.dataGridViewComboBox = null;
			}
		}

		//DataGridViewに表示されているコンボボックスの
		//SelectedIndexChangedイベントハンドラ
		private void dataGridViewComboBox_SelectedIndexChanged(object sender,
			EventArgs e)
		{
			//選択されたアイテムを表示
			DataGridViewComboBoxEditingControl cb =
				(DataGridViewComboBoxEditingControl)sender;

			{
				EKanabellRank rank;
				if (Program.TryParse((string)cb.EditingControlFormattedValue, out rank) == false)
				{
					Program.WriteLog("EKanabellRankへのキャストエラー　", LogLevel.エラー);
					return;
				}

				var twincarddata = form.getTwinCardData((CardData)dataGridView1.Rows[cb.EditingControlRowIndex].Tag);

				twincarddata.usercarddata.Rank = rank;
			}

		}

	}
}
