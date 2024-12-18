namespace ConsoleApp2.Day13;

public static class GaussElimination
{
    public static double[] Solve(double[,] matrix)
    {
        int n = matrix.GetLength(0); // Number of equations

        // Forward elimination
        for (int i = 0; i < n; i++)
        {
            // Find the pivot row
            int pivotRow = i;
            for (int k = i + 1; k < n; k++)
            {
                if (Math.Abs(matrix[k, i]) > Math.Abs(matrix[pivotRow, i]))
                {
                    pivotRow = k;
                }
            }

            // Swap rows if necessary
            if (pivotRow != i)
            {
                for (int j = 0; j <= n; j++)
                {
                    (matrix[i, j], matrix[pivotRow, j]) = (matrix[pivotRow, j], matrix[i, j]);
                }
            }

            // Make sure the pivot element is not zero
            if (Math.Abs(matrix[i, i]) < 1e-10)
            {
                throw new InvalidOperationException("The system of equations has no unique solution.");
            }

            // Eliminate column i for rows below
            for (int k = i + 1; k < n; k++)
            {
                double factor = matrix[k, i] / matrix[i, i];
                for (int j = i; j <= n; j++)
                {
                    matrix[k, j] -= factor * matrix[i, j];
                }
            }
        }

        // Back substitution
        double[] solution = new double[n];
        for (int i = n - 1; i >= 0; i--)
        {
            solution[i] = matrix[i, n];
            for (int j = i + 1; j < n; j++)
            {
                solution[i] -= matrix[i, j] * solution[j];
            }
            solution[i] /= matrix[i, i];
        }

        for (int i = 0; i < solution.Length; i++)
        {
            solution[i] = Math.Round(solution[i], 4);
        }

        return solution;
    }
}