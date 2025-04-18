namespace Lab7.models;

public class Line
{
    public readonly LineType LineType = LineType.None;
    public PointF Start;
    public PointF End;
    public List<Line> Neighbors;

    public Line(LineType lineType, PointF start, PointF end)
    {
        LineType = lineType;
        Start = start;
        End = end;
        Neighbors = new List<Line>();
    }
}

public enum LineType
{
    None,
    Vertical,
    Horizontal,
    Left45,
    Right45,
}