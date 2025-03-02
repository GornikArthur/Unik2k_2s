using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PaintWPF
{
	public class MyBrokenLine : MyFigure
	{
		public List<MyLine> arr_lines { get; set; } = new List<MyLine>();
		public int last_line { get; set; } = -1;

		public override void Calc(Point newPoint)
		{
			if (last_line >= 0)
			{
				arr_lines[last_line].line.X2 = newPoint.X;
				arr_lines[last_line].line.Y2 = newPoint.Y;
			}
		}

		public void AddLine(MyLine new_line)
		{
			last_line++;
			arr_lines.Add(new_line);
		}

		public MyLine GetLineByIndex(int index)
		{
			return arr_lines[index];
		}

		public override void SetFillColor(Color color)
		{
			// Ломаная линия не заполняется
		}

		public override bool IsPointInside(Point point)
		{
			return false;
		}

		public override void RemoveFigure(Canvas canvas)
		{
			foreach (MyLine myLine in arr_lines)
			{
				canvas.Children.Remove(myLine.line);
			}
		}

		public override void AddFigure(Canvas canvas)
		{
			foreach (MyLine myLine in arr_lines)
			{
				myLine.AddFigure(canvas);
			}
		}
	}
}
