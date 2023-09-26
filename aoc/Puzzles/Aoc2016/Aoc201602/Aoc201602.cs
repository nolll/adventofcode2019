﻿using Common.Puzzles;

namespace Aoc.Puzzles.Aoc2016.Aoc201602;

public class Aoc201602 : AocPuzzle
{
    public override string Name => "Bathroom Security";

    protected override PuzzleResult RunPart1()
    {
        var squareCodeFinder = new SquareKeyCodeFinder();
        var squareCode = squareCodeFinder.Find(InputFile);
        return new PuzzleResult(squareCode, "61529");
    }

    protected override PuzzleResult RunPart2()
    {
        var diamondCodeFinder = new DiamondKeyCodeFinder();
        var diamondCode = diamondCodeFinder.Find(InputFile);
        return new PuzzleResult(diamondCode, "C2C28");
    }
}