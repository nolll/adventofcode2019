﻿using App.Platform;

namespace App.Puzzles.Year2015.Day15
{
    public class Year2015Day15 : PuzzleDay
    {
        public override PuzzleResult RunPart1()
        {
            var bakery = new CookieBakery(FileInput);
            return new PuzzleResult(bakery.HighestScore, 21_367_368);
        }

        public override PuzzleResult RunPart2()
        {
            var bakery = new CookieBakery(FileInput);
            return new PuzzleResult(bakery.HighestScoreWith500Calories, 1_766_400);
        }
    }
}