﻿using System;
using System.Linq;
using Core.Eggnog;

namespace ConsoleApp.Years.Year2015.Puzzles
{
    public class Day17 : Day2015
    {
        public Day17() : base(17)
        {
        }

        protected override void RunDay()
        {
            WritePartTitle();
            var container = new EggnogContainers(FileInput);
            var combinations1 = container.GetCombinations(150);
            Console.WriteLine($"Combinations: {combinations1.Count}");

            WritePartTitle();
            var combinations2 = container.GetCombinationsWithLeastContainers(150);
            var containerCount = combinations2.First().Count;
            Console.WriteLine($"Combinations with {containerCount} containers: {combinations2.Count}");
        }
    }
}