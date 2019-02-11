namespace YuGiOhCollectionSupporter
{
	partial class PackUI
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.label2 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.略号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.名前 = new System.Windows.Forms.DataGridViewLinkColumn();
			this.レアリティ = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.所持 = new System.Windows.Forms.DataGridViewButtonColumn();
			this.未所持 = new System.Windows.Forms.DataGridViewButtonColumn();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("MS UI Gothic", 20F);
			this.label2.Location = new System.Drawing.Point(3, 46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 27);
			this.label2.TabIndex = 1;
			this.label2.Text = "パック名";
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Font = new System.Drawing.Font("MS UI Gothic", 20F);
			this.linkLabel1.Location = new System.Drawing.Point(3, 0);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(100, 27);
			this.linkLabel1.TabIndex = 2;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "ホームへ";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("MS UI Gothic", 15F);
			this.label1.Location = new System.Drawing.Point(3, 91);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(108, 20);
			this.label1.TabIndex = 3;
			this.label1.Text = "パックの種類";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("MS UI Gothic", 15F);
			this.label3.Location = new System.Drawing.Point(6, 124);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(69, 20);
			this.label3.TabIndex = 4;
			this.label3.Text = "全枚数";
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.button1.Location = new System.Drawing.Point(404, 93);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(90, 51);
			this.button1.TabIndex = 5;
			this.button1.Text = "全部所持";
			this.button1.UseVisualStyleBackColor = false;
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.button2.Location = new System.Drawing.Point(543, 93);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(90, 51);
			this.button2.TabIndex = 6;
			this.button2.Text = "全部未所持";
			this.button2.UseVisualStyleBackColor = false;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BackColor = System.Drawing.Color.DarkGray;
			this.panel1.Controls.Add(this.dataGridView1);
			this.panel1.Location = new System.Drawing.Point(7, 158);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(808, 467);
			this.panel1.TabIndex = 7;
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.略号,
            this.名前,
            this.レアリティ,
            this.所持,
            this.未所持});
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point(0, 0);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 21;
			this.dataGridView1.Size = new System.Drawing.Size(808, 467);
			this.dataGridView1.TabIndex = 0;
			// 
			// 略号
			// 
			this.略号.HeaderText = "略号";
			this.略号.MinimumWidth = 80;
			this.略号.Name = "略号";
			// 
			// 名前
			// 
			this.名前.HeaderText = "名前";
			this.名前.MinimumWidth = 100;
			this.名前.Name = "名前";
			this.名前.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.名前.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.名前.Width = 250;
			// 
			// レアリティ
			// 
			this.レアリティ.HeaderText = "レアリティ";
			this.レアリティ.MinimumWidth = 100;
			this.レアリティ.Name = "レアリティ";
			this.レアリティ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.レアリティ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.レアリティ.Width = 150;
			// 
			// 所持
			// 
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Transparent;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
			this.所持.DefaultCellStyle = dataGridViewCellStyle1;
			this.所持.HeaderText = "所持";
			this.所持.MinimumWidth = 50;
			this.所持.Name = "所持";
			this.所持.Text = "所持";
			this.所持.Width = 60;
			// 
			// 未所持
			// 
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
			this.未所持.DefaultCellStyle = dataGridViewCellStyle2;
			this.未所持.HeaderText = "未所持";
			this.未所持.MinimumWidth = 50;
			this.未所持.Name = "未所持";
			this.未所持.Text = "未所持";
			this.未所持.Width = 60;
			// 
			// PackUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.label2);
			this.Name = "PackUI";
			this.Size = new System.Drawing.Size(818, 628);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn 略号;
		private System.Windows.Forms.DataGridViewLinkColumn 名前;
		private System.Windows.Forms.DataGridViewTextBoxColumn レアリティ;
		private System.Windows.Forms.DataGridViewButtonColumn 所持;
		private System.Windows.Forms.DataGridViewButtonColumn 未所持;
	}
}
