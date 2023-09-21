﻿using Aoc.Platform;
using common.Puzzles;

namespace Aoc.Puzzles.Year2016.Day06;

public class Year2016Day06 : AocPuzzle
{
    public override string Title => "Signals and Noise";

    public override PuzzleResult RunPart1()
    {
        var reader = new RepetitionCodeReader();
        var messageMostCommon = reader.ReadMostCommon(FileInput);
        return new PuzzleResult(messageMostCommon, "ygjzvzib");
    }

    public override PuzzleResult RunPart2()
    {
        var reader = new RepetitionCodeReader();
        var messageLeastCommon = reader.ReadLeastCommon(FileInput);
        return new PuzzleResult(messageLeastCommon, "pdesmnoz");
    }
}