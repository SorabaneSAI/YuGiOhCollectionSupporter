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
            this.パックデータ取得ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.カードデータ取得ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.パックカード両方取得ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ログToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.並び順ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.あいうえお順ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.パック順ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ホームToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.パック分類設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.これについてToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.データ取得ToolStripMenuItem,
            this.ログToolStripMenuItem,
            this.並び順ToolStripMenuItem,
            this.ホームToolStripMenuItem,
            this.パック分類設定ToolStripMenuItem,
            this.これについてToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1113, 24);
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
            this.データ取得ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.パックデータ取得ToolStripMenuItem,
            this.カードデータ取得ToolStripMenuItem,
            this.パックカード両方取得ToolStripMenuItem});
            this.データ取得ToolStripMenuItem.Name = "データ取得ToolStripMenuItem";
            this.データ取得ToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.データ取得ToolStripMenuItem.Text = "データ取得";
            // 
            // パックデータ取得ToolStripMenuItem
            // 
            this.パックデータ取得ToolStripMenuItem.Name = "パックデータ取得ToolStripMenuItem";
            this.パックデータ取得ToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.パックデータ取得ToolStripMenuItem.Text = "パックデータ取得";
            this.パックデータ取得ToolStripMenuItem.Click += new System.EventHandler(this.両方取得ToolStripMenuItem_Click);
            // 
            // カードデータ取得ToolStripMenuItem
            // 
            this.カードデータ取得ToolStripMenuItem.Name = "カードデータ取得ToolStripMenuItem";
            this.カードデータ取得ToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.カードデータ取得ToolStripMenuItem.Text = "カードデータ取得";
            this.カードデータ取得ToolStripMenuItem.Click += new System.EventHandler(this.両方取得ToolStripMenuItem_Click);
            // 
            // パックカード両方取得ToolStripMenuItem
            // 
            this.パックカード両方取得ToolStripMenuItem.Name = "パックカード両方取得ToolStripMenuItem";
            this.パックカード両方取得ToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.パックカード両方取得ToolStripMenuItem.Text = "両方取得";
            this.パックカード両方取得ToolStripMenuItem.Click += new System.EventHandler(this.両方取得ToolStripMenuItem_Click);
            // 
            // ログToolStripMenuItem
            // 
            this.ログToolStripMenuItem.Name = "ログToolStripMenuItem";
            this.ログToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.ログToolStripMenuItem.Text = "ログ";
            this.ログToolStripMenuItem.Click += new System.EventHandler(this.ログToolStripMenuItem_Click);
            // 
            // 並び順ToolStripMenuItem
            // 
            this.並び順ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.あいうえお順ToolStripMenuItem,
            this.パック順ToolStripMenuItem});
            this.並び順ToolStripMenuItem.Name = "並び順ToolStripMenuItem";
            this.並び順ToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.並び順ToolStripMenuItem.Text = "並び順";
            // 
            // あいうえお順ToolStripMenuItem
            // 
            this.あいうえお順ToolStripMenuItem.Name = "あいうえお順ToolStripMenuItem";
            this.あいうえお順ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.あいうえお順ToolStripMenuItem.Text = "あいうえお順";
            this.あいうえお順ToolStripMenuItem.Click += new System.EventHandler(this.あいうえお順ToolStripMenuItem_Click);
            // 
            // パック順ToolStripMenuItem
            // 
            this.パック順ToolStripMenuItem.Name = "パック順ToolStripMenuItem";
            this.パック順ToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.パック順ToolStripMenuItem.Text = "パック順";
            this.パック順ToolStripMenuItem.Click += new System.EventHandler(this.パック順ToolStripMenuItem_Click);
            // 
            // ホームToolStripMenuItem
            // 
            this.ホームToolStripMenuItem.Name = "ホームToolStripMenuItem";
            this.ホームToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.ホームToolStripMenuItem.Text = "ホーム";
            this.ホームToolStripMenuItem.Click += new System.EventHandler(this.ホームToolStripMenuItem_Click);
            // 
            // パック分類設定ToolStripMenuItem
            // 
            this.パック分類設定ToolStripMenuItem.Name = "パック分類設定ToolStripMenuItem";
            this.パック分類設定ToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.パック分類設定ToolStripMenuItem.Text = "パック分類設定";
            this.パック分類設定ToolStripMenuItem.Click += new System.EventHandler(this.パック分類設定ToolStripMenuItem_Click);
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
            this.label1.Location = new System.Drawing.Point(12, 642);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "待機中";
            this.label1.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Size = new System.Drawing.Size(1113, 635);
            this.splitContainer1.SplitterDistance = 370;
            this.splitContainer1.TabIndex = 3;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(366, 631);
            this.treeView1.TabIndex = 5;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 663);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.splitContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "遊戯王カードコレクトサポーター";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
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
		public System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripMenuItem 並び順ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem あいうえお順ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem パック順ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem パックデータ取得ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem カードデータ取得ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem パックカード両方取得ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ホームToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem パック分類設定ToolStripMenuItem;
    }
}

