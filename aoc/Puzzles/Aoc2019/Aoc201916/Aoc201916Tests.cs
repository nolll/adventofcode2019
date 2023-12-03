using FluentAssertions;
using NUnit.Framework;

namespace Puzzles.Aoc.Puzzles.Aoc2019.Aoc201916;

public class Aoc201916Tests
{
    [Test]
    public void SimpleAfterOnePhase()
    {
        const string input = "12345678";
        var algorithm = new FrequencyAlgorithmPart1(input);
        var result = algorithm.Run(1);

        result.Should().Be("48226158");
    }

    [Test]
    public void SimpleAfterTwoPhases()
    {
        const string input = "12345678";
        var algorithm = new FrequencyAlgorithmPart1(input);
        var result = algorithm.Run(2);

        result.Should().Be("34040438");
    }

    [Test]
    public void SimpleAfterThreePhases()
    {
        const string input = "12345678";
        var algorithm = new FrequencyAlgorithmPart1(input);
        var result = algorithm.Run(3);

        result.Should().Be("03415518");
    }

    [Test]
    public void SimpleAfterFourPhases()
    {
        const string input = "12345678";
        var algorithm = new FrequencyAlgorithmPart1(input);
        var result = algorithm.Run(4);

        result.Should().Be("01029498");
    }

    [TestCase("80871224585914546619083218645595", "24176176")]
    [TestCase("19617804207202209144916044189917", "73745418")]
    [TestCase("69317163492948606335995924319873", "52432133")]
    public void FirstEightDigitsAfter100Phases(string input, string expected)
    {
        var algorithm = new FrequencyAlgorithmPart1(input);
        var result = algorithm.Run(100);

        result.Should().Be(expected);
    }

    [TestCase("03036732577212944063491565474664", "84462026")]
    [TestCase("02935109699940807407585447034323", "78725270")]
    [TestCase("03081770884921959731165446850517", "53553731")]
    public void MessageAfter100RealPhases(string input, string expected)
    {
        var algorithm = new FrequencyAlgorithmPart2(input);
        var result = algorithm.Run(100);

        result.Should().Be(expected);
    }
}