namespace ConsoleApp2.Day2;

public class Day2
{
    private const string FileName = "./../../../Day2/Input.txt";

    public int Part2()
    {
        return ReadInputFromFile(FileName).Count(row => IsRecordSafe(row) || row.Select((_, i) => row.Where((_, j) => i != j).ToList()).Any(IsRecordSafe));
    }

    private bool IsRecordSafe(List<int> row)
    {
        if (row.Count < 2)
        {
            return true;
        }
        
        int initialOrder =  row[0].CompareTo(row[1]);
        for (int i = 1; i < row.Count; i++)
        {
            if (row[i - 1].CompareTo(row[i]) != initialOrder 
                || Math.Abs(row[i - 1] - row[i]) <= 0 
                || Math.Abs(row[i - 1] - row[i]) > 3)
            {
                return false;
            }
        }
        
        return true;
    }
    
    public int Part1()
    {
        List<List<int>> data = ReadInputFromFile(FileName);
        int safeRecordsCount = 0;

        foreach (List<int> row in data)
        {
            bool isRecordSafe = true;
            int initialOrder =  row[0].CompareTo(row[1]);
            for (int i = 1; i < row.Count; i++)
            {
                if (row[i - 1].CompareTo(row[i]) != initialOrder 
                    || Math.Abs(row[i - 1] - row[i]) <= 0 
                    || Math.Abs(row[i - 1] - row[i]) > 3)
                {
                    isRecordSafe = false;
                    break;
                }
            }

            if (isRecordSafe)
            {
                safeRecordsCount++;
            }
        }

        return safeRecordsCount;
    }
    
    private List<List<int>> ReadInputFromFile(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        
        return lines.Select(line => line.Split(" ")).Select(line => line.Select(int.Parse).ToList()).ToList();
    }
}