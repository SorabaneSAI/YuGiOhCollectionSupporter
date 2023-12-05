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
			this.新しいパックと新しいカード取得ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ログToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ホームToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.パック分類設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.シリーズ期設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.販売価格調査ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.評価調査ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.これについてToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label1 = new System.Windows.Forms.Label();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.panel1 = new System.Windows.Forms.Panel();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.button3 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.treeView2 = new System.Windows.Forms.TreeView();
			this.button4 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.AutoSize = false;
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.データ取得ToolStripMenuItem,
            this.ログToolStripMenuItem,
            this.ホームToolStripMenuItem,
            this.パック分類設定ToolStripMenuItem,
            this.シリーズ期設定ToolStripMenuItem,
            this.販売価格調査ToolStripMenuItem,
            this.評価調査ToolStripMenuItem,
            this.これについてToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(1512, 30);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(53, 26);
			this.toolStripMenuItem1.Text = "設定";
			this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
			// 
			// データ取得ToolStripMenuItem
			// 
			this.データ取得ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.パックデータ取得ToolStripMenuItem,
            this.カードデータ取得ToolStripMenuItem,
            this.パックカード両方取得ToolStripMenuItem,
            this.新しいパックと新しいカード取得ToolStripMenuItem});
			this.データ取得ToolStripMenuItem.Name = "データ取得ToolStripMenuItem";
			this.データ取得ToolStripMenuItem.Size = new System.Drawing.Size(86, 26);
			this.データ取得ToolStripMenuItem.Text = "データ取得";
			// 
			// パックデータ取得ToolStripMenuItem
			// 
			this.パックデータ取得ToolStripMenuItem.Name = "パックデータ取得ToolStripMenuItem";
			this.パックデータ取得ToolStripMenuItem.Size = new System.Drawing.Size(272, 26);
			this.パックデータ取得ToolStripMenuItem.Text = "パックデータ取得";
			this.パックデータ取得ToolStripMenuItem.Click += new System.EventHandler(this.パックデータ取得ToolStripMenuItem_Click);
			// 
			// カードデータ取得ToolStripMenuItem
			// 
			this.カードデータ取得ToolStripMenuItem.Name = "カードデータ取得ToolStripMenuItem";
			this.カードデータ取得ToolStripMenuItem.Size = new System.Drawing.Size(272, 26);
			this.カードデータ取得ToolStripMenuItem.Text = "カードデータ取得";
			this.カードデータ取得ToolStripMenuItem.Click += new System.EventHandler(this.カードデータ取得ToolStripMenuItem_Click);
			// 
			// パックカード両方取得ToolStripMenuItem
			// 
			this.パックカード両方取得ToolStripMenuItem.Name = "パックカード両方取得ToolStripMenuItem";
			this.パックカード両方取得ToolStripMenuItem.Size = new System.Drawing.Size(272, 26);
			this.パックカード両方取得ToolStripMenuItem.Text = "両方取得";
			this.パックカード両方取得ToolStripMenuItem.Click += new System.EventHandler(this.両方取得ToolStripMenuItem_Click);
			// 
			// 新しいパックと新しいカード取得ToolStripMenuItem
			// 
			this.新しいパックと新しいカード取得ToolStripMenuItem.Name = "新しいパックと新しいカード取得ToolStripMenuItem";
			this.新しいパックと新しいカード取得ToolStripMenuItem.Size = new System.Drawing.Size(272, 26);
			this.新しいパックと新しいカード取得ToolStripMenuItem.Text = "新しいパックと新しいカード取得";
			this.新しいパックと新しいカード取得ToolStripMenuItem.Click += new System.EventHandler(this.新しいパックと新しいカード取得ToolStripMenuItem_Click);
			// 
			// ログToolStripMenuItem
			// 
			this.ログToolStripMenuItem.Name = "ログToolStripMenuItem";
			this.ログToolStripMenuItem.Size = new System.Drawing.Size(46, 26);
			this.ログToolStripMenuItem.Text = "ログ";
			this.ログToolStripMenuItem.Click += new System.EventHandler(this.ログToolStripMenuItem_Click);
			// 
			// ホームToolStripMenuItem
			// 
			this.ホームToolStripMenuItem.Name = "ホームToolStripMenuItem";
			this.ホームToolStripMenuItem.Size = new System.Drawing.Size(57, 26);
			this.ホームToolStripMenuItem.Text = "ホーム";
			this.ホームToolStripMenuItem.Click += new System.EventHandler(this.ホームToolStripMenuItem_Click);
			// 
			// パック分類設定ToolStripMenuItem
			// 
			this.パック分類設定ToolStripMenuItem.Name = "パック分類設定ToolStripMenuItem";
			this.パック分類設定ToolStripMenuItem.Size = new System.Drawing.Size(116, 26);
			this.パック分類設定ToolStripMenuItem.Text = "パック分類設定";
			this.パック分類設定ToolStripMenuItem.Click += new System.EventHandler(this.パック分類設定ToolStripMenuItem_Click);
			// 
			// シリーズ期設定ToolStripMenuItem
			// 
			this.シリーズ期設定ToolStripMenuItem.Name = "シリーズ期設定ToolStripMenuItem";
			this.シリーズ期設定ToolStripMenuItem.Size = new System.Drawing.Size(143, 26);
			this.シリーズ期設定ToolStripMenuItem.Text = "シリーズ（期）設定";
			this.シリーズ期設定ToolStripMenuItem.Click += new System.EventHandler(this.シリーズ期設定ToolStripMenuItem_Click);
			// 
			// 販売価格調査ToolStripMenuItem
			// 
			this.販売価格調査ToolStripMenuItem.Name = "販売価格調査ToolStripMenuItem";
			this.販売価格調査ToolStripMenuItem.Size = new System.Drawing.Size(113, 26);
			this.販売価格調査ToolStripMenuItem.Text = "販売価格調査";
			this.販売価格調査ToolStripMenuItem.Click += new System.EventHandler(this.販売価格調査ToolStripMenuItem_Click);
			// 
			// 評価調査ToolStripMenuItem
			// 
			this.評価調査ToolStripMenuItem.Name = "評価調査ToolStripMenuItem";
			this.評価調査ToolStripMenuItem.Size = new System.Drawing.Size(83, 26);
			this.評価調査ToolStripMenuItem.Text = "評価調査";
			this.評価調査ToolStripMenuItem.Click += new System.EventHandler(this.評価調査ToolStripMenuItem_Click);
			// 
			// これについてToolStripMenuItem
			// 
			this.これについてToolStripMenuItem.Name = "これについてToolStripMenuItem";
			this.これについてToolStripMenuItem.Size = new System.Drawing.Size(93, 26);
			this.これについてToolStripMenuItem.Text = "これについて";
			this.これについてToolStripMenuItem.Click += new System.EventHandler(this.これについてToolStripMenuItem_Click);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 802);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 15);
			this.label1.TabIndex = 2;
			this.label1.Text = "待機中";
			this.label1.Visible = false;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 35);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 31, 4, 4);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.AutoScroll = true;
			this.splitContainer1.Size = new System.Drawing.Size(1512, 794);
			this.splitContainer1.SplitterDistance = 481;
			this.splitContainer1.SplitterWidth = 5;
			this.splitContainer1.TabIndex = 3;
			// 
			// splitContainer2
			// 
			this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Margin = new System.Windows.Forms.Padding(2);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.AutoScroll = true;
			this.splitContainer2.Panel1.Controls.Add(this.panel1);
			this.splitContainer2.Panel1.Controls.Add(this.button3);
			this.splitContainer2.Panel1.Controls.Add(this.button1);
			this.splitContainer2.Panel1.Controls.Add(this.textBox1);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.AutoScroll = true;
			this.splitContainer2.Panel2.Controls.Add(this.panel2);
			this.splitContainer2.Panel2.Controls.Add(this.button4);
			this.splitContainer2.Panel2.Controls.Add(this.button2);
			this.splitContainer2.Panel2.Controls.Add(this.textBox2);
			this.splitContainer2.Size = new System.Drawing.Size(481, 794);
			this.splitContainer2.SplitterDistance = 224;
			this.splitContainer2.TabIndex = 6;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.treeView1);
			this.panel1.Location = new System.Drawing.Point(0, 43);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(220, 747);
			this.panel1.TabIndex = 4;
			// 
			// treeView1
			// 
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Margin = new System.Windows.Forms.Padding(4);
			this.treeView1.MinimumSize = new System.Drawing.Size(29, 4);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(220, 747);
			this.treeView1.TabIndex = 5;
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button3.Location = new System.Drawing.Point(173, 4);
			this.button3.Margin = new System.Windows.Forms.Padding(2);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(46, 34);
			this.button3.TabIndex = 8;
			this.button3.Text = "更新";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(115, 4);
			this.button1.Margin = new System.Windows.Forms.Padding(2);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(50, 34);
			this.button1.TabIndex = 7;
			this.button1.Text = "検索";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(4, 9);
			this.textBox1.Margin = new System.Windows.Forms.Padding(2);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(105, 22);
			this.textBox1.TabIndex = 6;
			this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.Controls.Add(this.treeView2);
			this.panel2.Location = new System.Drawing.Point(0, 43);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(249, 747);
			this.panel2.TabIndex = 1;
			// 
			// treeView2
			// 
			this.treeView2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView2.Location = new System.Drawing.Point(0, 0);
			this.treeView2.Margin = new System.Windows.Forms.Padding(2);
			this.treeView2.MinimumSize = new System.Drawing.Size(234, 4);
			this.treeView2.Name = "treeView2";
			this.treeView2.Size = new System.Drawing.Size(249, 747);
			this.treeView2.TabIndex = 0;
			this.treeView2.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView2_AfterSelect);
			// 
			// button4
			// 
			this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button4.Location = new System.Drawing.Point(195, 4);
			this.button4.Margin = new System.Windows.Forms.Padding(2);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(56, 34);
			this.button4.TabIndex = 9;
			this.button4.Text = "更新";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(137, 5);
			this.button2.Margin = new System.Windows.Forms.Padding(2);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(51, 34);
			this.button2.TabIndex = 8;
			this.button2.Text = "検索";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// textBox2
			// 
			this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox2.Location = new System.Drawing.Point(2, 4);
			this.textBox2.Margin = new System.Windows.Forms.Padding(2);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(129, 22);
			this.textBox2.TabIndex = 8;
			this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(1512, 829);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.splitContainer1);
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "Form1";
			this.Text = "遊戯王カードコレクトサポーター";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.PerformLayout();
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem これについてToolStripMenuItem;
		public System.Windows.Forms.ToolStripMenuItem ログToolStripMenuItem;
		public System.Windows.Forms.Label label1;
		public System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		public System.Windows.Forms.ToolStripMenuItem データ取得ToolStripMenuItem;
		public System.Windows.Forms.SplitContainer splitContainer1;
		public System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripMenuItem パックデータ取得ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem カードデータ取得ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem パックカード両方取得ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ホームToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem パック分類設定ToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Button button4;
		public System.Windows.Forms.TreeView treeView2;
		private System.Windows.Forms.ToolStripMenuItem シリーズ期設定ToolStripMenuItem;
		public System.Windows.Forms.ToolStripMenuItem 販売価格調査ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 新しいパックと新しいカード取得ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 評価調査ToolStripMenuItem;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
	}
}

