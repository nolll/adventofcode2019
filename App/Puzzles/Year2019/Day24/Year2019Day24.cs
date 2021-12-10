﻿using App.Platform;

namespace App.Puzzles.Year2019.Day24
{
    public class Year2019Day24 : Puzzle
    {
        public override PuzzleResult RunPart1()
        {
            var simulator = new BugLifeSimulator(FileInput);
            simulator.RunUntilRepeat();

            return new PuzzleResult(simulator.BiodiversityRating, 12_129_040);
        }

        public override PuzzleResult RunPart2()
        {
            var recursiveSimulator = new RecursiveBugLifeSimulator(FileInput);
            recursiveSimulator.Run(200);

            return new PuzzleResult(recursiveSimulator.BugCount, 2109);
        }
    }
}