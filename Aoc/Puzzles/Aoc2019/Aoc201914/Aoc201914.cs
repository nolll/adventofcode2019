﻿using Puzzles.Common.Puzzles;

namespace Puzzles.Aoc.Puzzles.Aoc2019.Aoc201914;

public class Aoc201914 : AocPuzzle
{
    public override string Name => "Space Stoichiometry";

    protected override PuzzleResult RunPart1()
    {
        var reactor = new NanoReactor(InputFile);
        reactor.Run();
        var oreForOneFuel = reactor.RequiredOreForOneFuel;

        return new PuzzleResult(oreForOneFuel, "4f7b51a9155bea7c24bbb1d4757e4bf1");
    }

    protected override PuzzleResult RunPart2()
    {
        var reactor = new NanoReactor(InputFile);
        reactor.Run();
        var fuelCount = reactor.FuelFromOneTrillionOre;

        return new PuzzleResult(fuelCount, "d2bf7b83647cf534681bd96e1a53db40");
    }
}