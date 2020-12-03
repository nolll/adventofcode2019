﻿using System;
using Core.AnimatedLights;

namespace ConsoleApp.Years.Year2015.Puzzles
{
    public class Day18 : Day2015
    {
        public Day18() : base(18)
        {
        }

        protected override void RunDay()
        {
            WritePartTitle();
            var gif = new AnimatedGif(FileInput);
            gif.RunAnimation(100);
            Console.WriteLine($"Lights switched on: {gif.LightCount}");

            WritePartTitle();
            var gif2 = new AnimatedGif(FileInput, true);
            gif2.RunAnimation(100);
            Console.WriteLine($"Lights switched on, when corners are always lit: {gif2.LightCount}");
        }
    }
}