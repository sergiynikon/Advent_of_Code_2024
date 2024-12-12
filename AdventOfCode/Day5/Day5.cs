namespace ConsoleApp2.Day5;

public class Day5
{
    private const string FileName = "./../../../Day5/Input.txt";
    
    public int Part1()
    {
        (List<(int, int)> rules, List<List<int>> pageNumbers) = ReadInputFromFile(FileName);

        int result = 0;

        foreach (var line in pageNumbers)
        {
            bool isValid = true;
            for (int i = 0; i < line.Count - 1; i++)
            {
                if (isValid)
                {
                    for (int j = i + 1; j < line.Count; j++)
                    {
                        if (rules.Contains((line[j], line[i])))
                        {
                            isValid = false;
                            break;
                        }
                    }
                }
            }

            if (isValid)
            {
                int centralElement = line[line.Count / 2];
                result += centralElement;
            }
        }
        
        return result;
    }
    
    public int Part2()
    {
        (List<(int, int)> rules, List<List<int>> pageNumbers) = ReadInputFromFile(FileName);

        int result = 0;

        foreach (var line in pageNumbers)
        {
            bool isValid = true;
            for (int i = 0; i < line.Count - 1; i++)
            {
                for (int j = i + 1; j < line.Count; j++)
                {
                    if (rules.Contains((line[j], line[i])))
                    {
                        (line[i], line[j]) = (line[j], line[i]);
                        isValid = false;
                    }
                }
            }

            if (!isValid)
            {
                int centralElement = line[line.Count / 2];
                result += centralElement;
            }
        }
        
        return result;
    }

    
    private (List<(int, int)>, List<List<int>>) ReadInputFromFile(string filePath)
    {
        string textFromfile = File.ReadAllText(filePath);
        
        string[] splitText = textFromfile.Split($"{Environment.NewLine}{Environment.NewLine}");
        string text1 = splitText[0];
        string text2 = splitText[1];

        List<(int, int)> rules = text1.Split(Environment.NewLine).Select(line =>
        {
            var parts = line.Split('|');
            return (int.Parse(parts[0]), int.Parse(parts[1]));
        }).ToList();
        
        List<List<int>> pageNumbers = text2.Split(Environment.NewLine).Select(line => line.Split(',').Select(int.Parse).ToList()).ToList();
        
        return (rules, pageNumbers);
    }
}