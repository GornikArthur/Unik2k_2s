using Lab7.models;

namespace Lab7;

public partial class Form1 : Form
{
    private PointF? startPoint = null;
    private List<Line> lines = new List<Line>();
    private const int MaxNeighborsDistance = 5;

    public Form1()
    {
        InitializeComponent();
    }

    private void p_canvas_MouseDown(object sender, MouseEventArgs e)
    {
        if (startPoint.HasValue)
        {
            lines.Add(GetLine(startPoint.Value, e.Location));
            p_canvas.Invalidate();
            startPoint = null;
        }
        else
        {
            startPoint = e.Location;
        }
    }

    private Line GetLine(PointF point1, PointF point2)
    {
        LineType lineType;
        double deltaX = point1.X - point2.X;
        double deltaY = point1.Y - point2.Y;
        // определяем направление линии
        if (Math.Abs(deltaY) < 1)
        {
            lineType = LineType.Horizontal;
        }
        else if (Math.Abs(deltaX) < 1)
        {
            lineType = LineType.Vertical;
        }
        else if (Math.Abs(deltaX / deltaY) < 0.2)
        {
            lineType = LineType.Vertical;
        }
        else if (Math.Abs(deltaY / deltaX) < 0.2)
        {
            lineType = LineType.Horizontal;
        }
        else
        {
            PointF highPoint = point1.Y > point2.Y ? point1 : point2;
            PointF lowPoint = point1.Y < point2.Y ? point1 : point2;
            // из-за перевернутых координат знак сравнения противоположный
            lineType = highPoint.X > lowPoint.X ? LineType.Left45 : LineType.Right45;
        }

        Line newLine = new Line(lineType, point1, point2);
        // находим соседей
        newLine.Neighbors = FindNeighbors(newLine);

        // каждому соседу добавляем новую линию
        foreach (var line in newLine.Neighbors)
        {
            line.Neighbors.Add(newLine);
        }

        return newLine;
    }

    private List<Line> FindNeighbors(Line newLine) =>
        lines.Where(line => GetDistance(newLine, line) < MaxNeighborsDistance).ToList();

    private double GetDistance(Line line1, Line line2) =>
        Math.Sqrt(Math.Pow(line1.Start.X - line2.Start.X, 2) + Math.Pow(line1.Start.Y - line2.Start.Y, 2));

    private void p_canvas_Paint(object sender, PaintEventArgs e)
    {
        foreach (var line in lines)
            e.Graphics.DrawLine(Pens.Black, line.Start, line.End);
    }

    private void btn_clear_Click(object sender, EventArgs e)
    {
        lines.Clear();
        p_canvas.Invalidate();
    }

    private void btn_generate_Click(object sender, EventArgs e)
    {
        lines = GenerateCandy();
        // Обновим холст
        p_canvas.Invalidate();
    }

    private void btn_validate_Click(object sender, EventArgs e)
    {
        bool isCandy = CandyDetector.IsCandy(lines);
        MessageBox.Show(isCandy ? "Изображение похоже на конфету!" : "Изображение не похоже на конфету!");
    }

    private List<Line> GenerateCandy()
    {
        List<Line> candyLines = new List<Line>();

        float centerX = p_canvas.Width / 2;
        float centerY = p_canvas.Height / 2;
        float size = 80; // размер квадрата
        float triangleSize = 40; // длина "лепестков"-треугольников

        // ===== Центр квадрата =====
        PointF topLeft = new PointF(centerX - size / 2, centerY - size / 2);
        PointF topRight = new PointF(centerX + size / 2, centerY - size / 2);
        PointF bottomLeft = new PointF(centerX - size / 2, centerY + size / 2);
        PointF bottomRight = new PointF(centerX + size / 2, centerY + size / 2);

        Line top = new Line(LineType.Horizontal, topLeft, topRight);
        Line bottom = new Line(LineType.Horizontal, bottomLeft, bottomRight);
        Line left = new Line(LineType.Vertical, topLeft, bottomLeft);
        Line right = new Line(LineType.Vertical, topRight, bottomRight);

        candyLines.AddRange([top, bottom, left, right]);

        // ===== Левый треугольник (<) =====
        PointF leftMid = new PointF(topLeft.X - triangleSize, centerY);

        Line right45OfLT = new Line(LineType.Right45, leftMid, topLeft);
        Line left45OfLT = new Line(LineType.Left45, leftMid, bottomLeft);

        candyLines.AddRange([right45OfLT, left45OfLT]);

        // ===== Правый треугольник (>) =====
        PointF rightMid = new PointF(topRight.X + triangleSize, centerY);

        Line right45OfRT = new Line(LineType.Right45, bottomRight, rightMid);
        Line left45OfRT = new Line(LineType.Left45, topRight, rightMid);

        candyLines.AddRange([right45OfRT, left45OfRT]);

        // ===== Привязываем соседей =====
        foreach (var line in candyLines)
        {
            line.Neighbors = candyLines
                .Where(other => other != line && GetDistance(line, other) < MaxNeighborsDistance)
                .ToList();
        }

        foreach (var line in candyLines)
        {
            foreach (var neighbor in line.Neighbors)
            {
                if (!neighbor.Neighbors.Contains(line))
                    neighbor.Neighbors.Add(line);
            }
        }

        p_canvas.Invalidate();
        return candyLines;
    }

}