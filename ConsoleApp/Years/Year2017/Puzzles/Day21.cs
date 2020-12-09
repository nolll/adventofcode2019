﻿using System;
using Core.FractalArt;

namespace ConsoleApp.Years.Year2017.Puzzles
{
    public class Day21 : Day2017
    {
        public Day21() : base(21)
        {
        }

        protected override void RunDay()
        {
            WritePartTitle();
            var artGenerator1 = new FractalArtGenerator(FileInput);
            artGenerator1.Run(5);
            Console.WriteLine($"Pixels on after 5 iterations: {artGenerator1.PixelsOn}");

            WritePartTitle();
            var artGenerator2 = new FractalArtGenerator(FileInput);
            artGenerator2.Run(18);
            Console.WriteLine($"Pixels on after 18 iterations: {artGenerator2.PixelsOn}");
        }
    }
}