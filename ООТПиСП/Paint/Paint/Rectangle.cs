using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Paint
{
	internal class Rectangle
	{
		public int x, start_x, start_y, y, width, height;

		public Rectangle(int x, int y, int width, int height) {
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
			this.start_x = x;
			this.start_y = y;
		}

		public void Calc(MouseEventArgs e)
		{
			this.x = Math.Min(this.start_x, e.X);
			this.y = Math.Min(this.start_y, e.Y);
			this.width = Math.Abs(e.X - this.start_x);
			this.height = Math.Abs(e.Y - this.start_y);
		}

		public void Draw(Graphics graph)
		{ 
			graph.DrawRectangle(Pens.Black, this.x, this.y, this.width, this.height);
		}
	}
}
