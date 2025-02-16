using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Paint
{
	internal class Line
	{
		public int st_x, st_y, fin_x, fin_y;

		public Line(int st_x, int st_y, int fin_x, int fin_y)
		{
			this.st_x = st_x;
			this.st_y = st_y;
			this.fin_x = fin_x;
			this.fin_y = fin_y;
		}

		public void Calc(MouseEventArgs e)
		{
			this.fin_x = e.X;
			this.fin_y = e.Y;
		}

		public void Draw(Graphics graph)
		{
			graph.DrawLine(Pens.Black, this.st_x, this.st_y, this.fin_x, this.fin_y);
		}
	}
}
