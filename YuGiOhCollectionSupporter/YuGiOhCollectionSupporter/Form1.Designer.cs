namespace YuGiOhCollectionSupporter
{
	partial class Form1
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.データ取得ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.これについてToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.webBrowser1 = new System.Windows.Forms.WebBrowser();
			this.label1 = new System.Windows.Forms.Label();
			this.ログToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.データ取得ToolStripMenuItem,
            this.ログToolStripMenuItem,
            this.これについてToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(563, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(43, 20);
			this.toolStripMenuItem1.Text = "設定";
			this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
			// 
			// データ取得ToolStripMenuItem
			// 
			this.データ取得ToolStripMenuItem.Name = "データ取得ToolStripMenuItem";
			this.データ取得ToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
			this.データ取得ToolStripMenuItem.Text = "データ取得";
			this.データ取得ToolStripMenuItem.Click += new System.EventHandler(this.データ取得ToolStripMenuItem_Click);
			// 
			// これについてToolStripMenuItem
			// 
			this.これについてToolStripMenuItem.Name = "これについてToolStripMenuItem";
			this.これについてToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
			this.これについてToolStripMenuItem.Text = "これについて";
			this.これについてToolStripMenuItem.Click += new System.EventHandler(this.これについてToolStripMenuItem_Click);
			// 
			// webBrowser1
			// 
			this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Top;
			this.webBrowser1.Location = new System.Drawing.Point(0, 24);
			this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser1.Name = "webBrowser1";
			this.webBrowser1.ScriptErrorsSuppressed = true;
			this.webBrowser1.Size = new System.Drawing.Size(563, 181);
			this.webBrowser1.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 426);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "待機中";
			this.label1.Visible = false;
			// 
			// ログToolStripMenuItem
			// 
			this.ログToolStripMenuItem.Name = "ログToolStripMenuItem";
			this.ログToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.ログToolStripMenuItem.Text = "ログ";
			this.ログToolStripMenuItem.Click += new System.EventHandler(this.ログToolStripMenuItem_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(563, 447);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.webBrowser1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form1";
			this.Text = "遊戯王カードコレクトサポーター";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem これについてToolStripMenuItem;
		private System.Windows.Forms.WebBrowser webBrowser1;
		private System.Windows.Forms.ToolStripMenuItem データ取得ToolStripMenuItem;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ToolStripMenuItem ログToolStripMenuItem;
	}
}

