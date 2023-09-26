﻿using Common.Puzzles;

namespace Aoc.Puzzles.Aoc2017.Aoc201708;

public class Aoc201708 : AocPuzzle
{
    public override string Name => "I Heard You Like Registers";

    protected override PuzzleResult RunPart1()
    {
        var calculator = new CpuInstructionCalculator(InputFile);
        return new PuzzleResult(calculator.LargestValueAtEnd, 6012);
    }

    protected override PuzzleResult RunPart2()
    {
        var calculator = new CpuInstructionCalculator(InputFile);
        return new PuzzleResult(calculator.LargestValueEver, 6369);
    }
}