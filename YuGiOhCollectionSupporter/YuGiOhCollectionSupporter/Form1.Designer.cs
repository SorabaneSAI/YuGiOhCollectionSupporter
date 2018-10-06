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
			this.webBrowser1 = new System.Windows.Forms.WebBrowser();
			this.label1 = new System.Windows.Forms.Label();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.種類 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.LV = new System.Windows.Forms.DataGridViewImageColumn();
			this.属性 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.種族 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ATK = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DEF = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.TXT = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
			this.menuStrip1.Size = new System.Drawing.Size(1025, 24);
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
			// webBrowser1
			// 
			this.webBrowser1.Location = new System.Drawing.Point(0, 24);
			this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser1.Name = "webBrowser1";
			this.webBrowser1.ScriptErrorsSuppressed = true;
			this.webBrowser1.Size = new System.Drawing.Size(20, 20);
			this.webBrowser1.TabIndex = 1;
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
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Name,
            this.種類,
            this.LV,
            this.属性,
            this.種族,
            this.ATK,
            this.DEF,
            this.TXT});
			this.dataGridView1.Location = new System.Drawing.Point(0, 27);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.RowTemplate.Height = 21;
			this.dataGridView1.Size = new System.Drawing.Size(1025, 431);
			this.dataGridView1.TabIndex = 3;
			// 
			// Name
			// 
			this.Name.HeaderText = "名前";
			this.Name.Name = "Name";
			this.Name.ReadOnly = true;
			this.Name.Width = 200;
			// 
			// 種類
			// 
			this.種類.HeaderText = "種類";
			this.種類.Name = "種類";
			this.種類.ReadOnly = true;
			this.種類.Width = 200;
			// 
			// LV
			// 
			this.LV.HeaderText = "LV";
			this.LV.Name = "LV";
			this.LV.ReadOnly = true;
			this.LV.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.LV.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.LV.Width = 50;
			// 
			// 属性
			// 
			this.属性.HeaderText = "属性";
			this.属性.Name = "属性";
			this.属性.ReadOnly = true;
			this.属性.Width = 60;
			// 
			// 種族
			// 
			this.種族.HeaderText = "種族";
			this.種族.Name = "種族";
			this.種族.ReadOnly = true;
			// 
			// ATK
			// 
			this.ATK.HeaderText = "ATK";
			this.ATK.Name = "ATK";
			this.ATK.ReadOnly = true;
			this.ATK.Width = 80;
			// 
			// DEF
			// 
			this.DEF.HeaderText = "DEF";
			this.DEF.Name = "DEF";
			this.DEF.ReadOnly = true;
			this.DEF.Width = 80;
			// 
			// TXT
			// 
			this.TXT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.TXT.HeaderText = "TXT";
			this.TXT.Name = "TXT";
			this.TXT.ReadOnly = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1025, 454);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.webBrowser1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form1";
			this.Text = "遊戯王カードコレクトサポーター";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem これについてToolStripMenuItem;
		private System.Windows.Forms.WebBrowser webBrowser1;
		private System.Windows.Forms.ToolStripMenuItem データ取得ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ログToolStripMenuItem;
		public System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Name;
		private System.Windows.Forms.DataGridViewTextBoxColumn 種類;
		private System.Windows.Forms.DataGridViewImageColumn LV;
		private System.Windows.Forms.DataGridViewTextBoxColumn 属性;
		private System.Windows.Forms.DataGridViewTextBoxColumn 種族;
		private System.Windows.Forms.DataGridViewTextBoxColumn ATK;
		private System.Windows.Forms.DataGridViewTextBoxColumn DEF;
		private System.Windows.Forms.DataGridViewTextBoxColumn TXT;
	}
}

