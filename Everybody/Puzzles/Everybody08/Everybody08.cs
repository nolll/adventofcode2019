using Pzl.Common;

namespace Pzl.Everybody.Puzzles.Everybody08;

[Name("A Shrine for Nullpointer")]
public class Everybody08 : EverybodyPuzzle
{
    public PuzzleResult RunPart1(string input)
    {
        var availableBlocks = int.Parse(input);
        var level = 1;
        var cols = new List<int> { level };
        
        while (cols.Sum() < availableBlocks)
        {
            level += 2;
            cols.Add(level);
        }
        
        var result = (cols.Sum() - availableBlocks) * cols.Last();
        
        return new PuzzleResult(result, "5dbd06d4622464e1becf46096ff400a7");
    }

    public PuzzleResult RunPart2(string input)
    {
        var result = RunPart2(input, 20240000, 1111);
        return new PuzzleResult(result, "afafe068673279bd495b7ccfc71a0064");
    }

    public PuzzleResult RunPart3(string input)
    {
        var result = RunPart3(input, 202400000, 10);
        return new PuzzleResult(result);
    }

    public long RunPart2(string input, int availableBlocks, int acolyteCount)
    {
        var priestCount = int.Parse(input);
        var thickness = 1;
        var cols = new List<long> { thickness };
        
        while (cols.Sum() < availableBlocks)
        {
            thickness = thickness * priestCount % acolyteCount;
            cols.Insert(0, 0);
            cols.Add(0);
            for (var i = 0; i < cols.Count; i++)
            {
                cols[i] += thickness;
            }
        }

        var sum = cols.Sum();
        return (sum - availableBlocks) * cols.Count;
    }
    
    public long RunPart3(string input, int availableBlocks, int acolyteCount)
    {
        var priestCount = int.Parse(input);
        var thickness = 1;
        var cols = new List<long> { thickness };
        var space = 0L;
        var sum = cols.Sum();
        
        while (sum - space < availableBlocks)
        {
            thickness = thickness * priestCount % acolyteCount + acolyteCount;
            cols.Insert(0, 0);
            cols.Add(0);
            for (var i = 0; i < cols.Count; i++)
            {
                cols[i] += thickness;
            }
            
            sum = cols.Sum();
            space = 0;
            for (var i = 1; i < cols.Count - 1; i++)
            {
                var prevHeight = cols[i - 1];
                var nextHeight = cols[i + 1];
                var maxSpace = Math.Min(prevHeight, nextHeight);
                var thisSpace = priestCount * cols.Count * cols[i] % acolyteCount;
                space += Math.Min(maxSpace, thisSpace);
            }
            //space = cols.Skip(1).SkipLast(1).Sum(col => priestCount * cols.Count * col % acolyteCount);
        }
        
        return sum - space - availableBlocks;
    }
}