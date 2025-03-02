using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PaintWPF
{
	public class MyRectangle : MyFigure
	{
		public double X { get; set; }
		public double Y { get; set; }
		public double Width { get; set; }
		public double Height { get; set; }
		public string StrokeColor { get; set; }
		public int StrokeThickness { get; set; }
		public string FillColor { get; set; } = "Transparent";

		[JsonIgnore] // Исключаем объект WPF, так как он не сериализуемый
		private Rectangle rect;

		public MyRectangle() { }

		public MyRectangle(Point startPoint, Color color, int thickness)
		{
			X = startPoint.X;
			Y = startPoint.Y;
			Width = 0;
			Height = 0;
			StrokeColor = color.ToString();
			StrokeThickness = thickness;
			InitializeRectangle();
		}

		private void InitializeRectangle()
		{
			rect = new Rectangle
			{
				Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString(StrokeColor),
				StrokeThickness = StrokeThickness,
				Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(FillColor),
				Width = Width,
				Height = Height

			};
			Canvas.SetLeft(rect, X);
			Canvas.SetTop(rect, Y);
		}

		public override void Calc(Point newPoint)
		{
			X = Math.Min(X, newPoint.X);
			Y = Math.Min(Y, newPoint.Y);
			Width = Math.Abs(newPoint.X - X);
			Height = Math.Abs(newPoint.Y - Y);

			if (rect != null)
			{
				Canvas.SetLeft(rect, X);
				Canvas.SetTop(rect, Y);
				rect.Width = Width;
				rect.Height = Height;
			}
		}

		public Rectangle GetFigure()
		{
			if (rect == null) InitializeRectangle();
			return rect;
		}

		public override bool IsPointInside(Point point)
		{
			return point.X >= X && point.X <= X + Width && point.Y >= Y && point.Y <= Y + Height;
		}

		public override void SetFillColor(Color color)
		{
			FillColor = color.ToString();
			if (rect != null)
			{
				rect.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(FillColor);
			}
		}

		public override void RemoveFigure(Canvas canvas)
		{
			canvas.Children.Remove(rect);
		}

		public override void AddFigure(Canvas canvas)
		{
			if (rect == null) InitializeRectangle();
			canvas.Children.Add(rect);
		}
	}
}
