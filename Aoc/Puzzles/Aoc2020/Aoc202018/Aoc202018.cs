﻿using Puzzles.Common.Puzzles;

namespace Puzzles.Aoc.Puzzles.Aoc2020.Aoc202018;

public class Aoc202018 : AocPuzzle
{
    public override string Name => "Operation Order";

    protected override PuzzleResult RunPart1()
    {
        var calculator = new HomeworkCalculator();
        var result = calculator.SumOfAll(InputFile, MathPrecedence.Order);
        return new PuzzleResult(result, "4a4cb1e5143143fe556872f0d8ace4bc");
    }

    protected override PuzzleResult RunPart2()
    {
        var calculator = new HomeworkCalculator();
        var result = calculator.SumOfAll(InputFile, MathPrecedence.Addition);
        return new PuzzleResult(result, "f4ba1f258e57a75e7a35552abca1311f");
    }
}