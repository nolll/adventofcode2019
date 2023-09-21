using NUnit.Framework;

namespace Aoc.Puzzles.Year2017.Day15;

public class Year2017Day15Tests
{
    [Test]
    public void Part1_MatchCountIsOneAfter5Runs()
    {
        var duel = new GeneratorDuel(65, 8921);
        duel.Run(5);

        Assert.That(duel.FinalCount, Is.EqualTo(1));
    }

    [Test]
    public void Part1_MatchCountIsOneAfter40MRuns()
    {
        var duel = new GeneratorDuel(65, 8921);
        duel.Run(40_000_000);

        Assert.That(duel.FinalCount, Is.EqualTo(588));
    }

    [Test]
    public void Part2_Finds309PairsIn5Runs()
    {
        var duel = new GeneratorDuel(65, 8921);
        duel.Run2(5_000_000);

        Assert.That(duel.FinalCount, Is.EqualTo(309));
    }
}