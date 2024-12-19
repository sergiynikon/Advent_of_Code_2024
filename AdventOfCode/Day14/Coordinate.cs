namespace ConsoleApp2.Day14;

public record struct Coordinate(int X, int Y)
{
    public override string ToString()
    {
        return $"({X}, {Y})";
    }

    public static Coordinate operator +(Coordinate a, Coordinate b)
    {
        return new Coordinate(a.X + b.X, a.Y + b.Y);
    }

    public static Coordinate operator -(Coordinate a, Coordinate b)
    {
        return new Coordinate(a.X - b.X, a.Y - b.Y);
    }

    public bool IsInsideBorders(Square borders)
    {
        return X >= borders.XMin && X <= borders.XMax && Y >= borders.YMin && Y <= borders.YMax;
    }
}