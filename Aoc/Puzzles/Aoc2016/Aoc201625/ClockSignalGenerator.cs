namespace Pzl.Aoc.Puzzles.Aoc2016.Aoc201625;

public class ClockSignalGenerator
{
    public readonly int LowestA = 0;

    public ClockSignalGenerator()
    {
        var index = 1;
        var output = "";
        const int targetOutputLength = 10;

        while (output != "0101010101")
        {
            output = "";
            var a = index;
            var d = a + 633 * 4;
                
            a = d;
            while (output.Length < targetOutputLength && a != 0)
            {
                var b = a;
                var c = 2;
                a = 0;
                while (b != 0)
                {
                    b--;
                    c--;
                    if (c != 0)
                        continue;
                    a++;
                    c = 2;
                }

                b = 2;
                while (c != 0)
                {
                    b--;
                    c--;
                }

                output += b;
            }
                
            LowestA = index;
            index++;
        }
    }
}