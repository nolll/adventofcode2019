﻿using System.Linq;
using Core.Common.Strings;
using Core.Platform;

namespace Core.Puzzles.Year2022.Day03;

public class Year2022Day03 : Puzzle
{
    public override PuzzleResult RunPart1()
    {
        var rucksacks = new Rucksacks();
        var result = rucksacks.GetPriority(FileInput);
        return new PuzzleResult(result);
    }

    public override PuzzleResult RunPart2()
    {
        return new EmptyPuzzleResult();
    }
}

public class Rucksacks
{
    public int GetPriority(string input)
    {
        var lines = PuzzleInputReader.ReadLines(input, false);
        var partsList = lines.Select(SplitInTwo);
        var totalSum = 0;
        foreach (var parts in partsList)
        {
            var s1 = parts[0];
            var s2 = parts[1];
            var a1 = s1.ToCharArray();
            var a2 = s2.ToCharArray();
            foreach (var c in a1)
            {
                if (a2.Contains(c))
                {
                    var priority = char.IsUpper(c)
                        ? (int)c - 38
                        : (int)c - 96;
                    totalSum += priority;
                    break;
                }
            }
        }

        return totalSum;
    }

    private string[] SplitInTwo(string s)
    {
        return new[]
        {
            s[..(s.Length / 2)],
            s[(s.Length / 2)..]
        };
    }
}