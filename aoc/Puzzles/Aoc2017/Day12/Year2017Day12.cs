﻿using Common.Puzzles;

namespace Aoc.Puzzles.Aoc2017.Day12;

public class Year2017Day12 : AocPuzzle
{
    public override string Name => "Digital Plumber";

    protected override PuzzleResult RunPart1()
    {
        var pipes = new Pipes(InputFile);
        return new PuzzleResult(pipes.PipesInGroupZero, 145);
    }

    protected override PuzzleResult RunPart2()
    {
        var pipes = new Pipes(InputFile);
        return new PuzzleResult(pipes.GroupCount, 207);
    }
}