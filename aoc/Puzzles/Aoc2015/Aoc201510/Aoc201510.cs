﻿using Common.Puzzles;

namespace Aoc.Puzzles.Aoc2015.Aoc201510;

public class Aoc201510 : AocPuzzle
{
    public override string Name => "Elves Look, Elves Say";

    protected override PuzzleResult RunPart1()
    {
        var game = new LookAndSayGame(Input, 40);
        return new PuzzleResult(game.Result.Length, 252_594);
    }

    protected override PuzzleResult RunPart2()
    {
        var game2 = new LookAndSayGame(Input, 50);
        return new PuzzleResult(game2.Result.Length, 3_579_328);
    }

    private const string Input = "1113222113";
}