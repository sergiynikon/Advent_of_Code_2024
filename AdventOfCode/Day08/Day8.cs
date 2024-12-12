namespace ConsoleApp2.Day8;

public class Day8
{
    private const string ExcludedSymbols = "#.";
    
    private const string FileName = "./../../../Day8/Input.txt";

    public int Part1()
    {
        string[] input = ReadInputFromFile(FileName);

        Dictionary<char, List<Coordinate>> antennasCoordinates = new();

        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                if (ExcludedSymbols.Contains(input[y][x]))
                {
                    continue;
                }
                
                if (!antennasCoordinates.TryGetValue(input[y][x], out List<Coordinate>? value))
                {
                    antennasCoordinates.Add(input[y][x], [new Coordinate(x, y)]);
                }
                else
                {
                    value.Add(new Coordinate(x, y));
                }
            }
        }
        
        HashSet<Coordinate> antinodesCoordinates = new();

        foreach (var keyValuePair in antennasCoordinates)
        {
            List<Coordinate> coordinates = keyValuePair.Value;

            for (int i = 0; i < coordinates.Count; i++)
            {
                for (int j = i + 1; j < coordinates.Count; j++)
                {
                    Coordinate c1 = coordinates[i];
                    Coordinate c2 = coordinates[j];

                    Coordinate antinode1 = 2 * c1 - c2;
                    Coordinate antinode2 = 2 * c2 - c1;

                    if (antinode1.IsInBounds(0, 0, input[j].Length - 1, input.Length - 1))
                    {
                        antinodesCoordinates.Add(antinode1);
                    }

                    if (antinode2.IsInBounds(0, 0, input[j].Length - 1, input.Length - 1))
                    {
                        antinodesCoordinates.Add(antinode2);
                    }
                }
            }
        }

        return antinodesCoordinates.Count;
    }
    
    public int Part2()
    {
        string[] input = ReadInputFromFile(FileName);

        Dictionary<char, List<Coordinate>> antennasCoordinates = new();

        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                if (ExcludedSymbols.Contains(input[y][x]))
                {
                    continue;
                }
                
                if (!antennasCoordinates.TryGetValue(input[y][x], out List<Coordinate>? value))
                {
                    antennasCoordinates.Add(input[y][x], [new Coordinate(x, y)]);
                }
                else
                {
                    value.Add(new Coordinate(x, y));
                }
            }
        }
        
        HashSet<Coordinate> antinodesCoordinates = new();
        
        foreach (var coordinates in antennasCoordinates.Select(keyValuePair => keyValuePair.Value))
        {
            for (int i = 0; i < coordinates.Count; i++)
            {
                antinodesCoordinates.Add(coordinates[i]);
                
                for (int j = i + 1; j < coordinates.Count; j++)
                {
                    Coordinate c1 = coordinates[i];
                    Coordinate c2 = coordinates[j];
                    
                    int idx1 = 1;
                    do
                    {
                        Coordinate antinode1 = (idx1 + 1) * c1 - idx1 * c2;
                        idx1++;
                        if (antinode1.IsInBounds(0, 0, input[j].Length - 1, input.Length - 1))
                        {
                            antinodesCoordinates.Add(antinode1);
                        }
                        else
                        {
                            break;
                        }
                    } while (true);
                    
                    int idx2 = 1;
                    do
                    {
                        Coordinate antinode2 = (idx2 + 1) * c2 - idx2 * c1;
                        idx2++;
                        if (antinode2.IsInBounds(0, 0, input[j].Length - 1, input.Length - 1))
                        {
                            antinodesCoordinates.Add(antinode2);
                        }
                        else
                        {
                            break;
                        }
                    } while (true);
                }
            }
        }

        return antinodesCoordinates.Count;
    }
    
    private string[] ReadInputFromFile(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);

        return lines;
    }
}

public readonly record struct Coordinate(int X, int Y)
{
    public static Coordinate operator -(Coordinate a, Coordinate b)
    {
        return new Coordinate(a.X - b.X, a.Y - b.Y);
    }

    public static Coordinate operator +(Coordinate a, Coordinate b)
    {
        return new Coordinate(a.X + b.X, a.Y + b.Y);
    }

    public static Coordinate operator *(Coordinate a, int num)
    {
        return new Coordinate(a.X * num, a.Y * num);
    }
    
    public static Coordinate operator *(int num, Coordinate a)
    {
        return new Coordinate(a.X * num, a.Y * num);
    }

    public bool IsInBounds(int x1, int y1, int x2, int y2)
    {
        return X >= x1 && X <= x2 && Y >= y1 && Y <= y2;
    }
}