using Lab7.models;

namespace Lab7;

public static class CandyDetector
{
    /*public static bool IsCandy(List<Line> lines)
    {
        // находим правый треугольник
        List<Line> r45 = lines.Where(line => line.LineType == LineType.RightDiagonal).ToList();
        foreach (var line in r45)
        {
            List<Line> horizontalLines = line.Neighbors
                .FindAll(neighbor => neighbor is Line {LineType: LineType.Horizaontal})
                .Cast<Line>()
                .ToList();
            if (horizontalLines.Count > 1)
                return false;
            
        }


        // находим левый треугольник
    }

    private static Geometric? GetLeftTriangle(List<Line> lines)
    {
        List<Line> r45Lines = lines.Where(line => line.LineType == LineType.RightDiagonal).ToList();
        
        if (r45Lines.Count == 0)
            return null;
        
            
        foreach (var r45 in r45Lines)
        {
            // находим всех соседние горизонтальные линии
            List<Line> hLines = FindNeighbors<Line>(r45.Neighbors, IsHorizontalLine);
            if (hLines.Count != 1)
                return null;
            Line h = hLines[0];
            // находим у горизонтальной линии l45
            List<Line> l45Lines = FindNeighbors<Line>(h.Neighbors, IsLeftDiagonalLine);
            if (hLines.Count != 1)
                return null;
            Line l45 = l45Lines[0];
            // у l45 находим соседа, который является нашей же r45
            Geometric? copyOfR45 = l45.Neighbors.Find(neighbor => neighbor.Equals(r45));
            if (copyOfR45 == null)
                return null;
            
        }
    }

    private static List<T> FindNeighbors<T>(List<Geometric> lines, Func<Geometric,bool> predicate)
    {
        return lines.Where(predicate).Cast<T>().ToList();
    }

    private static bool IsHorizontalLine(Geometric geometric)
    {
        return geometric is Line {LineType: LineType.Horizaontal};
    }

    private static bool IsLeftDiagonalLine(Geometric geometric)
    {
        return geometric is Line {LineType: LineType.LeftDiagonal};
    }*/

    private static Line? _currentLine;
    private static readonly HashSet<Line> Traversed = new HashSet<Line>();

    public static bool IsCandy(List<Line> lines)
    {
        Traversed.Clear();
        _currentLine = new Line(LineType.None, new PointF(0,0), new PointF(0,0)) {Neighbors = lines};
        return IsCandyPart() && IsCandyPart();
    }

    private static bool IsCandyPart()
    {
        return Validate(LineType.Horizontal) 
            && Validate(LineType.Left45)
            && Validate(LineType.Right45) 
            && Validate(LineType.Vertical);
    }

    private static bool Validate(LineType expectedType)
    {
        Line? findLine = _currentLine?.Neighbors.FirstOrDefault(line => !Traversed.Contains(line) && line.LineType == expectedType) ?? null;
        if (findLine is not null)
        {
            Traversed.Add(findLine);
            return true;
        }

        return false;
    }
}