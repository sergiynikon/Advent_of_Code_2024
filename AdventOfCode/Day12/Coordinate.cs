namespace ConsoleApp2.Day12;

public readonly record struct Coordinate(int X, int Y)
{
    public override string ToString()
    {
        return $"({X}, {Y})";
    }

    public Coordinate GetLeftNeighbor()
    {
        return this with { X = X - 1 };
    }

    public Coordinate GetRightNeighbor()
    {
        return this with { X = X + 1 };
    }

    public Coordinate GetTopNeighbor()
    {
        return this with { Y = Y - 1 };
    }

    public Coordinate GetBottomNeighbor()
    {
        return this with { Y = Y + 1 };
    }

    public Coordinate[] GetHorizontalNeighbors()
    {
        return [GetLeftNeighbor(), GetRightNeighbor()];
    }

    public Coordinate[] GetVerticalNeighbors()
    {
        return [GetTopNeighbor(), GetBottomNeighbor()];
    }

    public Coordinate[] GetNeighbors()
    {
        return GetHorizontalNeighbors().Union(GetVerticalNeighbors()).ToArray();
    }

    public Coordinate[] GetNeighbors(Coordinate topLeftBorder, Coordinate bottomRightBorder)
    {
        return GetNeighbors().Where(c => c.IsInsideBorders(topLeftBorder, bottomRightBorder)).ToArray();
    }

    private bool IsInsideBorders(Coordinate topLeftBorder, Coordinate bottomRightBorder)
    {
        return X >= topLeftBorder.X && X <= bottomRightBorder.X && Y >= topLeftBorder.Y && Y <= bottomRightBorder.Y;
    }
}