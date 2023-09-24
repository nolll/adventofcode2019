using NUnit.Framework;

namespace Euler.Puzzles.Euler028;

public class Euler028Tests
{
    [Test]
    public void Test()
    {
        var puzzle = new Euler028();
        var result = puzzle.Run(5);

        Assert.That(result, Is.EqualTo(101));
    }
}