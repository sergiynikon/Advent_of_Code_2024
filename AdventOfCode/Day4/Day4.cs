namespace ConsoleApp2.Day4;

public class Day4
{
    private const string FileName = "./../../../Day4/Input.txt";
    
    public int Part1()
    {
        string word = "XMAS";
        string wordRev = new string(word.Reverse().ToArray());

        string[] lines = ReadInputFromFile(FileName);

        int count = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                // horizontal
                if (j + word.Length <= lines[i].Length)
                {
                    string curr = lines[i].Substring(j, word.Length);
                    if (curr == word || curr == wordRev)
                    {
                        count++;
                    }
                }
                
                // vertical
                if (i + word.Length <= lines[j].Length)
                {
                    string curr = $"{lines[i][j]}{lines[i + 1][j]}{lines[i + 2][j]}{lines[i + 3][j]}";
                    if (curr == word || curr == wordRev)
                    {
                        count++;
                    }
                }
                
                // diagonal1
                if (i + word.Length <= lines[j].Length && j + word.Length <= lines[i].Length)
                {
                    string curr = $"{lines[i][j]}{lines[i + 1][j + 1]}{lines[i + 2][j + 2]}{lines[i + 3][j + 3]}";
                    if (curr == word || curr == wordRev)
                    {
                        count++;
                    }
                }
                
                // diagonal2
                if (i - word.Length + 1 >= 0 && j + word.Length <= lines[i].Length)
                {
                    string curr = $"{lines[i - word.Length + 4][j]}{lines[i - word.Length + 3][j + 1]}{lines[i - word.Length + 2][j + 2]}{lines[i - word.Length + 1][j + 3]}";
                    if (curr == word || curr == wordRev)
                    {
                        count++;
                    }
                }
            }
        }
        return count;
    }
    
    public int Part2()
    {
        string word = "XMAS";

        string[] lines = ReadInputFromFile(FileName);

        int count = 0;

        for (int i = 0; i <= lines.Length - 3; i++)
        {
            for (int j = 0; j <= lines[i].Length - 3; j++)
            {
                bool isXmas = lines[i + 1][j + 1] == 'A'
                    && (
                           (lines[i][j] == 'M' && lines[i][j + 2] == 'S' && lines[i + 2][j] == 'M' && lines[i + 2][j + 2] == 'S')
                        || (lines[i][j] == 'M' && lines[i][j + 2] == 'M' && lines[i + 2][j] == 'S' && lines[i + 2][j + 2] == 'S')
                        || (lines[i][j] == 'S' && lines[i][j + 2] == 'S' && lines[i + 2][j] == 'M' && lines[i + 2][j + 2] == 'M')
                        || (lines[i][j] == 'S' && lines[i][j + 2] == 'M' && lines[i + 2][j] == 'S' && lines[i + 2][j + 2] == 'M')
                    );
                if (isXmas)
                {
                    count++;
                }
            }
        }
        return count;
    }
    
    private string[] ReadInputFromFile(string filePath)
    {
        return File.ReadAllLines(filePath);
    }
}