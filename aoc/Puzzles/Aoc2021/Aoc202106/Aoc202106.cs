﻿using Common.Puzzles;

namespace Aoc.Puzzles.Aoc2021.Aoc202106;

public class Aoc202106 : AocPuzzle
{
    public override string Name => "Lanternfish";

    protected override PuzzleResult RunPart1()
    {
        var fishCounter = new FishCounter(InputFile);
        var result = fishCounter.FishCountAfter(80);
        return new PuzzleResult(result, 383_160);
    }

    protected override PuzzleResult RunPart2()
    {
        var fishCounter = new FishCounter(InputFile);
        var result = fishCounter.FishCountAfter(256);
        return new PuzzleResult(result, 1_721_148_811_504);
    }
}