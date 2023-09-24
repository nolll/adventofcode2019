﻿using Common.Puzzles;

namespace Aoc.Puzzles.Aoc2019.Day13;

public class Year2019Day13 : AocPuzzle
{
    public override string Name => "Care Package";

    protected override PuzzleResult RunPart1()
    {
        var arcade = new Arcade(InputFile);
        arcade.Play();

        return new PuzzleResult(arcade.NumberOfBlockTiles, 226);
    }

    protected override PuzzleResult RunPart2()
    {
        var arcade = new Arcade(InputFile);
        arcade.Play(2);

        return new PuzzleResult(arcade.Score, 10800);
    }
}