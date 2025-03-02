using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PaintWPF
{
	public class MyLine : MyFigure
	{
		public double X1 { get; set; }
		public double Y1 { get; set; }
		public double X2 { get; set; }
		public double Y2 { get; set; }

		public string StrokeColor { get; set; }
		public int StrokeThickness { get; set; }

		[JsonIgnore] // 🔹 Исключаем `line` из сериализации, т.к. его можно восстановить
		public Line line;

		// 🔹 Конструктор без параметров для JSON-десериализации
		public MyLine() { }

		public MyLine(Point startPoint, Color color, int thickness)
		{
			X1 = startPoint.X;
			Y1 = startPoint.Y;
			X2 = startPoint.X;
			Y2 = startPoint.Y;
			StrokeColor = color.ToString(); // Сохраняем цвет в строковом формате
			StrokeThickness = thickness;
			InitializeLine(); // Создаём фигуру
		}

		// 🔹 Метод для создания линии на основе свойств
		private void InitializeLine()
		{
			line = new Line
			{
				X1 = X1,
				Y1 = Y1,
				X2 = X2,
				Y2 = Y2,
				StrokeThickness = StrokeThickness,
				Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString(StrokeColor) // Восстанавливаем цвет
			};
		}

		public override void Calc(Point newPoint)
		{
			X2 = newPoint.X;
			Y2 = newPoint.Y;
			if (line != null)
			{
				line.X2 = X2;
				line.Y2 = Y2;
			}
		}

		public Line GetFigure()
		{
			if (line == null) InitializeLine(); // Если `line` не был создан, создаём его
			return line;
		}

		public override bool IsPointInside(Point point)
		{
			// Можно добавить проверку, например, на расстояние до линии
			return false;
		}

		public override void SetFillColor(Color color)
		{
			// Линии не поддерживают заливку
		}

		public override void RemoveFigure(Canvas canvas)
		{
			canvas.Children.Remove(line);
		}

		public override void AddFigure(Canvas canvas)
		{
			if (line == null) InitializeLine(); // Гарантируем, что объект создан
			canvas.Children.Add(line);
		}
	}
}
