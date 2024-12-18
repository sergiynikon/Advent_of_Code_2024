using System.Text.RegularExpressions;

namespace ConsoleApp2.Day13;

public class Parser
{
    private const string Line1Regex = @"Button A: X\+(\d+), Y\+(\d+)";
    private const string Line2Regex = @"Button B: X\+(\d+), Y\+(\d+)";
    private const string Line3Regex = @"Prize: X=(\d+), Y=(\d+)";

    public Solver[] ParseSLEs(string input)
    {
        string[][] blocks = input.Split($"{Environment.NewLine}{Environment.NewLine}").Select(blockStr => blockStr.Split(Environment.NewLine)).ToArray();

        return blocks.Select(ParseLE).Select(les => new Solver(les)).ToArray();
    }
    
    public Solver[] ParseSLEs2(string input)
    {
        string[][] blocks = input.Split($"{Environment.NewLine}{Environment.NewLine}").Select(blockStr => blockStr.Split(Environment.NewLine)).ToArray();

        return blocks.Select(ParseLE2).Select(les => new Solver(les)).ToArray();
    }

    private LE[] ParseLE(string[] block)
    {
        string line1 = block[0];
        string line2 = block[1];
        string line3 = block[2];
        
        Match matchA = Regex.Match(line1, Line1Regex);
        int[] a = new[] { matchA.Groups[1].Value, matchA.Groups[2].Value }.Select(int.Parse).ToArray();
            
        Match matchB = Regex.Match(line2, Line2Regex);
        int[] b = new[] { matchB.Groups[1].Value, matchB.Groups[2].Value }.Select(int.Parse).ToArray();
        
        Match matchC = Regex.Match(line3, Line3Regex);
        int[] c = new[] { matchC.Groups[1].Value, matchC.Groups[2].Value }.Select(int.Parse).ToArray();
        
        LE le0 = new([a[0], b[0]], c[0]);
        LE le1 = new([a[1], b[1]], c[1]);

        return [le0, le1];
    }
    
    private LE[] ParseLE2(string[] block)
    {
        string line1 = block[0];
        string line2 = block[1];
        string line3 = block[2];
        
        Match matchA = Regex.Match(line1, Line1Regex);
        int[] a = new[] { matchA.Groups[1].Value, matchA.Groups[2].Value }.Select(int.Parse).ToArray();
            
        Match matchB = Regex.Match(line2, Line2Regex);
        int[] b = new[] { matchB.Groups[1].Value, matchB.Groups[2].Value }.Select(int.Parse).ToArray();
        
        Match matchC = Regex.Match(line3, Line3Regex);
        int[] c = new[] { matchC.Groups[1].Value, matchC.Groups[2].Value }.Select(int.Parse).ToArray();
        
        LE le0 = new([a[0], b[0]], c[0] + 10000000000000);
        LE le1 = new([a[1], b[1]], c[1] + 10000000000000);

        return [le0, le1];
    }
}