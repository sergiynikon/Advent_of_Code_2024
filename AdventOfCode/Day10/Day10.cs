namespace ConsoleApp2.Day10;

public class Day10
{
    private const string FileName = "./../../../Day10/Input.txt";

    public int Part1()
    {
        string[] input = ReadInputFromFile(FileName);
        
        Field field = new Field(input);

        Coordinate[] startPositions = GetStartPositions(field);
        
        field.Draw(field, startPositions);

        Coordinate[][][] trailHeads = Trailheads(field, startPositions);

        return trailHeads.Sum(trailHead => trailHead.Length);
    }

    private Coordinate[][][] Trailheads(Field field, Coordinate[] startPositions)
    {
        List<Coordinate[][]> trailHeads = [];
        foreach (Coordinate position in startPositions)
        { 
            Coordinate[][] trailHead = TrailHeads(field, position);
            trailHeads.Add(trailHead);
        }

        return trailHeads.ToArray();
    }

    private Coordinate[][] TrailHeads(Field field, Coordinate startPosition)
    {
        Console.WriteLine($"start position: {startPosition}");
        List<Coordinate> currentTrail = [startPosition];
        List<Coordinate[]> succeededTrails = [];
        List<Coordinate> reachedEnds = [];

        TrailHeads(field, 0, currentTrail, succeededTrails, reachedEnds);
        
        return succeededTrails.ToArray();
    }

    private void TrailHeads(Field field, int currentNumber, List<Coordinate> currentTrail, List<Coordinate[]> succeededTrails, List<Coordinate> reachedEnds)
    {
        Coordinate position = currentTrail.Last();
        int nextNumber = currentNumber + 1;
        
        Coordinate topCoordinate = new Coordinate(position.X, position.Y - 1); 
        Coordinate bottomCoordinate = new Coordinate(position.X, position.Y + 1);
        Coordinate leftCoordinate = new Coordinate(position.X - 1, position.Y);
        Coordinate rightCoordinate = new Coordinate(position.X + 1, position.Y);
        
        Coordinate[] coordinates = new[] { topCoordinate, bottomCoordinate, leftCoordinate, rightCoordinate }
            .Where(c => field.IsInBounds(c) && field[c.Y][c.X] == nextNumber)
            .ToArray();

        foreach (var coordinate in coordinates)
        {
            currentTrail.Add(coordinate);
            
            if (currentTrail.Count == 10 /*&& !reachedEnds.Contains(coordinate)*/)
            {
                field.Draw(field, currentTrail.ToArray());
                reachedEnds.Add(currentTrail.Last());
                succeededTrails.Add(currentTrail.ToArray());
            }
            else
            {
                TrailHeads(field, currentNumber + 1, currentTrail, succeededTrails, reachedEnds);
            }
            
            currentTrail.RemoveAt(currentTrail.Count - 1);
        }
    }

    private Coordinate[] GetStartPositions(Field field)
    {
        List<Coordinate> startPosCoordinates = new();
        for (int y = 0; y < field.LengthY; y++)
        {
            for (int x = 0; x < field.LengthX; x++)
            {
                if (field[y][x] == 0)
                {
                    startPosCoordinates.Add(new Coordinate(x, y));
                }
            }
        }
        
        return startPosCoordinates.ToArray();
    }

    private string[] ReadInputFromFile(string filePath)
    {
        return File.ReadAllLines(filePath);
    }
}

public readonly record struct Coordinate(int X, int Y)
{
    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}

public class Field
{
    public Field(string[] lines)
    {
        Map = new int[lines.Length][];
        for (int y = 0; y < lines.Length; y++)
        {
            Map[y] = new int[lines[y].Length];
            for (int x = 0; x < lines[y].Length; x++)
            {
                Map[y][x] = lines[y][x] - '0';
            }
        }
    }
    
    public int[] this[int y] => Map[y];
    
    public int LengthY => Map.Length;
    
    public int LengthX => Map[0].Length;
    
    public int[][] Map { get; }

    public bool IsInBounds(Coordinate coordinate)
    {
        return coordinate.X >= 0 && coordinate.X < LengthX && coordinate.Y >= 0 && coordinate.Y < LengthY;
    }
    
    public void Draw(Field field, Coordinate[] highlights)
    {
        var oldColor = Console.ForegroundColor;
        for (int y = 0; y < field.LengthY; y++)
        {
            for (int x = 0; x < field.LengthX; x++)
            {
                Console.ForegroundColor = highlights.Contains(new Coordinate(x, y)) ? ConsoleColor.Green : ConsoleColor.Red;
                Console.Write(field[y][x]);
            }
            Console.WriteLine();
        }
        Console.ForegroundColor = oldColor;
        Console.WriteLine();
    }
}