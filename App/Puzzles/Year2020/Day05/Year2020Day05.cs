﻿using App.Platform;

namespace App.Puzzles.Year2020.Day05
{
    public class Year2020Day05 : PuzzleDay
    {
        public override PuzzleResult RunPart1()
        {
            var processor = new BoardingCardProcessor(FileInput);
            return new PuzzleResult(processor.HighestId, 953);
        }

        public override PuzzleResult RunPart2()
        {
            var processor = new BoardingCardProcessor(FileInput);
            var mySeat = processor.FindMySeat();
            return new PuzzleResult(mySeat.Id, 615);
        }
    }
}