using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.CoordinateSystems.CoordinateSystem2D;
using Common.Strings;

namespace Aoc.Puzzles.Aoc2016.Day02;

public class DiamondKeyCodeFinder
{
    private readonly Matrix<char> _buttons;

    public DiamondKeyCodeFinder()
    {
        _buttons = BuildButtonMatrix();
    }

    public string Find(string input)
    {
        var commandLines = ParseCommands(input);
        var code = new StringBuilder();
        foreach (var commandLine in commandLines)
        {
            foreach (var command in commandLine)
            {
                Move(command);
            }

            code.Append(_buttons.ReadValue());
        }
        return code.ToString();
    }

    private void Move(char direction)
    {
        if (direction == 'U')
        {
            _buttons.TryMoveUp();
            if (_buttons.ReadValue() == '.')
                _buttons.MoveDown();
        }

        if (direction == 'R')
        {
            _buttons.TryMoveRight();
            if (_buttons.ReadValue() == '.')
                _buttons.MoveLeft();
        }

        if (direction == 'D')
        {
            _buttons.TryMoveDown();
            if (_buttons.ReadValue() == '.')
                _buttons.MoveUp();
        }

        if (direction == 'L')
        {
            _buttons.TryMoveLeft();
            if (_buttons.ReadValue() == '.')
                _buttons.MoveRight();
        }
    }

    private static Matrix<char> BuildButtonMatrix()
    {
        const string input = """
..1..
.234.
56789
.ABC.
..D..
""";

        var matrix = MatrixBuilder.BuildCharMatrix(input);
        matrix.MoveTo(0, 2);
        return matrix;
    }

    private static IList<char[]> ParseCommands(string input)
    {
        return PuzzleInputReader.ReadLines(input).Select(o => o.ToCharArray()).ToList();
    }
}