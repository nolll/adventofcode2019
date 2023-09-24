﻿using Common.Puzzles;

namespace Aoc.Puzzles.Aoc2017.Day25;

public class Year2017Day25 : AocPuzzle
{
    public override string Name => "The Halting Problem";

    protected override PuzzleResult RunPart1()
    {
        var turingMachine = new TuringMachine(InputFile);
        var checksum = turingMachine.Run();
        return new PuzzleResult(checksum, 4387);
    }

    protected override PuzzleResult RunPart2() => PuzzleResult.Empty;
}