namespace ConsoleApp2.Day12;

public class Day12
{
    private const string FileName = "./../../../Day12/Input.txt";

    public int Part1()
    {
        char[,] input = ReadInputFromFile(FileName);

        Map map = new Map(input);

        Field[] fields = map.GetFieldsFromMap();

        return fields.Sum(f => f.Area * f.Perimeter);
    }
    
    public int Part2()
    {
        char[,] input = ReadInputFromFile(FileName);

        Map map = new Map(input);

        Field[] fields = map.GetFieldsFromMap();

        return fields.Sum(f => f.Area * f.SidesCount);
    }
    
    private char[,] ReadInputFromFile(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        
        char[,] output = new char[lines.Length, lines[0].Length];

        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (var x = 0; x < line.Length; x++)
            {
                var c = line[x];
                output[x, y] = lines[y][x];
            }
        }
        
        return output;
    }
}