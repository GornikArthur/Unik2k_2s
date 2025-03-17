using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PaintWPF
{
	public class MyBrokenLine : MyFigure
	{
		public List<MyLine> arr_lines { get; set; }
		public int last_line { get; set; } = -1;

		private static MyBrokenLine broken_line = null;

		public MyBrokenLine()
		{
			arr_lines = new List<MyLine>();
		}
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
		public override void MouseMove(Point pos, Canvas Paint_canvas, List<MyFigure> arr_figures)
		{
			if (broken_line != null && broken_line.GetLineByIndex(broken_line.last_line) != null)
			{
				broken_line.GetLineByIndex(broken_line.last_line).Calc(pos);
			}
		}

		public static MyLine CreatingLine(Point startPoint, Color selectedColor, int thickness, Canvas Paint_canvas, List<MyFigure> arr_figures)
		{
			if (broken_line == null) broken_line = new MyBrokenLine();
			MyLine line = new MyLine(startPoint, selectedColor, thickness, Paint_canvas, arr_figures);
			broken_line.AddLine(line);
			if (!Paint_canvas.Children.Contains(line.GetFigure()))
			{
				Paint_canvas.Children.Add(line.GetFigure());
			}
			return line;
		}
		public static bool FinishBrokenLine(List<MyFigure> arr_figures, Key key)
		{
			if (broken_line != null && key == Key.Escape)
			{
				arr_figures.Add(broken_line);
				broken_line = null;  // Сбрасываем для новой линии
				return false;
			}
			return true;
		}
	}
}
