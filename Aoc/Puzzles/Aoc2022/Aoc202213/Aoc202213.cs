﻿using Puzzles.Common.Puzzles;

namespace Puzzles.Aoc.Puzzles.Aoc2022.Aoc202213;

public class Aoc202213 : AocPuzzle
{
    public override string Name => "Distress Signal";

    protected override PuzzleResult RunPart1()
    {
        var signal = new DistressSignal();
        var result = signal.Part1(InputFile);

        return new PuzzleResult(result, "b7ce4fc8127f3ae910077459ccdd2466");
    }

    protected override PuzzleResult RunPart2()
    {
        var signal = new DistressSignal();
        var result = signal.Part2(InputFile);

        return new PuzzleResult(result, "dae06225ebac50b689604b2ca86cfabf");
    }
}