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
			this.ログToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.これについてToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label1 = new System.Windows.Forms.Label();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
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
			this.menuStrip1.Size = new System.Drawing.Size(745, 24);
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
			// ログToolStripMenuItem
			// 
			this.ログToolStripMenuItem.Name = "ログToolStripMenuItem";
			this.ログToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.ログToolStripMenuItem.Text = "ログ";
			this.ログToolStripMenuItem.Click += new System.EventHandler(this.ログToolStripMenuItem_Click);
			// 
			// これについてToolStripMenuItem
			// 
			this.これについてToolStripMenuItem.Name = "これについてToolStripMenuItem";
			this.これについてToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
			this.これについてToolStripMenuItem.Text = "これについて";
			this.これについてToolStripMenuItem.Click += new System.EventHandler(this.これについてToolStripMenuItem_Click);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 433);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "待機中";
			this.label1.Visible = false;
			// 
			// splitContainer1
			// 
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer1.Location = new System.Drawing.Point(0, 27);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.AutoScroll = true;
			this.splitContainer1.Panel1.Controls.Add(this.treeView1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.AutoScroll = true;
			this.splitContainer1.Panel2.Controls.Add(this.linkLabel1);
			this.splitContainer1.Size = new System.Drawing.Size(745, 428);
			this.splitContainer1.SplitterDistance = 229;
			this.splitContainer1.TabIndex = 3;
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new System.Drawing.Point(116, 118);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(56, 12);
			this.linkLabel1.TabIndex = 4;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "linkLabel1";
			// 
			// treeView1
			// 
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(225, 424);
			this.treeView1.TabIndex = 5;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(745, 454);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.splitContainer1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form1";
			this.Text = "遊戯王カードコレクトサポーター";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem これについてToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ログToolStripMenuItem;
		public System.Windows.Forms.Label label1;
		public System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		public System.Windows.Forms.ToolStripMenuItem データ取得ToolStripMenuItem;
		public System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.LinkLabel linkLabel1;
		public System.Windows.Forms.TreeView treeView1;
	}
}

