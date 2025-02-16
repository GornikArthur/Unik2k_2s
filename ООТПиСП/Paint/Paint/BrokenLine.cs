using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Paint
{
	internal class BrokeLine
	{
		List<Point> peaks = new List<Point>();

		public void Calc(MouseEventArgs e)
		{
			peaks.Add(e.Location);
		}

		public void Draw(Graphics graph)
		{
			for (int i = 1; i < peaks.Count; i++) {
				graph.DrawLine(Pens.Black, peaks[i], peaks[i - 1]);
			}
		}
	}
}
