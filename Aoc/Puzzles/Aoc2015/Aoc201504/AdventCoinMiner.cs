using Pzl.Tools.Cryptography;

namespace Pzl.Aoc.Puzzles.Aoc2015.Aoc201504;

public class AdventCoinMiner
{
    public static int Mine(string key, int leadingZeros, int startIndex = 1)
    {
        var index = startIndex;
        var hashFactory = new Hashfactory();
        var isCoinFound = GetCompareFunc(leadingZeros);
        while (true)
        {
            var hashedBytes = hashFactory.ByteHashFromString($"{key}{index}");

            if (isCoinFound(hashedBytes))
                return index;

            index++;
        }
    }

    private static Func<byte[], bool> GetCompareFunc(int leadingZeros)
        => leadingZeros == 5 ? StartsWithFiveZeros : StartsWithSixZeros;

    private static bool StartsWithFiveZeros(byte[] bytes) 
        => bytes[0] == 0 && bytes[1] == 0 && bytes[2] < 10;

    private static bool StartsWithSixZeros(byte[] bytes) 
        => bytes[0] == 0 && bytes[1] == 0 && bytes[2] == 0;
}