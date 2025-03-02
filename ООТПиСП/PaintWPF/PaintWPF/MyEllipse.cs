using System;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PaintWPF
{
	public class MyEllipse : MyFigure
	{
		public double X { get; set; }
		public double Y { get; set; }
		public double Width { get; set; }
		public double Height { get; set; }
		public string StrokeColor { get; set; }
		public int StrokeThickness { get; set; }
		public string FillColor { get; set; } = "Transparent";

		[JsonIgnore]
		private Ellipse ellip;

		public MyEllipse() { }

		public MyEllipse(Point startPoint, Color color, int thickness)
		{
			X = startPoint.X;
			Y = startPoint.Y;
			Width = 0;
			Height = 0;
			StrokeColor = color.ToString();
			StrokeThickness = thickness;
			InitializeEllipse();
		}

		private void InitializeEllipse()
		{
			ellip = new Ellipse
			{
				Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString(StrokeColor),
				StrokeThickness = StrokeThickness,
				Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(FillColor),
				Width = Width,
				Height = Height
			};
			Canvas.SetLeft(ellip, X);
			Canvas.SetTop(ellip, Y);
		}

		public override void Calc(Point newPoint)
		{
			X = Math.Min(X, newPoint.X);
			Y = Math.Min(Y, newPoint.Y);
			Width = Math.Abs(newPoint.X - X);
			Height = Math.Abs(newPoint.Y - Y);

			if (ellip != null)
			{
				Canvas.SetLeft(ellip, X);
				Canvas.SetTop(ellip, Y);
				ellip.Width = Width;
				ellip.Height = Height;
			}
		}

		public Ellipse GetFigure()
		{
			if (ellip == null) InitializeEllipse();
			return ellip;
		}

		public override bool IsPointInside(Point point)
		{
			double centerX = X + Width / 2;
			double centerY = Y + Height / 2;
			double a = Width / 2;
			double b = Height / 2;

			double distance = (Math.Pow(point.X - centerX, 2) / Math.Pow(a, 2)) + (Math.Pow(point.Y - centerY, 2) / Math.Pow(b, 2));
			return distance < 1;
		}

		public override void SetFillColor(Color color)
		{
			FillColor = color.ToString();
			if (ellip != null)
			{
				ellip.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(FillColor);
			}
		}

		public override void RemoveFigure(Canvas canvas)
		{
			canvas.Children.Remove(ellip);
		}

		public override void AddFigure(Canvas canvas)
		{
			if (ellip == null) InitializeEllipse();
			canvas.Children.Add(ellip);
		}
	}
}
