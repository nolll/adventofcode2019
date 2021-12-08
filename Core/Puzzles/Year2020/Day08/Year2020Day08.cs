﻿using Core.Platform;

namespace Core.Puzzles.Year2020.Day08
{
    public class Year2020Day08 : Year2020Day
    {
        public override int Day => 8;

        public override PuzzleResult RunPart1()
        {
            var console = new GameConsoleRunner(FileInput);
            var acc = console.RunUntilLoop();
            return new PuzzleResult(acc, 1446);
        }

        public override PuzzleResult RunPart2()
        {
            var console = new GameConsoleRunner(FileInput);
            var acc = console.RunUntilTermination();
            return new PuzzleResult(acc, 1403);
        }
    }
}