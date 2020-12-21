﻿using Core.ImageJigsaw;
using Core.IslandFood;

namespace ConsoleApp.Years.Year2020.Puzzles
{
    public class Day21 : Day2020
    {
        public Day21() : base(21)
        {
        }

        public override PuzzleResult RunPart1()
        {
            var detector = new AllergenDetector(FileInput);
            var ingredientCount = detector.FindIngredientsWithoutAllergens();
            return new PuzzleResult(ingredientCount);
        }
    }
}