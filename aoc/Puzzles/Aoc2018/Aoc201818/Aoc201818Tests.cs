using NUnit.Framework;

namespace Aoc.Puzzles.Aoc2018.Aoc201818;

public class Aoc201818Tests
{
    [Test]
    public void ResourceValueIsCorrect()
    {
        const string input = """
.#.#...|#.
.....#|##|
.|..|...#.
..|#.....#
#.#|||#|#|
...#.||...
.|....|...
||...#|.#|
|.||||..|.
...#.|..|.
""";

        var collection = new LumberCollection(input);
        collection.Run(10);
        Assert.That(collection.ResourceValue, Is.EqualTo(1147));
    }
}