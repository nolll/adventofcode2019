using System.Configuration;
using Pzl.Common;
using Pzl.Tools.Strings;

namespace Pzl.Everybody.Puzzles.Everybody09;

[Name("")]
public class Everybody09(string[] inputs) : EverybodyPuzzle
{
    protected override PuzzleResult RunPart1()
    {
        var result = Part1(inputs[0]);
        return new PuzzleResult(result, "dffa64bee7ea0c0ad66724de7afe7c08");
    }

    protected override PuzzleResult RunPart2()
    {
        var result = Part2(inputs[1]);
        return new PuzzleResult(result, "6032109447891782512325cb9251f9e2");
    }

    protected override PuzzleResult RunPart3()
    {
        return PuzzleResult.Empty;
    }

    private class ShortestCombination
    {
        public int Length { get; set; } = int.MaxValue;

        public void Report(List<int> combination)
        {
            var count = combination.Count;
            if (count < Length)
                Length = count;
        }
    }

    public static int Part1(string input)
    {
        return Solve(input, [1, 3, 5, 10]);
    }
    
    public static int Part2(string input)
    {
        return Solve(input, [1, 3, 5, 10, 15, 16, 20, 24, 25, 30]);
    }
    
    public static int Part3(string input)
    {
        var seen = new Dictionary<int, List<int>>();
        List<int> stamps = [1, 3, 5, 10, 15, 16, 20, 24, 25, 30, 37, 38, 49, 50, 74, 75, 100, 101];
        var orderedStamps = stamps.OrderDescending().ToList();
        var combinedSums = input.Split(LineBreaks.Single).Select(int.Parse);
        var total = 0;
        foreach (var sum in combinedSums)
        {
            var mid = sum / 2;
            var min = mid - 100;
            var max = mid + 100;
            var minCount = int.MaxValue;
            for (var i = min; i <= max; i++)
            {
                if (!seen.TryGetValue(i, out var s1))
                {
                    s1 = SolveBall(i, orderedStamps);
                    seen.Add(i, s1);
                }
                
                if (!seen.TryGetValue(sum - i, out var s2))
                {
                    s2 = SolveBall(sum - i, orderedStamps);
                    seen.Add(sum - i, s2);
                }
                
                if (Math.Abs(s1.Sum() - s2.Sum()) <= 100)
                {
                    var count = s1.Count + s2.Count;
                    minCount = Math.Min(minCount, count);
                }
            }
            
            Console.WriteLine(minCount);

            total += minCount;
        }

        return total;
    }

    private static int Solve(string input, int[] stamps)
    {
        var balls = input.Split(LineBreaks.Single).Select(int.Parse).ToArray();
        var orderedStamps = stamps.OrderDescending().ToList();
        var results = new List<List<int>>();
        
        foreach (var ball in balls)
        {
            results.Add(SolveBall(ball, orderedStamps));
        }
        
        return results.Sum(o => o.Count);
    }

    private static List<int> SolveBall(int ball, List<int> stamps)
    {
        List<int> combinations = [];
        var stampSum = stamps.Sum();
        var largestStamp = stamps.First();

        while (ball > stampSum)
        {
            ball -= largestStamp;
            combinations.Add(largestStamp);
        }
        
        var shortestCombination = new ShortestCombination();
        var solutions = GetCombinations(stamps, ball, combinations, shortestCombination).OrderBy(o => o.Count);
        return solutions.First();
    }

    private static List<List<int>> GetCombinations(
        IReadOnlyCollection<int> denominations, 
        int target, 
        List<int> combination,
        ShortestCombination shortestCombination)
    {
        if (target < 0 || !denominations.Any())
            return [];

        if (target == 0)
        {
            shortestCombination.Report(combination);
            return [combination];
        }

        if (combination.Count > shortestCombination.Length)
            return [];

        var denomination = denominations.First();
        var remainingDenominations = denominations.Skip(1).ToList();

        var returnedCombinations = new List<List<int>>();
        var nextCombination = combination.ToList();
        nextCombination.Add(denomination);
        returnedCombinations.AddRange(GetCombinations(denominations, target - denomination, nextCombination, shortestCombination));
        returnedCombinations.AddRange(GetCombinations(remainingDenominations, target, combination.ToList(), shortestCombination));

        return returnedCombinations;
    }
}