﻿using Common.Puzzles;

namespace Aoc.Puzzles.Aoc2021.Aoc202111;

public class Aoc202111 : AocPuzzle
{
    public override string Name => "Dumbo Octopus";

    protected override PuzzleResult RunPart1()
    {
        var flasher = new OctopusFlasher(InputFile);
        var result = flasher.Run(100);
        return new PuzzleResult(result, 1591);
    }

    protected override PuzzleResult RunPart2()
    {
        var flasher = new OctopusFlasher(InputFile);
        var result = flasher.Run();
        return new PuzzleResult(result, 314);
    }
}