﻿using System.Diagnostics;
using Puzzles.common.Combinatorics;
using Puzzles.common.Maths;
using Puzzles.common.Puzzles;
using static Puzzles.aquaq.Puzzles.Aquaq36.Aquaq36;

namespace Puzzles.aquaq.Puzzles.Aquaq36;

public class Aquaq36 : AquaqPuzzle
{
/*
Part 1: 30.332s211
Part 1: 57.818s212
Part 1: 58.071s260
Part 1: 60.270s139
Part 1: 60.686s82
Part 1: 61.389s95
Part 1: 74.507s178
Part 1: 97.802s46
Part 1: 126.687s55
Part 1: 238.005s123
Part 1: 407.183s60
*/

    public override string Name => "Tetonor Terror";

    protected override PuzzleResult Run()
    {
        var factorProvider = new FactorCache();
        var tetonors = InputFile.Split($"{Environment.NewLine}{Environment.NewLine}");
        var sum = 0;

        foreach (var tetonor in tetonors)
        {
            var rows = tetonor.Split(Environment.NewLine);
            var grid = rows[0][2..].Split(' ').Select(int.Parse).ToList();
            var input = rows[1][2..].Split(' ').Where(o => o != "*").Select(int.Parse).ToList();
            var result = Solve(grid, input, factorProvider);
            sum += result;
        }

        return new PuzzleResult(sum);
    }

    public static int Solve(List<int> grid, List<int> input, FactorCache? factorCache = null)
    {
        var possibleInputs = GetPossibleInputs(grid, input);

        foreach (var possibleInput in possibleInputs)
        {
            var result = Solve(grid, possibleInput, factorCache ?? new FactorCache(), 0);
            if (result > 0)
                return result;
        }

        return 0;
    }

    private static List<List<int>> GetPossibleInputs(List<int> grid, List<int> input)
    {
        var missingCount = grid.Count - input.Count;
        var factors = GetAllFactors(grid);
        var factorCombinations = CombinationGenerator.GetCombinationsFixedSize(factors, missingCount);
        var possibleInputs = new List<List<int>>();
        foreach (var combination in factorCombinations)
        {
            possibleInputs.Add(combination.Concat(input).ToList());
        }

        return possibleInputs;
    }

    private static int Solve(List<int> grid, List<int> input, FactorCache factorCache, int sum)
    {
        if (!grid.Any())
            return sum;

        foreach (var gridNumber in grid)
        {
            var factorList = factorCache.Get(gridNumber);
            foreach (var factors in factorList)
            {
                if (input.Contains(factors.a) && input.Contains(factors.b))
                {
                    var p = factors.a * factors.b;
                    var s = factors.a + factors.b;
                    if (grid.Contains(p) && grid.Contains(s))
                    {
                        var newGrid = grid.ToList();
                        var newInput = input.ToList();
                        newGrid.Remove(p);
                        newGrid.Remove(s);
                        newInput.Remove(factors.a);
                        newInput.Remove(factors.b);
                        var result = Solve(newGrid, newInput, factorCache, sum + factors.b - factors.a);
                        if (result > 0)
                            return result;
                    }
                }
            }
        }
        
        return 0;
    }

    public class FactorCache
    {
        private readonly Dictionary<int, List<(int, int)>> _cache = new();

        public List<(int a, int b)> Get(int n)
        {
            if (_cache.TryGetValue(n, out var factors))
                return factors;

            factors = MathTools.GetMultiplicationFactors(n);
            _cache.Add(n, factors);
            return factors;
        }
    }

    public static List<int> GetAllFactors(IEnumerable<int> gridNumbers)
    {
        var list = new List<int>();
        foreach (var n in gridNumbers)
        {
            var factors = MathTools.GetFactors(n).ToArray();
            list.AddRange(factors);
        }

        return list.Order().ToList();
    }

    public static Dictionary<int, List<int>> FindPossibleInputNumbers(List<int?> input, int max, HashSet<int>? factors = null)
    {
        var d = new Dictionary<int, List<int>>();
        for (var i = 0; i < input.Count; i++)
        {
            if (input[i] is null)
            {
                var lowerBound = FindLowerBound(input, i);
                var upperBound = FindUpperBound(input, i, max);
                var rangeCount = upperBound - lowerBound + 1;
                var numbers = Enumerable.Range(lowerBound, rangeCount);
                if (factors != null)
                    numbers = numbers.Where(factors.Contains);
                d.Add(i, numbers.ToList());
            }
        }

        return d;
    }

    private static int FindLowerBound(List<int?> input, int index)
    {
        for (var j = index - 1; j >= 0; j++)
        {
            var v = input[j];
            if (v is not null)
                return v.Value;
        }

        return 1;
    }

    private static int FindUpperBound(List<int?> input, int index, int max)
    {
        for (var j = index + 1; j < input.Count; j++)
        {
            var v = input[j];
            if (v is not null)
                return v.Value;
        }

        return max;
    }
}