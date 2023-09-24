﻿using Common.Puzzles;

namespace Aoc.Puzzles.Aoc2020.Day16;

public class Year2020Day16 : AocPuzzle
{
    public override string Name => "Ticket Translation";

    protected override PuzzleResult RunPart1()
    {
        var validator = new TicketValidator();
        var result = validator.GetErrorRate(InputFile);
        return new PuzzleResult(result, 23_122);
    }

    protected override PuzzleResult RunPart2()
    {
        var validator = new TicketValidator();
        var result = validator.CalculateAnswer(InputFile);
        return new PuzzleResult(result, 362_974_212_989);
    }
}