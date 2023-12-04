﻿using Puzzles.Common.Puzzles;

namespace Puzzles.Aoc.Puzzles.Aoc2019.Aoc201925;

public class Aoc201925 : AocPuzzle
{
    public override string Name => "Cryostasis";

    protected override PuzzleResult RunPart1()
    {
        var investigationDroid = new InvestigationDroid(InputFile);
        var password = investigationDroid.Run();

        return new PuzzleResult(password, "378fea8b73ddddacf10ae3b5978e47ab");
    }

    protected override PuzzleResult RunPart2() => PuzzleResult.Empty;
}