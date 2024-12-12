using System.Text;

namespace ConsoleApp2.Day6;

public class Day6
{
    private const string FileName = "./../../../Day6/Input.txt";

    public int Part1()
    {
        string[] field = ReadInputFromFile(FileName);
        
        HashSet<Coordinate> visitedCoords = [];

        Coordinate guardCoordinate = FindGuardCoordinate(field);
        Guard1 guard1 = new(field, guardCoordinate, Direction.Up);

        while (!guard1.IsOutsideOfBounds())
        {
            visitedCoords.Add(guard1.Coordinate);
            guard1.TryMove(out bool isMoved);
            if (!isMoved)
            {
                guard1.TurnRight();
            }
        }
        
        return visitedCoords.Count;
    }
    
    public int Part2()
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        string[] field = ReadInputFromFile(FileName);
        
        HashSet<Coordinate> visitedCoords = [];

        Coordinate guardCoordinate = FindGuardCoordinate(field);
        Guard2 guard2 = new(field, guardCoordinate, Direction.Up);
        
        while (!guard2.IsOutsideOfBounds())
        {
            visitedCoords.Add(guard2.Coordinate);
            guard2.TryMove(out bool isMoved);
            if (!isMoved)
            {
                guard2.TurnRight();
            }
        }
        
        int result = 0;

        foreach (Coordinate coordinate in visitedCoords)
        {
            char currentChar = field[coordinate.Y][coordinate.X];
            StringBuilder temp = new(field[coordinate.Y])
            {
                [coordinate.X] = '#'
            };
                
            field[coordinate.Y] = temp.ToString();
                
            guard2 = new Guard2(field, guardCoordinate, Direction.Up);
                
            while (!guard2.IsOutsideOfBounds())
            {
                if (guard2.IsLooped)
                {
                    result++;
                    break;
                }
                guard2.TryMove(out bool isMoved);
                if (!isMoved)
                {
                    guard2.TurnRight();
                }
            }
                
            temp[coordinate.X] = currentChar;
                
            field[coordinate.Y] = temp.ToString();
        }
        
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        Console.WriteLine(elapsedMs);
        
        return result;
    }
    
    
    private string[] ReadInputFromFile(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        
        return lines;
    }

    private static Coordinate FindGuardCoordinate(string[] lines)
    {
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (lines[y][x] == '^')
                {
                    return new Coordinate(x, y);
                }
            }
        }
        
        return new Coordinate(-1, -1);
    }
}

public readonly record struct Coordinate(int x, int y)
{
    public int X { get; init; } = x;
    public int Y { get; init; } = y;

    public override string ToString() => $"({X}, {Y})";   
}

public enum Direction
{
    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3
}

public class Guard1(string[] field, Coordinate coordinate, Direction direction)
{
    private Direction Direction { get; set; } = direction;
    public Coordinate Coordinate { get; private set; } = coordinate;

    public void PrintField()
    {
        Console.Clear();
        for (int y = 0; y < field.Length; y++)
        {
            for (int x = 0; x < field[y].Length; x++)
            {
                if (field[y][x] == '^')
                {
                    Console.Write('.');
                }
                else if (Coordinate.X == x && Coordinate.Y == y)
                {
                    Console.Write('O');
                }
                else
                {
                    Console.Write(field[y][x]);
                }
            }
            Console.WriteLine();
        }
    }

    public bool IsOutsideOfBounds()
    {
        return Coordinate.X < 0 || Coordinate.Y < 0 || Coordinate.X >= field.Length || Coordinate.Y >= field.Length;
    }

    private bool IsOutsideOfBounds(Coordinate coordinate)
    {
        return coordinate.X < 0 || coordinate.Y < 0 || coordinate.X >= field.Length || coordinate.Y >= field.Length;
    }

    public void TurnRight()
    {
        Direction = Direction switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => Direction
        };
    }

    public void TryMove(out bool success)
    {
        Coordinate nextCoordinate = Direction switch
        {
            Direction.Up => new Coordinate(Coordinate.X, Coordinate.Y - 1),
            Direction.Right => new Coordinate(Coordinate.X + 1, Coordinate.Y),
            Direction.Down => new Coordinate(Coordinate.X, Coordinate.Y + 1),
            Direction.Left => new Coordinate(Coordinate.X - 1, Coordinate.Y),
            _ => Coordinate
        };

        if (IsOutsideOfBounds(nextCoordinate))
        {
            success = true;
        }
        else
        {
            success = field[nextCoordinate.Y][nextCoordinate.X] != '#';
        }
        
        Coordinate = success ? nextCoordinate : Coordinate;
    }
}

public class Guard2(string[] field, Coordinate coordinate, Direction direction)
{
    private Dictionary<Coordinate, int> GuardTurningCoordinatesEntrances { get; init; } = new();
    private Direction Direction { get; set; } = direction;
    public Coordinate Coordinate { get; private set; } = coordinate;

    public void PrintField()
    {
        Console.Clear();
        for (int y = 0; y < field.Length; y++)
        {
            for (int x = 0; x < field[y].Length; x++)
            {
                if (field[y][x] == '^')
                {
                    Console.Write('.');
                }
                else if (Coordinate.X == x && Coordinate.Y == y)
                {
                    Console.Write('O');
                }
                else
                {
                    Console.Write(field[y][x]);
                }
            }
            Console.WriteLine();
        }
    }

    public bool IsOutsideOfBounds()
    {
        return Coordinate.X < 0 || Coordinate.Y < 0 || Coordinate.X >= field.Length || Coordinate.Y >= field.Length;
    }

    private bool IsOutsideOfBounds(Coordinate coordinate)
    {
        return coordinate.X < 0 || coordinate.Y < 0 || coordinate.X >= field.Length || coordinate.Y >= field.Length;
    }

    public void TurnRight()
    {
        Direction = Direction switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => Direction
        };

        if (!GuardTurningCoordinatesEntrances.TryAdd(Coordinate, 1))
        {
            GuardTurningCoordinatesEntrances[Coordinate]++;
        }
    }

    public bool IsLooped => GuardTurningCoordinatesEntrances.Any(pair => pair.Value > 2);

    public void TryMove(out bool success)
    {
        Coordinate nextCoordinate = Direction switch
        {
            Direction.Up => new Coordinate(Coordinate.X, Coordinate.Y - 1),
            Direction.Right => new Coordinate(Coordinate.X + 1, Coordinate.Y),
            Direction.Down => new Coordinate(Coordinate.X, Coordinate.Y + 1),
            Direction.Left => new Coordinate(Coordinate.X - 1, Coordinate.Y),
            _ => Coordinate
        };

        if (IsOutsideOfBounds(nextCoordinate))
        {
            success = true;
        }
        else
        {
            success = field[nextCoordinate.Y][nextCoordinate.X] != '#';
        }

        if (success)
        {
            Coordinate = nextCoordinate;
        }
    }
}