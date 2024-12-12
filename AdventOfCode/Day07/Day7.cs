namespace ConsoleApp2.Day7;

public class Day7
{
    private const string FileName = "./../../../Day7/Input.txt";

    private enum Operator
    {
        Plus = 0,
        Multiply = 1,
        Combine = 2
    }
    
    public long Part1()
    {
        List<(long equationResult, List<int> equationNums)> input = ReadInputFromFile(FileName);

        long result = 0;

        foreach ((long equationResult, List<int> equationNums) in input)
        {
            List<List<Operator>> operatorPerms = GetOperatorPermutations(equationNums.Count - 1);

            foreach (List<Operator> operatorPerm in operatorPerms)
            {
                long currentRes = equationNums[0];

                for (int i = 1; i < equationNums.Count; i++)
                {
                    currentRes = ApplyOperator(operatorPerm[i - 1], currentRes, equationNums[i]);
                }

                if (currentRes == equationResult)
                {
                    result += equationResult;
                    break;
                }
            }
        }
        
        return result;
    }
    
    public long Part2()
    {
        List<(long equationResult, List<int> equationNums)> input = ReadInputFromFile(FileName);

        long result = 0;

        foreach ((long equationResult, List<int> equationNums) in input)
        {
            List<List<Operator>> operatorPerms = GetOperatorPermutations2(equationNums.Count - 1);

            foreach (List<Operator> operatorPerm in operatorPerms)
            {
                long currentRes = equationNums[0];

                for (int i = 1; i < equationNums.Count; i++)
                {
                    currentRes = ApplyOperator(operatorPerm[i - 1], currentRes, equationNums[i]);
                }

                if (currentRes == equationResult)
                {
                    result += equationResult;
                    break;
                }
            }
        }
        
        return result;
    }

    private long ApplyOperator(Operator op, long num1, long num2)
    {
        return op switch
        {
            Operator.Plus => num1 + num2,
            Operator.Combine => num1 * num2,
            Operator.Multiply => long.Parse($"{num1}{num2}"),
            _ => throw new ArgumentOutOfRangeException(nameof(op), op, null)
        };
    }

    private List<List<Operator>> GetOperatorPermutations(int length)
    {
        List<Operator> operators = [Operator.Multiply, Operator.Plus];
        var result = new List<List<Operator>>();
        GeneratePermutations(operators, new List<Operator>(), length, result);
        return result;
    }
    
    private List<List<Operator>> GetOperatorPermutations2(int length)
    {
        List<Operator> operators = [Operator.Multiply, Operator.Plus, Operator.Combine];
        var result = new List<List<Operator>>();
        GeneratePermutations(operators, new List<Operator>(), length, result);
        return result;
    }

    private void GeneratePermutations(List<Operator> operators, List<Operator> current, int length, List<List<Operator>> result)
    {
        if (current.Count == length)
        {
            result.Add(new List<Operator>(current));
            return;
        }

        foreach (var @operator in operators)
        {
            current.Add(@operator);
            GeneratePermutations(operators, current, length, result);
            current.RemoveAt(current.Count - 1);
        }
    }
    
    private List<(long, List<int>)> ReadInputFromFile(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);

        return lines.Select(line =>
        {
            string[] parts = line.Split(":");
            return (long.Parse(parts[0]),
                parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList());
        }).ToList();
    }
}