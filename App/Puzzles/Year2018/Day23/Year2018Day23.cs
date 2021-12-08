﻿using App.Platform;

namespace App.Puzzles.Year2018.Day23
{
    public class Year2018Day23 : PuzzleDay
    {
        public override PuzzleResult RunPart1()
        {
            var formation = new NanobotFormation(FileInput);
            var botCount = formation.GetBotsInRangeOfStrongestBot().Count;
            return new PuzzleResult(botCount, 481);
        }

        public override PuzzleResult RunPart2()
        {
            var formation = new NanobotFormation(FileInput);
            var distanceToBestCoords = formation.FindManhattanDistanceToBestCoords();
            return new PuzzleResult(distanceToBestCoords, 47_141_479);
        }
    }
}