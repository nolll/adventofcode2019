using Pzl.Common;

namespace Pzl.Aoc.Puzzles.Aoc2020.Aoc202004;

[Name("Passport Processing")]
public class Aoc202004 : AocPuzzle
{
    public PuzzleResult RunPart1(string input)
    {
        var processor = new PassportProcessor(input);
        var passportCount = processor.GetNumberOfPassportsThatHasAllFields();
        return new PuzzleResult(passportCount, "1e10a803d525ec160795a9bed9161106");
    }

    public PuzzleResult RunPart2(string input)
    {
        var processor = new PassportProcessor(input);
        var passportCount = processor.GetNumberOfValidPassports();
        return new PuzzleResult(passportCount, "4648ca473c884f7676991b343c2db8e0");
    }
}