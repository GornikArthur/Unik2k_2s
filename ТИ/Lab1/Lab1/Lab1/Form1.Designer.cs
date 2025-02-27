namespace Lab1
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			ColumnCheckBox = new CheckBox();
			ColumnTextBox1 = new TextBox();
			Columnlabel1 = new Label();
			ColumnTextBox3 = new TextBox();
			Columnlabel3 = new Label();
			ColumnButtonMake = new Button();
			ColumnButtonGet = new Button();
			Columnlabel2 = new Label();
			ColumnTextBox2 = new TextBox();
			ResTextBox = new TextBox();
			label1 = new Label();
			VigenereButtonGet = new Button();
			VigenereButtonMake = new Button();
			Vigenerelabel3 = new Label();
			VigenereTextBox2 = new TextBox();
			Vigenerelabel = new Label();
			VigenereTextBox1 = new TextBox();
			VigenereCheckBox = new CheckBox();
			LoadButton = new Button();
			SaveButton = new Button();
			SuspendLayout();
			// 
			// ColumnCheckBox
			// 
			ColumnCheckBox.AutoSize = true;
			ColumnCheckBox.Location = new Point(25, 51);
			ColumnCheckBox.Name = "ColumnCheckBox";
			ColumnCheckBox.Size = new Size(208, 19);
			ColumnCheckBox.TabIndex = 0;
			ColumnCheckBox.Text = "Столбцовый метод шифровнаия";
			ColumnCheckBox.UseVisualStyleBackColor = true;
			ColumnCheckBox.CheckedChanged += ColumnCheckBox_CheckedChanged;
			// 
			// ColumnTextBox1
			// 
			ColumnTextBox1.Location = new Point(122, 76);
			ColumnTextBox1.Name = "ColumnTextBox1";
			ColumnTextBox1.ScrollBars = ScrollBars.Horizontal;
			ColumnTextBox1.Size = new Size(249, 23);
			ColumnTextBox1.TabIndex = 1;
			ColumnTextBox1.TextChanged += ColumnTextBox1_TextChanged_1;
			// 
			// Columnlabel1
			// 
			Columnlabel1.AutoSize = true;
			Columnlabel1.Location = new Point(21, 79);
			Columnlabel1.Name = "Columnlabel1";
			Columnlabel1.Size = new Size(95, 15);
			Columnlabel1.TabIndex = 2;
			Columnlabel1.Text = "Введите ключ 1:";
			// 
			// ColumnTextBox3
			// 
			ColumnTextBox3.Location = new Point(122, 139);
			ColumnTextBox3.Multiline = true;
			ColumnTextBox3.Name = "ColumnTextBox3";
			ColumnTextBox3.ScrollBars = ScrollBars.Both;
			ColumnTextBox3.Size = new Size(249, 58);
			ColumnTextBox3.TabIndex = 3;
			ColumnTextBox3.TextChanged += ColumnTextBox2_TextChanged;
			// 
			// Columnlabel3
			// 
			Columnlabel3.AutoSize = true;
			Columnlabel3.Location = new Point(21, 142);
			Columnlabel3.Name = "Columnlabel3";
			Columnlabel3.Size = new Size(84, 15);
			Columnlabel3.TabIndex = 4;
			Columnlabel3.Text = "Введите текст:";
			// 
			// ColumnButtonMake
			// 
			ColumnButtonMake.Location = new Point(122, 203);
			ColumnButtonMake.Name = "ColumnButtonMake";
			ColumnButtonMake.Size = new Size(123, 33);
			ColumnButtonMake.TabIndex = 5;
			ColumnButtonMake.Text = "Зашифровать";
			ColumnButtonMake.UseVisualStyleBackColor = true;
			ColumnButtonMake.Click += ColumnButtonMake_Click;
			// 
			// ColumnButtonGet
			// 
			ColumnButtonGet.Location = new Point(248, 203);
			ColumnButtonGet.Name = "ColumnButtonGet";
			ColumnButtonGet.Size = new Size(123, 33);
			ColumnButtonGet.TabIndex = 6;
			ColumnButtonGet.Text = "Дешифровать";
			ColumnButtonGet.UseVisualStyleBackColor = true;
			ColumnButtonGet.Click += ColumnButtonGet_Click;
			// 
			// Columnlabel2
			// 
			Columnlabel2.AutoSize = true;
			Columnlabel2.Location = new Point(21, 108);
			Columnlabel2.Name = "Columnlabel2";
			Columnlabel2.Size = new Size(95, 15);
			Columnlabel2.TabIndex = 8;
			Columnlabel2.Text = "Введите ключ 2:";
			// 
			// ColumnTextBox2
			// 
			ColumnTextBox2.Location = new Point(122, 105);
			ColumnTextBox2.Name = "ColumnTextBox2";
			ColumnTextBox2.ScrollBars = ScrollBars.Horizontal;
			ColumnTextBox2.Size = new Size(249, 23);
			ColumnTextBox2.TabIndex = 7;
			ColumnTextBox2.TextChanged += ColumnTextBox2_TextChanged_1;
			// 
			// ResTextBox
			// 
			ResTextBox.Location = new Point(122, 278);
			ResTextBox.Multiline = true;
			ResTextBox.Name = "ResTextBox";
			ResTextBox.ReadOnly = true;
			ResTextBox.ScrollBars = ScrollBars.Vertical;
			ResTextBox.Size = new Size(637, 199);
			ResTextBox.TabIndex = 9;
			ResTextBox.TextChanged += ResTextBox_TextChanged;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(404, 260);
			label1.Name = "label1";
			label1.Size = new Size(63, 15);
			label1.TabIndex = 10;
			label1.Text = "Результат:";
			// 
			// VigenereButtonGet
			// 
			VigenereButtonGet.Location = new Point(627, 203);
			VigenereButtonGet.Name = "VigenereButtonGet";
			VigenereButtonGet.Size = new Size(123, 33);
			VigenereButtonGet.TabIndex = 17;
			VigenereButtonGet.Text = "Дешифровать";
			VigenereButtonGet.UseVisualStyleBackColor = true;
			VigenereButtonGet.Click += VigenereButtonGet_Click;
			// 
			// VigenereButtonMake
			// 
			VigenereButtonMake.Location = new Point(498, 203);
			VigenereButtonMake.Name = "VigenereButtonMake";
			VigenereButtonMake.Size = new Size(123, 33);
			VigenereButtonMake.TabIndex = 16;
			VigenereButtonMake.Text = "Зашифровать";
			VigenereButtonMake.UseVisualStyleBackColor = true;
			VigenereButtonMake.Click += VigenereButtonMake_Click;
			// 
			// Vigenerelabel3
			// 
			Vigenerelabel3.AutoSize = true;
			Vigenerelabel3.Location = new Point(404, 113);
			Vigenerelabel3.Name = "Vigenerelabel3";
			Vigenerelabel3.Size = new Size(84, 15);
			Vigenerelabel3.TabIndex = 15;
			Vigenerelabel3.Text = "Введите текст:";
			// 
			// VigenereTextBox2
			// 
			VigenereTextBox2.Location = new Point(501, 108);
			VigenereTextBox2.Multiline = true;
			VigenereTextBox2.Name = "VigenereTextBox2";
			VigenereTextBox2.ScrollBars = ScrollBars.Both;
			VigenereTextBox2.Size = new Size(249, 89);
			VigenereTextBox2.TabIndex = 14;
			VigenereTextBox2.TextChanged += VigenereTextBox2_TextChanged;
			// 
			// Vigenerelabel
			// 
			Vigenerelabel.AutoSize = true;
			Vigenerelabel.Location = new Point(400, 79);
			Vigenerelabel.Name = "Vigenerelabel";
			Vigenerelabel.Size = new Size(86, 15);
			Vigenerelabel.TabIndex = 13;
			Vigenerelabel.Text = "Введите ключ:";
			// 
			// VigenereTextBox1
			// 
			VigenereTextBox1.Location = new Point(501, 76);
			VigenereTextBox1.Name = "VigenereTextBox1";
			VigenereTextBox1.ScrollBars = ScrollBars.Horizontal;
			VigenereTextBox1.Size = new Size(249, 23);
			VigenereTextBox1.TabIndex = 12;
			VigenereTextBox1.TextChanged += VigenereTextBox1_TextChanged;
			// 
			// VigenereCheckBox
			// 
			VigenereCheckBox.AutoSize = true;
			VigenereCheckBox.Location = new Point(404, 51);
			VigenereCheckBox.Name = "VigenereCheckBox";
			VigenereCheckBox.Size = new Size(195, 19);
			VigenereCheckBox.TabIndex = 11;
			VigenereCheckBox.Text = "Метод шифровнаия Виженера";
			VigenereCheckBox.UseVisualStyleBackColor = true;
			VigenereCheckBox.CheckedChanged += VigenereCheckBox_CheckedChanged;
			// 
			// LoadButton
			// 
			LoadButton.Location = new Point(21, 12);
			LoadButton.Name = "LoadButton";
			LoadButton.Size = new Size(189, 23);
			LoadButton.TabIndex = 18;
			LoadButton.Text = "Загрузить данные из файла";
			LoadButton.UseVisualStyleBackColor = true;
			LoadButton.Click += LoadButton_Click;
			// 
			// SaveButton
			// 
			SaveButton.Location = new Point(336, 494);
			SaveButton.Name = "SaveButton";
			SaveButton.Size = new Size(189, 23);
			SaveButton.TabIndex = 19;
			SaveButton.Text = "Сохранить данные в файл";
			SaveButton.UseVisualStyleBackColor = true;
			SaveButton.Click += SaveButton_Click;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 543);
			Controls.Add(SaveButton);
			Controls.Add(LoadButton);
			Controls.Add(VigenereButtonGet);
			Controls.Add(VigenereButtonMake);
			Controls.Add(Vigenerelabel3);
			Controls.Add(VigenereTextBox2);
			Controls.Add(Vigenerelabel);
			Controls.Add(VigenereTextBox1);
			Controls.Add(VigenereCheckBox);
			Controls.Add(label1);
			Controls.Add(ResTextBox);
			Controls.Add(Columnlabel2);
			Controls.Add(ColumnTextBox2);
			Controls.Add(ColumnButtonGet);
			Controls.Add(ColumnButtonMake);
			Controls.Add(Columnlabel3);
			Controls.Add(ColumnTextBox3);
			Controls.Add(Columnlabel1);
			Controls.Add(ColumnTextBox1);
			Controls.Add(ColumnCheckBox);
			Name = "Form1";
			Text = "ТИ Lab1";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private CheckBox ColumnCheckBox;
		private TextBox ColumnTextBox1;
		private Label Columnlabel1;
		private TextBox ColumnTextBox3;
		private Label Columnlabel3;
		private Button ColumnButtonMake;
		private Button ColumnButtonGet;
		private Label Columnlabel2;
		private TextBox ColumnTextBox2;
		private TextBox ResTextBox;
		private Label label1;
		private Button VigenereButtonGet;
		private Button VigenereButtonMake;
		private Label Vigenerelabel3;
		private TextBox VigenereTextBox2;
		private Label Vigenerelabel;
		private TextBox VigenereTextBox1;
		private CheckBox VigenereCheckBox;
		private Button LoadButton;
		private Button SaveButton;
	}
}
