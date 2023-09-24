﻿using Common.Puzzles;

namespace Aoc.Puzzles.Aoc2022.Day24;

public class Year2022Day24 : AocPuzzle
{
    public override string Name => "Blizzard Basin";

    protected override PuzzleResult RunPart1()
    {
        var blizzardNavigation = new BlizzardNavigation(InputFile);
        var result = blizzardNavigation.Part1();

        return new PuzzleResult(result, 249);
    }

    protected override PuzzleResult RunPart2()
    {
        var blizzardNavigation = new BlizzardNavigation(InputFile);
        var result = blizzardNavigation.Part2();

        return new PuzzleResult(result, 735);
    }
}