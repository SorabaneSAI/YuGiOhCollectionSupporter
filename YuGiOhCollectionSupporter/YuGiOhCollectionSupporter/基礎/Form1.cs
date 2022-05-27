using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YuGiOhCollectionSupporter
{

	public partial class Form1 : Form
	{
		public Config config = new Config();
		public LogForm logform = new LogForm();

		public CardDataBase CardDB = new CardDataBase();

		public List<string> PackList = new List<string>();	//データ取得が終わったかのカウント用
		public List<PackData> PackDataList = new List<PackData>();

		public Form1()
		{
			InitializeComponent();
			config = Config.Load();
			//			webBrowser1.Navigate(config.URL);
			logform.AddLog(String.Format("遊戯王カードコレクションサポーター  バージョン:{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString()), LogLevel.必須項目);
			Program.Load(CardDB.SaveDataPath, ref CardDB);
			formPanel.SetFormPanelLeft(CardDB,this);

			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
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

		public void WriteLog(string txt, LogLevel loglevel, Exception e = null)
        {
            switch (loglevel)
            {
                case LogLevel.情報:
					logform.AddLog(txt, loglevel);
					Program.Log.Debug(txt);
					break;
                case LogLevel.警告:
                    break;
                case LogLevel.エラー:
                    break;
                case LogLevel.必須項目:
					UpdateLabel(txt);
					logform.AddLog(txt, loglevel);
					Program.Log.Info(txt);
					break;
                default:
					Program.Log.Error("不明なloglevel");
					MessageBox.Show("", "不明なloglevel", MessageBoxButtons.OK, MessageBoxIcon.Error);
					break;
            }

		}



		public void UpdateLabel(string txt)
		{
			label1.Invoke(new Action(() =>
			{
				label1.Text = txt;
				label1.Update();
			}));
		}


		private async void データ取得ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var sw = new System.Diagnostics.Stopwatch();
			sw.Start();

			label1.Visible = true;	//左下のラベルをON
			toolStripMenuItem1.Enabled = false;	//設定を触れないように
			データ取得ToolStripMenuItem.Enabled = false;	//このボタンも触れないように


			//公式サイトにアクセスして、全パックを取得する パックのタイプを取得するため必要
			PackDataList = await GetAllPacks.getAllPackDatasAsync(config.getCardListURL(), this);

			CardDB = await GetAllCards.getAllCardsAsync(config,this);


			label1.Visible = false;  //左下のラベル非表示
			toolStripMenuItem1.Enabled = true;	//設定を触れるように
			データ取得ToolStripMenuItem.Enabled = true;  //このボタンも触れるように

			Program.Save(CardDB.SaveDataPath, CardDB);

			sw.Stop();
			TimeSpan ts = sw.Elapsed;
			MessageBox.Show("カード情報の取得が終了しました。\n全カード種類:" + CardDB.getAllCardCount() + $"\nかかった時間:{ts.Hours}時間 {ts.Minutes}分 {ts.Seconds}秒 {ts.Milliseconds}ミリ秒"
				, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

		
		}

		private void ログToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if(logform.Visible == false)
				logform.Show();
			else
				logform.Hide();
		}




		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}

		//クリックしたら右側にパックの内容表示
		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			formPanel.SetFormPanelRight(CardDB, e.Node.Text,this);
		}

        private void あいうえお順ToolStripMenuItem_Click(object sender, EventArgs e)
        {
			パック順ToolStripMenuItem.CheckState = CheckState.Unchecked;
			あいうえお順ToolStripMenuItem.CheckState = CheckState.Indeterminate;


        }

        private void パック順ToolStripMenuItem_Click(object sender, EventArgs e)
        {
			あいうえお順ToolStripMenuItem.CheckState = CheckState.Unchecked;
			パック順ToolStripMenuItem.CheckState = CheckState.Indeterminate;
		}
	}
}
