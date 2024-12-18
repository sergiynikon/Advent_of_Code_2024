namespace ConsoleApp2.Day13;

public class Day13
{
    private const string FileName = "./../../../Day13/Input.txt";

    public long Part1()
    {
        string input = ReadFromFile(FileName);

        Solver[] sles = new Parser().ParseSLEs(input);

        return sles.Where(sle => sle.IsLEResultsInt && !sle.IsLEResultExceedLimits).Sum(sle => sle.Cost);
    }
    
    public long Part2()
    {
        string input = ReadFromFile(FileName);

        Solver[] sles = new Parser().ParseSLEs2(input);

        return sles.Where(sle => sle.IsLEResultsInt).Sum(sle => sle.Cost);
    }
    
    private string ReadFromFile(string fileName)
    {
        return File.ReadAllText(fileName);
    }
}