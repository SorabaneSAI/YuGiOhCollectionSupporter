namespace YuGiOhCollectionSupporter
{
    partial class SeriesGroupForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.SeriesGroupDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.シリーズ名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.開始日 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.有効フラグDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SeriesGroupDataBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.75F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.25F));
			this.tableLayoutPanel1.Controls.Add(this.button1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.button2, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.22222F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.777778F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1067, 562);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.button1.Location = new System.Drawing.Point(690, 520);
			this.button1.Margin = new System.Windows.Forms.Padding(4);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(167, 29);
			this.button1.TabIndex = 1;
			this.button1.Text = "保存";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.button2.Location = new System.Drawing.Point(922, 520);
			this.button2.Margin = new System.Windows.Forms.Padding(4);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(141, 29);
			this.button2.TabIndex = 2;
			this.button2.Text = "キャンセル";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.AutoGenerateColumns = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.シリーズ名,
            this.開始日,
            this.有効フラグDataGridViewCheckBoxColumn});
			this.tableLayoutPanel1.SetColumnSpan(this.dataGridView1, 2);
			this.dataGridView1.DataSource = this.SeriesGroupDataBindingSource;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point(4, 4);
			this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersWidth = 51;
			this.dataGridView1.RowTemplate.Height = 21;
			this.dataGridView1.Size = new System.Drawing.Size(1059, 499);
			this.dataGridView1.TabIndex = 3;
			// 
			// SeriesGroupDataBindingSource
			// 
			this.SeriesGroupDataBindingSource.DataSource = typeof(YuGiOhCollectionSupporter.SeriesGroupData);
			// 
			// シリーズ名
			// 
			this.シリーズ名.DataPropertyName = "シリーズ名";
			this.シリーズ名.HeaderText = "シリーズ名";
			this.シリーズ名.MinimumWidth = 6;
			this.シリーズ名.Name = "シリーズ名";
			this.シリーズ名.ReadOnly = true;
			this.シリーズ名.Width = 200;
			// 
			// 開始日
			// 
			this.開始日.DataPropertyName = "開始日";
			this.開始日.HeaderText = "開始日";
			this.開始日.MinimumWidth = 6;
			this.開始日.Name = "開始日";
			this.開始日.ReadOnly = true;
			this.開始日.Width = 200;
			// 
			// 有効フラグDataGridViewCheckBoxColumn
			// 
			this.有効フラグDataGridViewCheckBoxColumn.DataPropertyName = "有効フラグ";
			this.有効フラグDataGridViewCheckBoxColumn.HeaderText = "有効";
			this.有効フラグDataGridViewCheckBoxColumn.MinimumWidth = 6;
			this.有効フラグDataGridViewCheckBoxColumn.Name = "有効フラグDataGridViewCheckBoxColumn";
			this.有効フラグDataGridViewCheckBoxColumn.Width = 50;
			// 
			// SeriesGroupForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1067, 562);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "SeriesGroupForm";
			this.Text = "SeriesGroupForm";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PackGroupForm_FormClosed);
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SeriesGroupDataBindingSource)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource SeriesGroupDataBindingSource;
		private System.Windows.Forms.DataGridViewTextBoxColumn シリーズ名DataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn シリーズ名;
		private System.Windows.Forms.DataGridViewTextBoxColumn 開始日;
		private System.Windows.Forms.DataGridViewCheckBoxColumn 有効フラグDataGridViewCheckBoxColumn;
	}
}