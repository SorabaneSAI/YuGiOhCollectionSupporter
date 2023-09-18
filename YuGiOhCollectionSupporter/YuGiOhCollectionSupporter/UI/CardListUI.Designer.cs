namespace YuGiOhCollectionSupporter
{
	partial class CardListUI
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

		#region コンポーネント デザイナーで生成されたコード

		/// <summary> 
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.type = new System.Windows.Forms.DataGridViewImageColumn();
			this.名前 = new System.Windows.Forms.DataGridViewLinkColumn();
			this.略号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.レアリティ = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Qランク = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.Is同名予備カード枚数十分 = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.Q値段 = new System.Windows.Forms.DataGridViewLinkColumn();
			this.collectDataUI1 = new YuGiOhCollectionSupporter.CollectDataUI();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.flowLayoutPanel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("MS UI Gothic", 15F);
			this.label1.Location = new System.Drawing.Point(4, 46);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 12, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(134, 25);
			this.label1.TabIndex = 3;
			this.label1.Text = "パックの種類";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("MS UI Gothic", 15F);
			this.label4.Location = new System.Drawing.Point(4, 83);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 12, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(87, 25);
			this.label4.TabIndex = 8;
			this.label4.Text = "シリーズ";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 7);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.linkLabel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.collectDataUI1, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel3, 0, 3);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 4);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 8;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1012, 1061);
			this.tableLayoutPanel1.TabIndex = 9;
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(4, 941);
			this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(1004, 116);
			this.flowLayoutPanel2.TabIndex = 13;
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Font = new System.Drawing.Font("MS UI Gothic", 20F);
			this.linkLabel1.Location = new System.Drawing.Point(4, 0);
			this.linkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(121, 34);
			this.linkLabel1.TabIndex = 9;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "パック名";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.type,
            this.名前,
            this.略号,
            this.レアリティ,
            this.Qランク,
            this.Is同名予備カード枚数十分,
            this.Q値段});
			this.dataGridView1.Location = new System.Drawing.Point(4, 517);
			this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.RowHeadersWidth = 51;
			this.dataGridView1.RowTemplate.Height = 21;
			this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.dataGridView1.Size = new System.Drawing.Size(988, 416);
			this.dataGridView1.TabIndex = 0;
			this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
			this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellEndEdit);
			this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
			this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
			this.dataGridView1.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView1_CurrentCellDirtyStateChanged);
			this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DataGridView1_EditingControlShowing);
			this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
			// 
			// type
			// 
			this.type.HeaderText = "";
			this.type.MinimumWidth = 6;
			this.type.Name = "type";
			this.type.ReadOnly = true;
			this.type.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.type.Width = 30;
			// 
			// 名前
			// 
			this.名前.HeaderText = "名前";
			this.名前.MinimumWidth = 100;
			this.名前.Name = "名前";
			this.名前.ReadOnly = true;
			this.名前.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.名前.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.名前.Width = 250;
			// 
			// 略号
			// 
			this.略号.HeaderText = "略号";
			this.略号.MinimumWidth = 70;
			this.略号.Name = "略号";
			this.略号.ReadOnly = true;
			this.略号.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.略号.Width = 125;
			// 
			// レアリティ
			// 
			this.レアリティ.HeaderText = "レアリティ";
			this.レアリティ.MinimumWidth = 70;
			this.レアリティ.Name = "レアリティ";
			this.レアリティ.ReadOnly = true;
			this.レアリティ.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.レアリティ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.レアリティ.Width = 70;
			// 
			// Qランク
			// 
			this.Qランク.HeaderText = "Qランク";
			this.Qランク.Items.AddRange(new object[] {
            "S",
            "A",
            "B",
            "C",
            "D",
            "なし",
            "不明"});
			this.Qランク.MinimumWidth = 6;
			this.Qランク.Name = "Qランク";
			this.Qランク.Width = 80;
			// 
			// Is同名予備カード枚数十分
			// 
			this.Is同名予備カード枚数十分.HeaderText = "同名枚数";
			this.Is同名予備カード枚数十分.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9+"});
			this.Is同名予備カード枚数十分.MinimumWidth = 6;
			this.Is同名予備カード枚数十分.Name = "Is同名予備カード枚数十分";
			this.Is同名予備カード枚数十分.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.Is同名予備カード枚数十分.Width = 80;
			// 
			// Q値段
			// 
			this.Q値段.HeaderText = "Q値段";
			this.Q値段.MinimumWidth = 6;
			this.Q値段.Name = "Q値段";
			this.Q値段.ReadOnly = true;
			this.Q値段.Width = 80;
			// 
			// collectDataUI1
			// 
			this.collectDataUI1.Location = new System.Drawing.Point(5, 157);
			this.collectDataUI1.Margin = new System.Windows.Forms.Padding(5);
			this.collectDataUI1.Name = "collectDataUI1";
			this.collectDataUI1.Size = new System.Drawing.Size(987, 289);
			this.collectDataUI1.TabIndex = 11;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(4, 455);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(1004, 54);
			this.flowLayoutPanel1.TabIndex = 12;
			// 
			// flowLayoutPanel3
			// 
			this.flowLayoutPanel3.Controls.Add(this.button1);
			this.flowLayoutPanel3.Controls.Add(this.button2);
			this.flowLayoutPanel3.Controls.Add(this.button3);
			this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 111);
			this.flowLayoutPanel3.Name = "flowLayoutPanel3";
			this.flowLayoutPanel3.Size = new System.Drawing.Size(592, 38);
			this.flowLayoutPanel3.TabIndex = 14;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(4, 4);
			this.button1.Margin = new System.Windows.Forms.Padding(4);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(177, 29);
			this.button1.TabIndex = 11;
			this.button1.Text = "リスト更新";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(188, 3);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(151, 30);
			this.button2.TabIndex = 12;
			this.button2.Text = "セーブ";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click_1);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(345, 3);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(176, 30);
			this.button3.TabIndex = 13;
			this.button3.Text = "このリストのデータ取得";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// CardListUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.AutoSize = true;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "CardListUI";
			this.Size = new System.Drawing.Size(1078, 1069);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.flowLayoutPanel3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private CollectDataUI collectDataUI1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.DataGridViewImageColumn type;
		private System.Windows.Forms.DataGridViewLinkColumn 名前;
		private System.Windows.Forms.DataGridViewTextBoxColumn 略号;
		private System.Windows.Forms.DataGridViewTextBoxColumn レアリティ;
		private System.Windows.Forms.DataGridViewComboBoxColumn Qランク;
		private System.Windows.Forms.DataGridViewComboBoxColumn Is同名予備カード枚数十分;
		private System.Windows.Forms.DataGridViewLinkColumn Q値段;
	}
}
