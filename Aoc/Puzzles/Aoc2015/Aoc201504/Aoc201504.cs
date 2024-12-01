﻿using Pzl.Common;

namespace Pzl.Aoc.Puzzles.Aoc2015.Aoc201504;

[Name("The Ideal Stocking Stuffer")]
public class Aoc201504 : AocPuzzle
{
    private int _index;

    protected override PuzzleResult RunPart1(string input)
    {
        var coin = AdventCoinMiner.Mine(input, 5);
        _index = coin;
        return new PuzzleResult(coin, "e89372908b5202e6ce4d69a8b3538295");
    }

    protected override PuzzleResult RunPart2(string input)
    {
        var coin = AdventCoinMiner.Mine(input, 6, _index);
        return new PuzzleResult(coin, "9eb348bda7d61e7026099765b89a55fa");
    }
}