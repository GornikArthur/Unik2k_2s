using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PaintWPF
{
	[JsonDerivedType(typeof(MyLine), typeDiscriminator: "Line")]
	[JsonDerivedType(typeof(MyRectangle), typeDiscriminator: "Rectangle")]
	[JsonDerivedType(typeof(MyEllipse), typeDiscriminator: "Ellipse")]
	[JsonDerivedType(typeof(MyBrokenLine), typeDiscriminator: "BrokenLine")]
	[JsonDerivedType(typeof(MyPolygon), typeDiscriminator: "Polygon")]
	public abstract class MyFigure
	{
		public abstract void Calc(Point newPoint);
		public abstract bool IsPointInside(Point point);
		public abstract void SetFillColor(Color color);
		public abstract void RemoveFigure(Canvas canvas);
		public abstract void AddFigure(Canvas canvas);
		public abstract void MouseMove(Point pos, Canvas Paint_canvas, List<MyFigure> arr_figures);
	}
}
