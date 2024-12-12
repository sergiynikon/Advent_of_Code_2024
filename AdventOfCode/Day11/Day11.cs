namespace ConsoleApp2.Day11;

public class Day11
{
    private const string FileName = "./../../../Day11/Input.txt";

    public long Part2()
    {
        int blinksNumber = 75;
        List<long> stones = ReadInputFromFile(FileName);
        Dictionary<long, long> currentNumbers = new();
        stones.ForEach(stone => AddToDictionary(currentNumbers, stone, 1));
        
        for (int i = 0; i < blinksNumber; i++)
        {
            var previousNumbers = currentNumbers.ToDictionary(pair => pair.Key, pair => pair.Value);
            currentNumbers = currentNumbers.ToDictionary(pair => pair.Key, pair => 0L);
            
            foreach (KeyValuePair<long, long> pair in previousNumbers.Where(pair => pair.Value != 0))
            {
                Func(pair.Key).ForEach(key => AddToDictionary(currentNumbers, key, pair.Value));;
            }
        }
        
        return currentNumbers.Sum(pair => pair.Value);
    }

    private List<long> Func(long value)
    {
        if (value == 0)
        {
            return [1];
        }
        if (value.ToString().Length % 2 == 0)
        {
            string numberStr = value.ToString();
            long left = long.Parse(numberStr[..(numberStr.Length / 2)]);
            long right = long.Parse(numberStr[(numberStr.Length / 2)..]);

            return [left, right];
        }

        return [value * 2024];
    }

    private void AddToDictionary(Dictionary<long, long> currentNumbers, long key, long value)
    {
        if (!currentNumbers.TryAdd(key, value))
        {
            currentNumbers[key] += value;
        }
    }

    public int Part1()
    {
        int blinksNumber = 25;
        
        List<long> stones = ReadInputFromFile(FileName);
        LinkedList<long> stonesList = new LinkedList<long>(stones);
        Console.WriteLine(string.Join(" ", stonesList));

        for (int i = 0; i < blinksNumber; i++)
        {
            for (LinkedListNode<long>? node = stonesList.First; node != null; node = node.Next)
            {
                if (node.Value == 0)
                {
                    node.Value = 1;
                }
                else if (node.Value.ToString().Length % 2 == 0)
                {
                    string numberStr = node.Value.ToString();
                    long left = long.Parse(numberStr.Substring(0, numberStr.Length / 2));
                    long right = long.Parse(numberStr.Substring(numberStr.Length / 2));
                    stonesList.AddBefore(node, new LinkedListNode<long>(left));
                    stonesList.AddBefore(node, new LinkedListNode<long>(right));
                    node = node.Previous;
                    stonesList.Remove(node!.Next!);
                }
                else
                {
                    node.Value *= 2024;
                }
            }
            Console.WriteLine($"Iter {i + 1}: {string.Join(" ", stonesList)}");
        }
        
        return stonesList.Count;
    }
    
    private List<long> ReadInputFromFile(string filePath)
    {
        var text = File.ReadAllText(filePath);
        
        return text.Split(" ").Select(long.Parse).ToList();
    }
}