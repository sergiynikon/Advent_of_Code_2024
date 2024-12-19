namespace ConsoleApp2.Day14;

public class Day14
{
    private const string FileName = "./../../../Day14/Input.txt";
    private const int IterationsCount = 100;
    private const int XLength = 101;
    private const int YLength = 103;
    
    public long Part1()
    {
        Robot[] robots = Parser.Parse(ReadFromFile(FileName));
        Square borders = new(0, XLength - 1, 0, YLength - 1);
        Bathroom bathroom = new(robots, borders);
        
        bathroom.Iterate(IterationsCount);
        (int q1, int q2, int q3, int q4) robotsCount = bathroom.CountRobots();
        
        return robotsCount.q1 * robotsCount.q2 * robotsCount.q3 * robotsCount.q4;
    }

    public int Part2()
    {
        Robot[] robots = Parser.Parse(ReadFromFile(FileName));
        Square borders = new(0, XLength - 1, 0, YLength - 1);
        Bathroom bathroom = new(robots, borders);

        int iteration = bathroom.FindIterationWithChristmassTreeRobotPositions();
        
        return iteration;
    }

    private string ReadFromFile(string filename)
    {
        return File.ReadAllText(filename);
    }
}