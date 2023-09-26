﻿using Common.Puzzles;

namespace Aoc.Puzzles.Aoc2021.Aoc202110;

public class Aoc202110 : AocPuzzle
{
    public override string Name => "Syntax Scoring";

    protected override PuzzleResult RunPart1()
    {
        var syntaxChecker = new SyntaxChecker();
        var result = syntaxChecker.GetTotalErrorScore(InputFile);
        return new PuzzleResult(result, 399153);
    }

    protected override PuzzleResult RunPart2()
    {
        var syntaxChecker = new SyntaxChecker();
        var result = syntaxChecker.FindMiddleScore(InputFile);
        return new PuzzleResult(result, 2995077699);
    }
}