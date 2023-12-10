﻿using Pzl.Common;

namespace Pzl.Aoc.Puzzles.Aoc2022.Aoc202206;

[Name("Tuning Trouble")]
public class Aoc202206 : AocPuzzle
{
    protected override PuzzleResult RunPart1()
    {
        var result = TuningTrouble.FindMarker(InputFile);
        return new PuzzleResult(result, "df3123551cc80e1f0ce2d1ce2c900f7d");
    }

    protected override PuzzleResult RunPart2()
    {
        var result = TuningTrouble.FindMessage(InputFile);
        return new PuzzleResult(result, "7b7d3c62d59d828eb3c5f0b8985d39e8");
    }
}