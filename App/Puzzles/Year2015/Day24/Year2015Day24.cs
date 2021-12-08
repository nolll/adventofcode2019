﻿using App.Platform;

namespace App.Puzzles.Year2015.Day24
{
    public class Year2015Day24 : PuzzleDay
    {
        public override string Comment => "Present balancing";
        public override bool IsSlow => true;

        public override PuzzleResult RunPart1()
        {
            var balancer1 = new PresentBalancer(FileInput, 3);
            return new PuzzleResult(balancer1.QuantumEntanglementOfFirstGroup, 11_266_889_531);
        }

        public override PuzzleResult RunPart2()
        {
            var balancer2 = new PresentBalancer(FileInput, 4);
            return new PuzzleResult(balancer2.QuantumEntanglementOfFirstGroup, 77_387_711);
        }
    }
}