﻿using System;
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
		public PackDataBase PackDB = new PackDataBase();

		public Form1()
		{
			InitializeComponent();

			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			config = Config.Load();
			Program.WriteLog(String.Format("遊戯王カードコレクションサポーター  バージョン:{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString()), LogLevel.必須項目);
			Program.Load(CardDB.SaveDataPath, ref CardDB);
			Program.Load(PackDB.SaveDataPath, ref PackDB);
			formPanel.ShowHome(this);

			//			あいうえお順ToolStripMenuItem.CheckState = CheckState.Indeterminate;
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


		public void UpdateLabel(string txt)
		{
			label1.Invoke(new Action(() =>
			{
				label1.Text = txt;
				label1.Update();
			}));
		}



		private void ログToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (logform.Visible == false)
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
			formPanel.SetFormPanelRight(e.Node, this);
		}

		private void あいうえお順ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			パック順ToolStripMenuItem.CheckState = CheckState.Unchecked;
			あいうえお順ToolStripMenuItem.CheckState = CheckState.Indeterminate;

			formPanel.SetFormPanelLeft( this);
		}

		private void パック順ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			あいうえお順ToolStripMenuItem.CheckState = CheckState.Unchecked;
			パック順ToolStripMenuItem.CheckState = CheckState.Indeterminate;

			formPanel.SetFormPanelLeft(this);
		}


		private async void 両方取得ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var menuitem = (ToolStripMenuItem)sender;
			bool IsPackSearch = false;
			bool IsCardSearch = false;
			if (menuitem.Name.Contains("パック")) IsPackSearch = true;
			if (menuitem.Name.Contains("カード")) IsCardSearch = true;

			int 推測探索秒 = 0;
			if (IsPackSearch) 推測探索秒 += 1800;
			if (IsCardSearch) 推測探索秒 += (int)(config.CardID_MAX - config.CardID_MIN);

			//メッセージボックスを表示する
			DialogResult result = MessageBox.Show($"捜索には{推測探索秒 / 60}分程度かかることが予測されます。\n本当に始めますか？", "",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Exclamation,
				MessageBoxDefaultButton.Button2);

			if (result == DialogResult.No) return;

			var sw = new System.Diagnostics.Stopwatch();
			sw.Start();

			InvalidMenuItem();

			string ans = "";
			List<string> ErrorList = new List<string>();

			//breakで抜けるためのdo-while(false)
			do
			{

				if (IsPackSearch)
				{
					//公式サイトにアクセスして、全パックを取得する パックのタイプを取得するため必要
					var newdatalist = await GetAllPacks.getAllPackDatasAsync(config.URL, this);
					if (newdatalist == null) break;

					//新しいデータを追加し、古いデータは上書きする
					(int newnum, int updatenum) = PackDB.AddPackDataList(newdatalist);


					Program.SavePackData();

					ans += "パックの情報の取得が完了しました。\n全パック種類:" + PackDB.PackDataList.Count + $"\nうち{newnum}件が新しいデータとして登録され、{updatenum}件が更新されました。\n";
				}

				if (IsCardSearch)
				{
					var newcardDB = await GetAllCards.getAllCardsAsync(config, this, ErrorList);

					if (newcardDB == null) break;

					//新しいデータを追加し、古いデータは上書きする
					(int newnum, int updatenum) tmp = CardDB.AddCardDataList(newcardDB.CardList);

					Program.SaveCardData();

					ans += "カード情報の取得が終了しました。\n全カード種類:" + CardDB.getAllCardNum() +
						$"\nうち{tmp.newnum}件が新しいデータとして登録され、\n{tmp.updatenum}件が更新されました。\n";
				}

			} while (false);


			ValidMenuItem();

			sw.Stop();
			TimeSpan ts = sw.Elapsed;

			string msg = ans + $"エラー件数:{ErrorList.Count}件\n" + Program.ToJson(ErrorList, Newtonsoft.Json.Formatting.None) + $"\nかかった時間:{ts.Hours}時間 {ts.Minutes}分 {ts.Seconds}秒 {ts.Milliseconds}ミリ秒";
			Program.WriteLog(msg, LogLevel.必須項目);
			MessageBox.Show(msg
				, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}


		public void InvalidMenuItem()
		{
			Invoke(new Action(() =>
			{
				label1.Visible = true;  //左下のラベルをON
				toolStripMenuItem1.Enabled = false; //設定を触れないように
				データ取得ToolStripMenuItem.Enabled = false; //このボタンも触れないように
				並び順ToolStripMenuItem.Enabled = false;
			}));

		}

		public void ValidMenuItem()
		{
			Invoke(new Action(() =>
			{
				label1.Visible = false;  //左下のラベル非表示
				toolStripMenuItem1.Enabled = true;  //設定を触れるように
				データ取得ToolStripMenuItem.Enabled = true;  //このボタンも触れるように
				並び順ToolStripMenuItem.Enabled = true;
			}));

		}

        private void ホームToolStripMenuItem_Click(object sender, EventArgs e)
        {
			formPanel.ShowHome(this);
        }

	}
}
