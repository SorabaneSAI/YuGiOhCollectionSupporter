namespace YuGiOhCollectionSupporter
{
	partial class KanabellForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(34, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "URL";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(52, 20);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(268, 22);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "https://www.ka-nabell.com";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 394);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(149, 44);
			this.button1.TabIndex = 2;
			this.button1.Text = "全データ取得＆解析";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(179, 394);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(141, 44);
			this.button2.TabIndex = 3;
			this.button2.Text = "解析だけやりなおし";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(337, 394);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(134, 44);
			this.button3.TabIndex = 4;
			this.button3.Text = "解析漏れリスト";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(337, 20);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(318, 22);
			this.textBox2.TabIndex = 5;
			this.textBox2.Text = "/?act=sell_search&type=4&genre=1";
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(489, 394);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(125, 44);
			this.button4.TabIndex = 6;
			this.button4.Text = "レアリティ比較設定";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(635, 394);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(121, 44);
			this.button5.TabIndex = 7;
			this.button5.Text = "スルーページ設定";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// KanabellForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.Name = "KanabellForm";
			this.Text = "KanabellForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
	}
}