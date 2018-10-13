using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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
		public bool ProgramEndFlag = false;

		public CardDataBase CardDB = new CardDataBase();

		public List<string> PackList = new List<string>();	//データ取得が終わったかのカウント用
		public List<PackData> PackDataList = new List<PackData>();
		public bool PackGotFlag = false;

		public Form1()
		{
			InitializeComponent();
			config = Config.Load();
			//			webBrowser1.Navigate(config.URL);
			AddLog(String.Format("遊戯王カードコレクションサポーター  バージョン:{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString()), LogLevel.必須項目);
			CardDB = CardDataBase.Load();
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

			PackList.Clear();
			PackDataList.Clear();
			PackGotFlag = false;
			//超重いので別の処理
			//			Task.Run(() =>	 dataget.getAllData());
			dataget.getAllData();
			getCardData();
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
			lock (logform.dataGridView1)
			{
				logform.dataGridView1.Rows.Add(LV, text);
				//ログに加えるが、現在のログレベル以下なら非表示にする
				int index = logform.dataGridView1.Rows.GetLastRow(DataGridViewElementStates.Visible);
				var row = logform.dataGridView1.Rows[index];
				if ((int)(LogLevel)row.Cells[0].Value < logform.comboBox1.SelectedIndex)
					row.Visible = false;
				logform.dataGridView1.FirstDisplayedScrollingRowIndex = logform.dataGridView1.Rows.GetLastRow(DataGridViewElementStates.Visible);

				//増えてきたら情報ログを消してく
				if (logform.dataGridView1.Rows.Count > 1000)
				{
					for (int i=0; i< logform.dataGridView1.Rows.Count; i++ )
					{
						var r = logform.dataGridView1.Rows[i];
						if ((LogLevel)r.Cells[0].Value == LogLevel.情報)
							logform.dataGridView1.Rows.RemoveAt(i);
					}
				}
			}

		}

		public void AddPack(string name)
		{
			lock (PackList)
			{
				//重複チェック
				foreach (var item in PackList)
				{
					if (item.Equals(name))
						return;
				}
				PackList.Add(name);
			}
		}
		public void DeletePack(string name)
		{
			lock (PackList)
			{
				//重複チェック
				foreach (var item in PackList)
				{
					if (item.Equals(name))
					{
						PackList.Remove(item);
						return;
					}
				}
				AddLog("処理済みのパックを処理",LogLevel.警告);
			}
		}

		public async void getCardData()
		{
			CardDataBase cdb = new CardDataBase();
			while (PackGotFlag == false)
			{
				await Task.Delay(1000);
			}
			foreach (var pack in PackDataList)
			{
				PackList.Add(pack.Name);
				getPackData(pack.URL, pack.Name, pack.TypeName, pack.SeriesName,cdb);

				label1.Text = "残りパック数:" + PackList.Count;
			}
			while (PackList.Count >0)
			{
				await Task.Delay(1000);
			}

			label1.Visible = false;  //最後に非表示
			toolStripMenuItem1.Enabled = true;
			データ取得ToolStripMenuItem.Enabled = true;

			CardDB = cdb;
			CardDataBase.Save(CardDB);
			MessageBox.Show("カード情報の取得が終了しました。\n全カード種類:" +CardDB.getAllCardCount(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		//パックのページに移動し、カードのリンクを得る
		public void getPackData(string PackURL, string PackName, string PackType, string SeriesName, CardDataBase cdb)
		{
			if (ProgramEndFlag == true)
				return;

			Invoke(new Action(() =>
			{
				AddPack(PackName);
				AddLog(PackName + "に接続 :" + PackURL, LogLevel.情報);
			}));

			CardGet card = new CardGet(PackName, cdb, this);
			card.Navigate(PackURL);
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
			ProgramEndFlag = true;
		}
	}
}
