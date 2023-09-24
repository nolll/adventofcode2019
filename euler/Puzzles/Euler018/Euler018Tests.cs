using NUnit.Framework;

namespace Euler.Puzzles.Euler018;

public class Euler018Tests
{
    [Test]
    public void Test()
    {
        const string triangle = @"
3
7 4
2 4 6
8 5 9 3";

        var puzzle = new Euler018();
        var result = puzzle.Run(triangle);

        Assert.That(result, Is.EqualTo(23));
    }
}