namespace ConsoleApp2.Day13;

/// <summary>
/// Linear Expression
/// <remarks>
/// a0*x + a1*x + ... + an*x = b
/// </remarks>
/// </summary>
/// <param name="a">Parameters of LE</param>
/// <param name="b">Result of LE</param>
public record LE(long[] A, long B)
{
    
    public long this[int index]
    {
        get => A[index];
        set => A[index] = value;
    }
    
    public int Length => A.Length;
    
    public long B { get; set; } = B;
    public long[] A { get; set; } = A;
}