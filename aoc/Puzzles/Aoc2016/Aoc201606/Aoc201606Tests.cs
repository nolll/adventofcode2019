using NUnit.Framework;

namespace Aoc.Puzzles.Aoc2016.Aoc201606;

public class Aoc201606Tests
{
    private const string Input = """
eedadn
drvtee
eandsr
raavrd
atevrs
tsrnev
sdttsa
rasrtv
nssdts
ntnada
svetve
tesnvt
vntsnd
vrdear
dvrsen
enarar
""";

    [Test]
    public void MessageIsCorrect_MostCommon()
    {
        var reader = new RepetitionCodeReader();
        var coin = reader.ReadMostCommon(Input);

        Assert.That(coin, Is.EqualTo("easter"));
    }

    [Test]
    public void MessageIsCorrect_LeastCommon()
    {
        var reader = new RepetitionCodeReader();
        var coin = reader.ReadLeastCommon(Input);

        Assert.That(coin, Is.EqualTo("advent"));
    }
}