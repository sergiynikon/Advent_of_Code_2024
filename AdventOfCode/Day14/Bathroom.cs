using System.Diagnostics;
using System.Text;

namespace ConsoleApp2.Day14;

public class Bathroom(Robot[] Robots, Square borders)
{
    public void Iterate(int iterationsCount)
    {
        for (int i = 0; i < iterationsCount; i++)
        {
            foreach (var robot in Robots)
            {
                robot.Move(borders);
            }
        }
    }

    public int FindIterationWithChristmassTreeRobotPositions()
    {
        int currentIteration = 0;
        do
        {
            bool[][] robotPositions = new bool[borders.YMax - borders.YMin + 1][].Select(b => new bool[borders.XMax - borders.XMin + 1]).ToArray();
            
            foreach (var robot in Robots)
            {
                robotPositions[robot.Position.Y][robot.Position.X] = true;
            }

            if (robotPositions.Any(row =>
                {
                    for (int i = borders.XMin; i <= borders.XMax - 10; i++)
                    {
                        if (row.Skip(i).Take(10).All(b => b))
                        {
                            return true;
                        }
                    }

                    return false;
                }))
            {
                return currentIteration;
            }

            foreach (Robot robot in Robots)
            {
                robot.Move(borders);
            }
            
            currentIteration++;
        } while (currentIteration <= (borders.XMax - borders.XMin) * (borders.YMax - borders.YMin));
        
        throw new ApplicationException("There was no Christmass tree in robot positions in any iteration.");
    }

    public (int q1, int q2, int q3, int q4) CountRobots()
    {
        Square q1Square = borders with { XMax = borders.XMax / 2 - 1, YMax = borders.YMax / 2 - 1 };
        Square q2Square = borders with { XMin = borders.XMax / 2 + 1, YMax = borders.YMax / 2 - 1 };
        Square q3Square = borders with { XMax = borders.XMax / 2 - 1, YMin = borders.YMax / 2 + 1 };
        Square q4Square = borders with { XMin = borders.XMax / 2 + 1, YMin = borders.YMax / 2 + 1 };

        int q1 = Robots.Count(r => r.IsInsideBorders(q1Square));
        int q2 = Robots.Count(r => r.IsInsideBorders(q2Square));
        int q3 = Robots.Count(r => r.IsInsideBorders(q3Square));
        int q4 = Robots.Count(r => r.IsInsideBorders(q4Square));
        
        return (q1, q2, q3, q4);
    }

    public void Display(int iteration)
    {
        string fileName = "./../../../Day14/Output.txt";
        Console.WriteLine();
        Console.WriteLine($"Iteration {iteration}");
        
        bool[,] robotPositions = new bool[borders.XMax - borders.XMin + 1, borders.YMax - borders.YMin + 1];

        foreach (var robot in Robots)
        {
            robotPositions[robot.Position.X, robot.Position.Y] = true;
        }
        
        List<string> lines = [];

        for (int i = 0; i < robotPositions.GetLength(1); i++)
        {
            StringBuilder sb = new();
            for (int j = 0; j < robotPositions.GetLength(0); j++)
            {
                sb.Append(robotPositions[j, i] ? '#' : '.');
            }
            
            lines.Add(sb.ToString());
        }

        if (lines.Any(line => line.Contains("###############")))
        {
            File.AppendAllLines(fileName, [$"Iteration {iteration}"]);
            File.AppendAllLines(fileName, lines);   
        }
    }
};