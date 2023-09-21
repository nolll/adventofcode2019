using NUnit.Framework;

namespace Aoc.Puzzles.Year2015.Day23;

public class Year2015Day23Tests
{
    [Test]
    public void RegisterAContains2()
    {
        const string input = """
inc a
jio a, +2
tpl a
inc a
""";

        var computer = new ChristmasComputer();
        computer.Run(input.Trim());

        Assert.That(computer.RegisterA, Is.EqualTo(2));
    }
}