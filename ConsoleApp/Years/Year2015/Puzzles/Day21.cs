﻿using System;
using Core.RpgSimulation;
using Core.Tools;

namespace ConsoleApp.Years.Year2015.Puzzles
{
    public class Day21 : Day2015
    {
        public Day21() : base(21)
        {
        }

        protected override void RunDay()
        {
            var p = GetParams();

            WritePartTitle();
            var simulator = new RpgSimulator();
            var leastGoldRequiredToWin = simulator.WinWithLowestCost(p.HitPoints, p.Damage, p.Armor);
            Console.WriteLine($"Least amount of gold to win: {leastGoldRequiredToWin}");

            WritePartTitle();
            var mostGoldThatLoses = simulator.LoseWithHighestCost(p.HitPoints, p.Damage, p.Armor);
            Console.WriteLine($"Most amount of gold that loses: {mostGoldThatLoses}");
        }

        private Params GetParams()
        {
            var rows = PuzzleInputReader.ReadLines(FileInput);

            return new Params
            {
                HitPoints = GetIntFromRow(rows[0]),
                Damage = GetIntFromRow(rows[1]),
                Armor = GetIntFromRow(rows[2])
            };
        }

        private static int GetIntFromRow(string s)
        {
            return int.Parse(s.Split(':')[1].Trim());
        }

        private class Params
        {
            public int HitPoints { get; set; }
            public int Damage { get; set; }
            public int Armor { get; set; }
        }
    }
}