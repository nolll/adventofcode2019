﻿using Core.Platform;

namespace Core.Puzzles.Year2018.Day20;

public class Year2018Day20 : Puzzle
{
    public override PuzzleResult RunPart1()
    {
        var navigator = new RegularMapNavigator(FileInput);
        return new PuzzleResult(navigator.MostDoors, 4050);
    }

    public override PuzzleResult RunPart2()
    {
        var navigator = new RegularMapNavigator(FileInput);
        return new PuzzleResult(navigator.RoomsMoreThat1000DoorsAway, 8564);
    }
}