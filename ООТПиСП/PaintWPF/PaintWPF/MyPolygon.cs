using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PaintWPF
{
	public class MyPolygon : MyFigure
	{
		public List<MyLine> arr_lines { get; set; } = new List<MyLine>();
		public int last_line { get; set; } = 0;
		public List<Point> Points { get; set; } = new List<Point>();
		public string StrokeColor { get; set; }
		public double StrokeThickness { get; set; }

		[JsonIgnore] // Не сериализуем сам WPF-объект
		private Polygon polygon;

		public MyPolygon() { } // Пустой конструктор для десериализации

		public MyPolygon(Point startPoint, Color selectedColor, int thickness)
		{
			MyLine new_line = new MyLine(startPoint, selectedColor, thickness);
			arr_lines.Add(new_line);
			StrokeColor = selectedColor.ToString();
			StrokeThickness = thickness;
		}

		public override void Calc(Point newPoint)
		{
			arr_lines[last_line].line.X2 = newPoint.X;
			arr_lines[last_line].line.Y2 = newPoint.Y;
		}

		public MyLine GetLineByIndex(int index)
		{
			return arr_lines[index];
		}

		public void AddLine(MyLine new_line)
		{
			last_line++;
			arr_lines.Add(new_line);
		}

		public void make_Polygon(Canvas Paint_canvas, Color color, int thickness)
		{
			Points.Clear();
			Points.Add(new Point(arr_lines[0].line.X1, arr_lines[0].line.Y1));
			for (int i = 1; i < arr_lines.Count; i++)
			{
				Points.Add(new Point(arr_lines[i].line.X2, arr_lines[i].line.Y2));
			}

			foreach (var line in arr_lines)
			{
				Paint_canvas.Children.Remove(line.GetFigure());
			}

			arr_lines.Clear();

			polygon = new Polygon
			{
				Points = new PointCollection(Points),
				Stroke = new SolidColorBrush(color),
				StrokeThickness = thickness
			};

			Paint_canvas.Children.Add(polygon);
		}

		public override bool IsPointInside(Point point)
		{
			int n = Points.Count;
			bool isInside = false;

			for (int i = 0, j = n - 1; i < n; j = i++)
			{
				Point vertex1 = Points[i];
				Point vertex2 = Points[j];

				if ((vertex1.Y > point.Y) != (vertex2.Y > point.Y) &&
					(point.X < (vertex2.X - vertex1.X) * (point.Y - vertex1.Y) / (vertex2.Y - vertex1.Y) + vertex1.X))
				{
					isInside = !isInside;
				}
			}

			return isInside;
		}

		public override void SetFillColor(Color color)
		{
			if (polygon != null)
				polygon.Fill = new SolidColorBrush(color);
		}

		public override void RemoveFigure(Canvas canvas)
		{
			if (polygon != null)
				canvas.Children.Remove(polygon);
		}
		private void InitializePolygon()
		{
			if (Points.Count == 0) return; // Если нет точек, нельзя создать полигон

			polygon = new Polygon
			{
				Points = new PointCollection(Points),
				Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString(StrokeColor),
				StrokeThickness = StrokeThickness
			};
		}

		public override void AddFigure(Canvas canvas)
		{
			if (polygon == null) InitializePolygon();
			canvas.Children.Add(polygon);
		}
	}
}
