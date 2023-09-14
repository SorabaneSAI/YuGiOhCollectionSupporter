namespace YuGiOhCollectionSupporter
{
	partial class NotMatchForm
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
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.名前 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.カードデータ = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.カーナベルデータ = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.名前,
            this.カードデータ,
            this.カーナベルデータ});
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point(0, 0);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersWidth = 51;
			this.dataGridView1.RowTemplate.Height = 24;
			this.dataGridView1.Size = new System.Drawing.Size(800, 450);
			this.dataGridView1.TabIndex = 0;
			// 
			// 名前
			// 
			this.名前.HeaderText = "名前";
			this.名前.MinimumWidth = 6;
			this.名前.Name = "名前";
			this.名前.ReadOnly = true;
			this.名前.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.名前.Width = 200;
			// 
			// カードデータ
			// 
			this.カードデータ.HeaderText = "カードデータ";
			this.カードデータ.MinimumWidth = 6;
			this.カードデータ.Name = "カードデータ";
			this.カードデータ.ReadOnly = true;
			this.カードデータ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.カードデータ.Width = 200;
			// 
			// カーナベルデータ
			// 
			this.カーナベルデータ.HeaderText = "カーナベルデータ";
			this.カーナベルデータ.MinimumWidth = 6;
			this.カーナベルデータ.Name = "カーナベルデータ";
			this.カーナベルデータ.ReadOnly = true;
			this.カーナベルデータ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.カーナベルデータ.Width = 300;
			// 
			// NotMatchForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.dataGridView1);
			this.Name = "NotMatchForm";
			this.Text = "NotMatchForm";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn 名前;
		private System.Windows.Forms.DataGridViewTextBoxColumn カードデータ;
		private System.Windows.Forms.DataGridViewTextBoxColumn カーナベルデータ;
	}
}