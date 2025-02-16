using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
	public partial class Form1 : Form
	{
		BrokeLine rect = new BrokeLine();
		private bool isDrawing = false;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			//e.Graphics.Clear(Color.White);
			if (isDrawing)
			{
				rect.Draw(e.Graphics);
			}
		}

		private void Form1_MouseDown(object sender, MouseEventArgs e)
		{
			/*rect = new Ellipse(e.X, e.Y, 0, 0);
			isDrawing = true;*/
		}

		private void Form1_MouseMove(object sender, MouseEventArgs e)
		{
			/*if (isDrawing)
			{
				rect.Calc(e);
				Invalidate();
			}*/
		}

		private void Form1_MouseUp(object sender, MouseEventArgs e)
		{
			//isDrawing = false;
		}

		private void Form1_MouseClick(object sender, MouseEventArgs e)
		{
			rect.Calc(e);
		}

		private void Form1_DoubleClick(object sender, EventArgs e)
		{
			isDrawing = true;
			Invalidate();
			
		}
	}
}
