﻿using Aoc.Platform;

namespace Aoc.Puzzles.Year2018.Day11;

public class Year2018Day11 : Puzzle
{
    public override string Title => "Chronal Charge";
    public override bool IsFunToOptimize => true;

    public override PuzzleResult RunPart1()
    {
        var grid = new PowerGrid(300, Input);
        var maxCoords = grid.GetMaxCoords();
        var strCoords = $"{maxCoords.X},{maxCoords.Y}";
        return new PuzzleResult(strCoords, "20,43");
    }

    public override PuzzleResult RunPart2()
    {
        var grid = new PowerGrid(300, Input);
        var (coords, size) = grid.GetMaxCoordsAnySize();
        var strCoordsAndSize2 = $"{coords.X},{coords.Y},{size}";
        return new PuzzleResult(strCoordsAndSize2, "233,271,13");
    }

    private const int Input = 1309;
}