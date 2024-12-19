using System.Text.RegularExpressions;

namespace ConsoleApp2.Day14;

public static class Parser
{
    private const string RobotInputRegex = @"p=(-?\d+),(-?\d+) v=(-?\d+),(-?\d+)";
    
    public static Robot[] Parse(string input)
    {
        return input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(ExtractRobotFromString).ToArray();
    }

    private static Robot ExtractRobotFromString(string str)
    {
        Match match = Regex.Match(str, RobotInputRegex);
        
        Coordinate position = new Coordinate(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
        Coordinate velocity = new Coordinate(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));
        
        return new Robot(position, velocity);
    }
}