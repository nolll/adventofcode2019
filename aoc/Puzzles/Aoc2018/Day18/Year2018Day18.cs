﻿using Common.Puzzles;

namespace Aoc.Puzzles.Aoc2018.Day18;

public class Year2018Day18 : AocPuzzle
{
    public override string Name => "Settlers of The North Pole";

    protected override PuzzleResult RunPart1()
    {
        var collection = new LumberCollection(InputFile);
        collection.Run(10);
        return new PuzzleResult(collection.ResourceValue, 763_804);
    }

    protected override PuzzleResult RunPart2()
    {
        var collection2 = new LumberCollection(InputFile);
        collection2.Run(1_000_000_000);
        return new PuzzleResult(collection2.ResourceValue, 188_400);
    }
}