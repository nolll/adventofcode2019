﻿using Aoc.Platform;

namespace Aoc.Puzzles.Year2019.Day20;

public class Year2019Day20 : Puzzle
{
    public override string Title => "Donut Maze";
    public override string Comment => "Donut Maze";
    public override bool IsSlow => true; // More than 7 minutes

    public override PuzzleResult RunPart1()
    {
        var mazeSolver = new DonutMazeSolver(FileInput);
        return new PuzzleResult(mazeSolver.ShortestStepCount, 462);
    }

    public override PuzzleResult RunPart2()
    {
        var recursiveDonutMazeSolver = new RecursiveDonutMazeSolver(FileInput);
        return new PuzzleResult(recursiveDonutMazeSolver.ShortestStepCount, 5288);
    }
}