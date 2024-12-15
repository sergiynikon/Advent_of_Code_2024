namespace ConsoleApp2.Day12;

public class Map(char[,] map)
{
    public Coordinate TopLeft { get; } = new(0, 0);

    public Coordinate BottomRight { get; } = new(map.GetLength(1) - 1, map.GetLength(0) - 1);

    public Field[] GetFieldsFromMap()
    {
        List<Field> fields = [];

        bool[,] visited = new bool[map.GetLength(1), map.GetLength(0)];
        
        for (int i = 0; i < map.GetLength(1); i++)
        {
            for (int j = 0; j < map.GetLength(0); j++)
            {
                if (visited[i, j] == false)
                {
                    Coordinate[] fieldCoordinates = GetField(new Coordinate(i, j), visited);
                    
                    fields.Add(new Field(fieldCoordinates));
                }
            }
        }
        
        return fields.ToArray();
    }

    private Coordinate[] GetField(Coordinate start, bool[,] visited)
    {
        HashSet<Coordinate> fieldCoordinates = new();

        char plantType = map[start.X, start.Y];
        GetFieldRecursive(start, plantType, visited, fieldCoordinates);
        
        return fieldCoordinates.ToArray();
    }

    private void GetFieldRecursive(Coordinate current, char plantType, bool[,] visited, HashSet<Coordinate> fieldCoordinates)
    {
        if (map[current.X, current.Y] != plantType || visited[current.X, current.Y])
        {
            return;
        }
        
        fieldCoordinates.Add(current);
        visited[current.X, current.Y] = true;
        
        Coordinate[] neighbors = current.GetNeighbors(TopLeft, BottomRight);

        foreach (Coordinate neighbor in neighbors)
        {
            GetFieldRecursive(neighbor, plantType, visited, fieldCoordinates);
        }
    }
}