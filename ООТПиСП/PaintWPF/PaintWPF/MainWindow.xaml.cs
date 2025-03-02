using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Xml.Serialization;
using System.Numerics;

namespace PaintWPF
{
	public partial class MainWindow : Window
	{
		enum Figure
		{
			FLine,
			FRectangle,
			FEllipse,
			FBrokenLine,
			FPolygon
		}

		private List<MyFigure> arr_figures = new List<MyFigure>();
		private bool isDrawing = false;
		private Figure chose_figure = Figure.FLine;
		private MyBrokenLine broken_line;
		private MyPolygon my_polygon;
		private int thickness = 1;
		private Color selectedColor;
		private bool selectedFillColor;

		public MainWindow()
		{
			InitializeComponent();
			this.MouseDown += MainWindow_MouseDown;
			this.MouseMove += MainWindow_MouseMove;
			this.MouseUp += MainWindow_MouseUp;
			this.KeyDown += MainWindow_KeyDown;
			LineButton.Click += LineButton_Click;
			RectangleButton.Click += RectangleButton_Click;
			EllipseButton.Click += EllipseButton_Click;
			BrokenLineButton.Click += BrokenLineButton_Click;
			PolygonButton.Click += PolygonButton_Click;
			selectedFillColor = false;
		}
		private void LineButton_Click(object sender, RoutedEventArgs e)
		{
			chose_figure = Figure.FLine;
		}
		private void RectangleButton_Click(object sender, RoutedEventArgs e)
		{
			chose_figure = Figure.FRectangle;
		}
		private void EllipseButton_Click(object sender, RoutedEventArgs e)
		{
			chose_figure = Figure.FEllipse;
		}
		private void BrokenLineButton_Click(object sender, RoutedEventArgs e)
		{
			chose_figure = Figure.FBrokenLine;
		}
		private void PolygonButton_Click(object sender, RoutedEventArgs e)
		{
			chose_figure = Figure.FPolygon;
		}
		private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (selectedFillColor) return;
			switch (chose_figure)
			{
				case Figure.FLine:
					{
						MyLine figure = new MyLine(e.GetPosition(Paint_canvas), selectedColor, thickness);
						Paint_canvas.Children.Add(figure.GetFigure());
						arr_figures.Add(figure);
						isDrawing = true;
						break;
					}
				case Figure.FRectangle:
					{
						MyRectangle figure = new MyRectangle(e.GetPosition(Paint_canvas), selectedColor, thickness);
						Paint_canvas.Children.Add(figure.GetFigure());
						arr_figures.Add(figure);
						isDrawing = true;
						break;
					}
				case Figure.FEllipse:
					{
						MyEllipse figure = new MyEllipse(e.GetPosition(Paint_canvas), selectedColor, thickness);
						Paint_canvas.Children.Add(figure.GetFigure());
						arr_figures.Add(figure);
						isDrawing = true;
						break;
					}
				case Figure.FBrokenLine:
					{
						if (broken_line == null) broken_line = new MyBrokenLine();
						MyLine line = new MyLine(e.GetPosition(Paint_canvas), selectedColor, thickness);
						broken_line.AddLine(line);
						Paint_canvas.Children.Add(broken_line.GetLineByIndex(broken_line.last_line).GetFigure());
						isDrawing = true;
						break;
					}
				case Figure.FPolygon:
					{
						if (my_polygon == null)
						{
							my_polygon = new MyPolygon(e.GetPosition(Paint_canvas), selectedColor, thickness);
							Paint_canvas.Children.Add(my_polygon.GetLineByIndex(0).GetFigure());
						}
						MyLine line = new MyLine(e.GetPosition(Paint_canvas), selectedColor, thickness);
						my_polygon.AddLine(line);
						Paint_canvas.Children.Add(my_polygon.GetLineByIndex(my_polygon.last_line).GetFigure());
						isDrawing = true;
						break;
					}
				default:
					break;
			}
		}

		private void MainWindow_MouseMove(object sender, MouseEventArgs e)
		{
			if (isDrawing)
			{
				switch (chose_figure)
				{
					case Figure.FLine:
					case Figure.FRectangle:
					case Figure.FEllipse:
						{
							arr_figures[arr_figures.Count - 1].Calc(e.GetPosition(Paint_canvas));
							break;
						}
					case Figure.FBrokenLine:
						{
							broken_line.GetLineByIndex(broken_line.last_line).Calc(e.GetPosition(Paint_canvas));
							break;
						}
					case Figure.FPolygon:
						{
							my_polygon.GetLineByIndex(my_polygon.last_line).Calc(e.GetPosition(Paint_canvas));
							my_polygon.GetLineByIndex(0).Calc(e.GetPosition(Paint_canvas));
							break;
						}
					default:
						break;
				}
			}
		}

		private void MainWindow_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (chose_figure == Figure.FLine || chose_figure == Figure.FRectangle || chose_figure == Figure.FEllipse) isDrawing = false;
		}

		private void MainWindow_KeyDown(object sender, KeyEventArgs e)
		{
			if ((chose_figure == Figure.FBrokenLine || chose_figure == Figure.FPolygon) && e.Key == Key.Escape)
			{
				if (chose_figure == Figure.FBrokenLine)
				{
					arr_figures.Add(broken_line);
					broken_line = null;
				}
				else
				{
					my_polygon.make_Polygon(Paint_canvas, selectedColor, thickness);
					arr_figures.Add(my_polygon);
					my_polygon = null;
				}
				isDrawing = false;
			}

			if (!isDrawing && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
			{
				if (e.Key == Key.Z)
				{
					arr_figures[arr_figures.Count-1].RemoveFigure(Paint_canvas);
					arr_figures.Remove(arr_figures[arr_figures.Count - 1]);
				}
			}
		}
		private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
		{
			if (e.NewValue.HasValue) // Проверяем, что цвет не null
			{
				selectedColor = e.NewValue.Value;
			}
		}
		private void SizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (SizeTextBox != null)  // Проверяем, что элемент инициализирован
			{
				thickness = (int)e.NewValue;
				SizeTextBox.Text = thickness.ToString();
			}
		}

		private void FillButton_Click(object sender, RoutedEventArgs e)
		{
			selectedFillColor = !selectedFillColor;
		}

		private void Paint_canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (selectedFillColor)
			{
				foreach (MyFigure fig in arr_figures)
				{
					if (fig.IsPointInside(e.GetPosition(Paint_canvas)))
					{
						fig.SetFillColor(selectedColor);
					}
				}
			}
		}
		private void SaveFigures(string filePath)
		{
			var options = new JsonSerializerOptions { WriteIndented = true };
			string json = JsonSerializer.Serialize(arr_figures, options);
			File.WriteAllText(filePath, json);
		}

		private void LoadFigures(string filePath)
		{
			if (!File.Exists(filePath)) return;

			string json = File.ReadAllText(filePath);
			arr_figures = JsonSerializer.Deserialize<List<MyFigure>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			Paint_canvas.Children.Clear(); // Очищаем холст

			foreach (var figure in arr_figures)
			{
				figure.AddFigure(Paint_canvas); // Добавляем фигуры обратно
			}
		}

		private void LoadButton_Click(object sender, RoutedEventArgs e)
		{
			var dialog = new Microsoft.Win32.OpenFileDialog();
			if (dialog.ShowDialog() == true)
			{
				string file_name = dialog.FileName;
				LoadFigures(file_name);
			}
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			var dialog = new Microsoft.Win32.SaveFileDialog();
			if (dialog.ShowDialog() == true)
			{
				string file_name = dialog.FileName;
				SaveFigures(file_name);
			}
		}
	}
}
