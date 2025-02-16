using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Paint
{
	internal class Polygon
	{
		List<Point> peaks = new List<Point>();

		public void Calc(MouseEventArgs e)
		{
			peaks.Add(e.Location);
		}

		public void Draw(Graphics graph)
		{
			graph.DrawPolygon(Pens.Black, peaks.ToArray());
			peaks = new List<Point>();
		}
	}
}
