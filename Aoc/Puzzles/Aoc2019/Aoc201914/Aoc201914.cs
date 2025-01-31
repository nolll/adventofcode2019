using Pzl.Common;

namespace Pzl.Aoc.Puzzles.Aoc2019.Aoc201914;

[Name("Space Stoichiometry")]
public class Aoc201914 : AocPuzzle
{
    public PuzzleResult RunPart1(string input)
    {
        var reactor = new NanoReactor(input);
        reactor.Run();
        var oreForOneFuel = reactor.RequiredOreForOneFuel;

        return new PuzzleResult(oreForOneFuel, "4f7b51a9155bea7c24bbb1d4757e4bf1");
    }

    public PuzzleResult RunPart2(string input)
    {
        var reactor = new NanoReactor(input);
        reactor.Run();
        var fuelCount = reactor.FuelFromOneTrillionOre;

        return new PuzzleResult(fuelCount, "d2bf7b83647cf534681bd96e1a53db40");
    }
}