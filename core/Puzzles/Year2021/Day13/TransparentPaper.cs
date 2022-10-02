using System.Collections.Generic;
using System.Linq;
using Core.Common.CoordinateSystems;
using Core.Common.Strings;

namespace Core.Puzzles.Year2021.Day13;

public class TransparentPaper
{
    private IMatrix<char> _matrix;
    private readonly IList<string> _folds;

    public TransparentPaper(string input)
    {
        var groups = PuzzleInputReader.ReadLineGroups(input);

        _matrix = BuildMatrix(groups.First());
        _folds = groups[1];
    }

    private static Matrix<char> BuildMatrix(IEnumerable<string> rows)
    {
        var matrix = new Matrix<char>(defaultValue: '.');

        foreach (var row in rows)
        {
            var parts = row.Split(',').Select(int.Parse).ToArray();
            var x = parts[0];
            var y = parts[1];

            matrix.MoveTo(x, y);
            matrix.WriteValue('#');
        }

        matrix.MoveTo(0, 0);

        return matrix;
    }

    public int DotCountAfterFirstFold()
    {
        Fold(_folds.First());
        return _matrix.Values.Count(o => o == '#');
    }

    public string MessageAfterFold()
    {
        foreach (var fold in _folds) 
            Fold(fold);

        return _matrix.Print();
    }

    private void Fold(string fold)
    {
        var (dimension, value) = ParseFoldInstruction(fold);
        Fold(dimension, value);
    }

    private (char dimension, int value) ParseFoldInstruction(string s)
    {
        var parts = s.Split(' ');
        var dimension = parts[2][0];
        var value = int.Parse(parts[2][2..]);
        return (dimension, value);
    }

    private void Fold(char dimension, int value)
    {
        if (dimension == 'x')
            FoldHorizontal(value);
        else
            FoldVertical(value);
    }

    private void FoldHorizontal(int foldCol)
    {
        var keepTo = new MatrixAddress(foldCol - 1, _matrix.Height - 1);
        var foldFrom = new MatrixAddress(foldCol + 1, 0);

        var newMatrix = _matrix.Copy().Slice(to: keepTo);
        var foldMatrix = _matrix.Copy().Slice(foldFrom);
        foldMatrix = foldMatrix.FlipHorizontal();
        var widthDiff = newMatrix.Width - foldMatrix.Width;

        for (var y = 0; y < newMatrix.Height; y++)
        {
            for (var x = widthDiff; x < newMatrix.Width; x++)
            {
                var v = foldMatrix.ReadValueAt(x - widthDiff, y);
                if (v == '#')
                {
                    newMatrix.MoveTo(x, y);
                    newMatrix.WriteValue(v);
                }
            }
        }

        _matrix = newMatrix;
    }

    private void FoldVertical(int foldRow)
    {
        var keepTo = new MatrixAddress(_matrix.Width - 1, foldRow - 1);
        var foldFrom = new MatrixAddress(0, foldRow + 1);

        var newMatrix = _matrix.Copy().Slice(to: keepTo);
        var foldMatrix = _matrix.Copy().Slice(foldFrom);
        foldMatrix = foldMatrix.FlipVertical();
        var heightDiff = newMatrix.Height - foldMatrix.Height;

        for (var y = heightDiff; y < newMatrix.Height; y++)
        {
            for (var x = 0; x < newMatrix.Width; x++)
            {
                var v = foldMatrix.ReadValueAt(x, y - heightDiff);
                if (v == '#')
                {
                    newMatrix.MoveTo(x, y);
                    newMatrix.WriteValue(v);
                }
            }
        }

        _matrix = newMatrix;
    }
}