﻿using Common.Puzzles;

namespace Aoc.Puzzles.Aoc2017.Day23;

public class Year2017Day23 : AocPuzzle
{
    public override string Name => "Coprocessor Conflagration";

    protected override PuzzleResult RunPart1()
    {
        var processor1 = new CoProcessor(InputFile);
        processor1.Run();
        return new PuzzleResult(processor1.MulCount, 4225);
    }

    protected override PuzzleResult RunPart2()
    {
        var processor2 = new OptimizedCoProcessor();
        processor2.Run();
        return new PuzzleResult(processor2.H, 905);
    }
}