namespace ConsoleApp2.Day1;

// 1 2 3 3 3 4
// 3 3 3 4 5 9
public class Day1
{
    private const string FileName = "./../../../Day1/Input.txt";

    public int Part2()
    {
        var (left, right) = ReadInputFromFile(FileName);

        Dictionary<int, int> rDict = new();

        foreach (var item in right.Where(item => !rDict.TryAdd(item, 1)))
        {
            rDict[item]++;
        }

        return left.Where(t => rDict.ContainsKey(t)).Sum(t => t * rDict[t]);
    }
    
    public int CalculateDistances()
    {
        var (left, right) = ReadInputFromFile(FileName);

        left = left.Order().ToList();
        right = right.Order().ToList();

        return left.Select((t, i) => Math.Abs(t - right[i])).Sum();
    }

    private (List<int> left, List<int> right) ReadInputFromFile(string filePath)
    {
        List<int> leftList = new();
        List<int> rightList = new();

        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var left = int.Parse(parts[0]);
            var right = int.Parse(parts[1]);
            leftList.Add(left);
            rightList.Add(right);
        }
        
        return (leftList, rightList);
    }
}