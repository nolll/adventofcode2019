﻿using Core.Platform;

namespace Core.Puzzles.Year2022.Day17;

public class Year2022Day17 : Puzzle
{
    public override string Title => "Pyroclastic Flow";

    public override PuzzleResult RunPart1()
    {
        var tetris = new Tetris();
        var result = tetris.Run(FileInput, 2022);

        return new PuzzleResult(result, 3197);
    }

    public override PuzzleResult RunPart2()
    {
        var tetris = new Tetris();
        var result = tetris.Run(FileInput, 1_000_000_000_000);

        return new PuzzleResult(result, 1_568_513_119_571);
    }
}   