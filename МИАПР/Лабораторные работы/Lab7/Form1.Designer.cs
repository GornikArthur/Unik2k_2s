namespace Lab7;

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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        p_controls = new System.Windows.Forms.Panel();
        btn_validate = new System.Windows.Forms.Button();
        btn_clear = new System.Windows.Forms.Button();
        btn_generate = new System.Windows.Forms.Button();
        p_canvas = new System.Windows.Forms.Panel();
        p_controls.SuspendLayout();
        SuspendLayout();
        // 
        // p_controls
        // 
        p_controls.BackColor = System.Drawing.SystemColors.AppWorkspace;
        p_controls.Controls.Add(btn_validate);
        p_controls.Controls.Add(btn_clear);
        p_controls.Controls.Add(btn_generate);
        p_controls.Dock = System.Windows.Forms.DockStyle.Top;
        p_controls.Location = new System.Drawing.Point(0, 0);
        p_controls.Name = "p_controls";
        p_controls.Size = new System.Drawing.Size(814, 62);
        p_controls.TabIndex = 0;
        // 
        // btn_validate
        // 
        btn_validate.BackColor = System.Drawing.Color.FromArgb(((int)((byte)128)), ((int)((byte)255)), ((int)((byte)128)));
        btn_validate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btn_validate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        btn_validate.Location = new System.Drawing.Point(651, 9);
        btn_validate.Name = "btn_validate";
        btn_validate.Size = new System.Drawing.Size(153, 40);
        btn_validate.TabIndex = 2;
        btn_validate.Text = "Проверить";
        btn_validate.UseVisualStyleBackColor = false;
        btn_validate.Click += btn_validate_Click;
        // 
        // btn_clear
        // 
        btn_clear.BackColor = System.Drawing.Color.FromArgb(((int)((byte)255)), ((int)((byte)192)), ((int)((byte)128)));
        btn_clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btn_clear.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        btn_clear.Location = new System.Drawing.Point(183, 9);
        btn_clear.Name = "btn_clear";
        btn_clear.Size = new System.Drawing.Size(153, 40);
        btn_clear.TabIndex = 1;
        btn_clear.Text = "Очистить холст";
        btn_clear.UseVisualStyleBackColor = false;
        btn_clear.Click += btn_clear_Click;
        // 
        // btn_generate
        // 
        btn_generate.BackColor = System.Drawing.Color.FromArgb(((int)((byte)128)), ((int)((byte)255)), ((int)((byte)255)));
        btn_generate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btn_generate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        btn_generate.Location = new System.Drawing.Point(11, 9);
        btn_generate.Name = "btn_generate";
        btn_generate.Size = new System.Drawing.Size(153, 40);
        btn_generate.TabIndex = 0;
        btn_generate.Text = "Сгенерировать ";
        btn_generate.UseVisualStyleBackColor = false;
        btn_generate.Click += btn_generate_Click;
        // 
        // p_canvas
        // 
        p_canvas.Dock = System.Windows.Forms.DockStyle.Fill;
        p_canvas.Location = new System.Drawing.Point(0, 62);
        p_canvas.Name = "p_canvas";
        p_canvas.Size = new System.Drawing.Size(814, 495);
        p_canvas.TabIndex = 1;
        p_canvas.Paint += p_canvas_Paint;
        p_canvas.MouseDown += p_canvas_MouseDown;
        // 
        // Form1
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(814, 557);
        Controls.Add(p_canvas);
        Controls.Add(p_controls);
        DoubleBuffered = true;
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "Form1";
        p_controls.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button btn_validate;

    private System.Windows.Forms.Button btn_generate;

    private System.Windows.Forms.Button btn_clear;

    private System.Windows.Forms.Panel p_canvas;

    private System.Windows.Forms.Panel p_controls;

    #endregion
}