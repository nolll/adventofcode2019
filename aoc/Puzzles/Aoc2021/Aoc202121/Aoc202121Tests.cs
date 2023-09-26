using NUnit.Framework;

namespace Aoc.Puzzles.Aoc2021.Aoc202121;

public class Aoc202121Tests
{
    [Test]
    public void Part1()
    {
        var game = new DiracDiceGame();
        var result = game.Play(4, 8);

        Assert.That(result.Result, Is.EqualTo(739785));
    }
    
    [Test]
    public void Part2()
    {
        var game = new RealDiracDiceGame();
        var result = game.Play(4, 8);

        Assert.That(result, Is.EqualTo(444356092776315));
    }
}