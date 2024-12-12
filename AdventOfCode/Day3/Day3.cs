using System.Text.RegularExpressions;

namespace ConsoleApp2.Day3;

public class Day3
{
    private const string FileName = "./../../../Day3/Input.txt";

    public int Part1()
    {
        string text = ReadInputFromFile(FileName);
        
        List<(int left, int right)> entries = GetMulEntries(text);

        return entries.Sum(entry => entry.left * entry.right);
    }

    public int Part2()
    {
        string text = ReadInputFromFile(FileName);
        List<(int left, int right)> entries = GetDoDontMulEntries(text);
        
        return entries.Sum(entry => entry.left * entry.right);
    }

    private List<(int left, int right)> GetDoDontMulEntries(string text)
    {
        string regexBetweenDoDont = @"do\(\)([\s\S]*?)don\'t\(\)";
        string wrappedText = $"do(){text}don't()";

        MatchCollection matches = Regex.Matches(wrappedText, regexBetweenDoDont);

        return matches
            .SelectMany(match => GetMulEntries(match.Groups[1].Value)).ToList();
    }

    private List<(int, int)> GetMulEntries(string text)
    {
        string regex = @"mul\((\d+),(\d+)\)";
        
        MatchCollection matches = Regex.Matches(text, regex);

        return matches
            .Select(match => 
                (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)))
            .ToList();
    }
    
    private string ReadInputFromFile(string filePath)
    {
        return File.ReadAllText(filePath);
    }
}