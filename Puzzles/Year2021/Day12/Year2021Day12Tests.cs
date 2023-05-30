using NUnit.Framework;

namespace Aoc.Puzzles.Year2021.Day12;

public class Year2021Day12Tests
{
    [Test]
    public void Part1()
    {
        var caveSystem = new CaveSystem(Input.Trim(), false);
        var result = caveSystem.CountPaths();

        Assert.That(result, Is.EqualTo(10));
    }

    [Test]
    public void Part2()
    {
        var caveSystem = new CaveSystem(Input.Trim(), true);
        var result = caveSystem.CountPaths();

        Assert.That(result, Is.EqualTo(36));
    }

    private const string Input = """
start-A
start-b
A-c
A-b
b-d
A-end
b-end
""";
}