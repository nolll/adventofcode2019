﻿using Common.Puzzles;

namespace Aoc.Puzzles.Aoc2017.Day04;

public class Year2017Day04 : AocPuzzle
{
    public override string Name => "High-Entropy Passphrases";

    protected override PuzzleResult RunPart1()
    {
        var validator = new PassphraseValidator();
        var validCount1 = validator.GetValidCount1(InputFile);
        return new PuzzleResult(validCount1, 477);
    }

    protected override PuzzleResult RunPart2()
    {
        var validator = new PassphraseValidator();
        var validCount2 = validator.GetValidCount2(InputFile);
        return new PuzzleResult(validCount2, 167);
    }
}