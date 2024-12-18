namespace ConsoleApp2.Day13;

public class Solver
{
    public Solver(LE[] linearEquations)
    {
        LinearEquations = linearEquations;
        LEResults = Solve();
    }
    
    public LE[] LinearEquations { get; }

    public double[] LEResults { get; }

    public bool IsLEResultsInt => LEResults.All(r =>r % 1 == 0);

    public bool IsLEResultExceedLimits => LEResults.Any(r => r > 100);

    public long Cost => (long)LEResults[0] * 3 + (long)LEResults[1];

    public double[] Solve()
    {
        double[,] matrix = new double[LinearEquations.Length, LinearEquations[0].A.Length + 1];

        for (int i = 0; i < LinearEquations.Length; i++)
        {
            for (int j = 0; j < LinearEquations[i].A.Length + 1; j++)
            {
                if (j == LinearEquations[i].A.Length)
                {
                    matrix[i, j] = LinearEquations[i].B;
                }
                else
                {
                    matrix[i, j] = LinearEquations[i].A[j];
                }
            }
        }

        return GaussElimination.Solve(matrix);
    }
}