namespace ConsoleApp2.Day12;

public class Field(Coordinate[] fieldCoordinates)
{
    private Coordinate[] Coordinates { get; } = fieldCoordinates;
    
    public int Area => Coordinates.Length;

    public int Perimeter
    {
        get
        {
            int perimeter = 0;
            foreach (var c in Coordinates)
            {
                Coordinate[] neighbors = c.GetNeighbors();
                
                int fences = neighbors.Count(c => !Coordinates.Contains(c));
                perimeter += fences;
            }
            
            return perimeter;
        }
    }

    public int SidesCount
    {
        get
        {
            int xMin = Coordinates.MinBy(c => c.X).X;
            int yMin = Coordinates.MinBy(c => c.Y).Y;
            int xMax = Coordinates.MaxBy(c => c.X).X;
            int yMax = Coordinates.MaxBy(c => c.Y).Y;
            
            return CountVerticalSides(xMin, xMax, yMin, yMax) + CountHorizontalSides(yMin, yMax, xMin, xMax);
        }
    }

    private int CountHorizontalSides(int yMin, int yMax, int xMin, int xMax)
    {
        int sidesCount = 0;
        for (int y = yMin; y <= yMax + 1; y++)
        {
            List<(bool top, bool bot)> pairs = [];
            for (int x = xMin; x <= xMax; x++)
            {
                bool top = Coordinates.Contains(new Coordinate(x, y - 1));
                bool bot = Coordinates.Contains(new Coordinate(x, y));
                pairs.Add((top, bot));
            }

            if (pairs[0].top != pairs[0].bot)
            {
                sidesCount++;
            }

            for (int i = 1; i < pairs.Count; i++)
            {
                if (pairs[i].top != pairs[i].bot && pairs[i] != pairs[i - 1])
                {
                    sidesCount++;
                }
            }
        }

        return sidesCount;
    }

    private int CountVerticalSides(int xMin, int xMax, int yMin, int yMax)
    {
        int sidesCount = 0;
        for (int x = xMin; x <= xMax + 1; x++)
        {
            List<(bool left, bool right)> pairs = [];
            for (int y = yMin; y <= yMax; y++)
            {
                bool left = Coordinates.Contains(new Coordinate(x - 1, y));
                bool right = Coordinates.Contains(new Coordinate(x, y));
                pairs.Add((left, right));
            }

            if (pairs[0].left != pairs[0].right)
            {
                sidesCount++;
            }

            for (int i = 1; i < pairs.Count; i++)
            {
                if (pairs[i].left != pairs[i].right && pairs[i] != pairs[i - 1])
                {
                    sidesCount++;
                }
            }
        }

        return sidesCount;
    }
}