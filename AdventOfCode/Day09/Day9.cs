namespace ConsoleApp2.Day9;

public class Day9
{
    private const string FileName = "./../../../Day9/Input.txt";

    public long Part1()
    {
        void PickupNextI(int[] arr, ref int idx)
        {
            for (int i = idx + 1; i < arr.Length; i++)
            {
                if (arr[i] == -1)
                {
                    idx = i;
                    return;
                }
            }
        }
    
        void PickupNextJ(int[] arr, ref int idx)
        {
            for (int i = idx - 1; i >= 0; i--)
            {
                if (arr[i] != -1)
                {
                    idx = i;
                    return;
                }
            }
        }
        
        var input = ReadInputFromFile(FileName);

        int id = 0;
        bool isGap = false;
        List<int> blocks = new();
        foreach (char numC in input)
        {
            int num = int.Parse(numC.ToString());
            IEnumerable<int> block = Enumerable.Repeat(!isGap ? id : -1, num);

            blocks.AddRange(block);
            if (!isGap)
            {
                id++;
            }
            
            isGap = !isGap;
        }
        
        
        // free space
        int[] newBlocksArr = blocks.ToArray();

        int ridx = -1;
        int lidx = newBlocksArr.Length;
        
        while (ridx < lidx)
        {
            PickupNextI(newBlocksArr, ref ridx);
            PickupNextJ(newBlocksArr, ref lidx);

            if (ridx < lidx)
            {
                (newBlocksArr[ridx], newBlocksArr[lidx]) = (newBlocksArr[lidx], newBlocksArr[ridx]);
            }           
        }

        blocks = newBlocksArr.Where(block => block != -1).ToList();

        Console.WriteLine(string.Join(string.Empty, blocks));

        long result = 0;
        for (int i = 0; i < blocks.Count; i++)
        {
            result += i * (blocks[i]);
        }
        
        return result;
    }
    
    public long Part2()
    {
        void PickupBlockToMoveLeft(List<int> blocks, ref int numLIdx, ref int numRIdx)
        {
            int? currentNum = null;
            int? currentNumLIdx = null;
            int? currentNumRIdx = null;
        
            for (int i = numLIdx - 1; i >= 0; i--)
            {
                if (blocks[i] != -1)
                {
                    currentNum = blocks[i];
                    currentNumRIdx = i;
                    for (int j = i; j >= 0; j--)
                    {
                        if (j == 0)
                        {
                            currentNumLIdx = j;
                        }
                        else if (blocks[j] != currentNum)
                        {
                            currentNumLIdx = j + 1;
                            break;
                        }
                    }
                
                    break;
                }
            }

            if (currentNum != null)
            {
                numLIdx = currentNumLIdx.Value;
                numRIdx = currentNumRIdx.Value;
            }
            else
            {
                numLIdx = -1;
                numRIdx = -1;
            }
        }
        
        var input = ReadInputFromFile(FileName);

        int id = 0;
        bool isGap = false;
        List<int> blocks = new();
        foreach (char numC in input)
        {
            int num = int.Parse(numC.ToString());
            IEnumerable<int> block = Enumerable.Repeat(!isGap ? id : -1, num);

            blocks.AddRange(block);
            if (!isGap)
            {
                id++;
            }
            
            isGap = !isGap;
        }
        
        
        // free space
        int numLIdx = blocks.Count;
        int numRIdx = blocks.Count;
        do
        {
            // Console.WriteLine(string.Join("", blocks.Select(b => b == -1 ? "." : b.ToString())));
            PickupBlockToMoveLeft(blocks, ref numLIdx, ref numRIdx);

            int blockLength = numRIdx - numLIdx + 1;
            for (int i = 0; i < numLIdx; i++)
            {
                if (blocks[i] == -1)
                {
                    bool fits = true;
                    for (int j = i; j < i + blockLength; j++)
                    {
                        if (blocks[j] != -1)
                        {
                            fits = false;
                            break;
                        }
                    }

                    if (fits)
                    {
                        for (int j = i, jj = numLIdx; j < i + blockLength; j++, jj++)
                        {
                            (blocks[j], blocks[jj]) = (blocks[jj], blocks[j]);
                        }

                        break;
                    }
                }
            }
        } while (numLIdx >= 0);
        
        
        
        long result = 0;
        for (int i = 0; i < blocks.Count; i++)
        {
            if (blocks[i] != -1)
            {
                result += i * (blocks[i]);
            }
        }
        
        return result;
    }
    
    private string ReadInputFromFile(string filePath)
    {
        return File.ReadAllText(filePath);
    }
}