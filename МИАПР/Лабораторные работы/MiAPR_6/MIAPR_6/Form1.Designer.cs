﻿namespace MIAPR_6
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.button1 = new System.Windows.Forms.Button();
			this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.label1 = new System.Windows.Forms.Label();
			this.radioBtnMaximum = new System.Windows.Forms.RadioButton();
			this.radioBtnMinimum = new System.Windows.Forms.RadioButton();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView
			// 
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.ColumnHeadersVisible = false;
			this.dataGridView.Location = new System.Drawing.Point(789, 49);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.RowHeadersVisible = false;
			this.dataGridView.RowHeadersWidth = 51;
			this.dataGridView.Size = new System.Drawing.Size(347, 331);
			this.dataGridView.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(900, 435);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(236, 64);
			this.button1.TabIndex = 3;
			this.button1.Text = "Выполнить классификацию объектов";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// chart1
			// 
			chartArea1.Name = "ChartArea1";
			this.chart1.ChartAreas.Add(chartArea1);
			legend1.Name = "Legend1";
			this.chart1.Legends.Add(legend1);
			this.chart1.Location = new System.Drawing.Point(0, 4);
			this.chart1.Name = "chart1";
			series1.ChartArea = "ChartArea1";
			series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
			series1.Legend = "Legend1";
			series1.Name = "Series1";
			this.chart1.Series.Add(series1);
			this.chart1.Size = new System.Drawing.Size(710, 504);
			this.chart1.TabIndex = 4;
			this.chart1.Text = "chart1";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(785, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(242, 25);
			this.label1.TabIndex = 5;
			this.label1.Text = "Количество объектов:";
			// 
			// radioBtnMaximum
			// 
			this.radioBtnMaximum.AutoSize = true;
			this.radioBtnMaximum.Location = new System.Drawing.Point(789, 435);
			this.radioBtnMaximum.Name = "radioBtnMaximum";
			this.radioBtnMaximum.Size = new System.Drawing.Size(74, 29);
			this.radioBtnMaximum.TabIndex = 1;
			this.radioBtnMaximum.TabStop = true;
			this.radioBtnMaximum.Text = "Max";
			this.radioBtnMaximum.UseVisualStyleBackColor = true;
			// 
			// radioBtnMinimum
			// 
			this.radioBtnMinimum.AutoSize = true;
			this.radioBtnMinimum.Checked = true;
			this.radioBtnMinimum.Location = new System.Drawing.Point(789, 470);
			this.radioBtnMinimum.Name = "radioBtnMinimum";
			this.radioBtnMinimum.Size = new System.Drawing.Size(68, 29);
			this.radioBtnMinimum.TabIndex = 0;
			this.radioBtnMinimum.TabStop = true;
			this.radioBtnMinimum.Text = "Min";
			this.radioBtnMinimum.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(821, 392);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(283, 25);
			this.label2.TabIndex = 6;
			this.label2.Text = "Критерий классификации:";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(1033, 10);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(71, 30);
			this.textBox1.TabIndex = 7;
			this.textBox1.Text = "4";
			this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
			// 
			// Form1
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.ClientSize = new System.Drawing.Size(1174, 521);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.radioBtnMaximum);
			this.Controls.Add(this.radioBtnMinimum);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.chart1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.dataGridView);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "LAB6 Иерархическое групование";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioBtnMaximum;
        private System.Windows.Forms.RadioButton radioBtnMinimum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
    }
}

