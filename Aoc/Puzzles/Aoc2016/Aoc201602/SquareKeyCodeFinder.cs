using System.Text;
using Pzl.Tools.CoordinateSystems.CoordinateSystem2D;
using Pzl.Tools.Strings;

namespace Pzl.Aoc.Puzzles.Aoc2016.Aoc201602;

public class SquareKeyCodeFinder
{
    private readonly Matrix<char> _buttons = BuildButtonMatrix();

    public string Find(string input)
    {
        var commandLines = ParseCommands(input);
        var code = new StringBuilder();
        foreach (var commandLine in commandLines)
        {
            foreach (var command in commandLine) 
                Move(command);

            code.Append(_buttons.ReadValue());
        }
        return code.ToString();
    }

    private void Move(char direction)
    {
        var dir = DirectionConverter.GetDirection(direction);
        _buttons.TryMove(dir);
    }

    private static Matrix<char> BuildButtonMatrix()
    {
        const string input = """
                             123
                             456
                             789
                             """;

        var matrix = MatrixBuilder.BuildCharMatrix(input);
        matrix.MoveTo(1, 1);
        return matrix;
    }

    private static IList<char[]> ParseCommands(string input) => 
        StringReader.ReadLines(input).Select(o => o.ToCharArray()).ToList();
}