using System.Collections.Generic;
using System.Linq;
using Core.Common.CoordinateSystems.CoordinateSystem2D;

namespace Core.Puzzles.Year2019.Day17;

public class ScaffoldIntersectionFinder
{
    private readonly IMatrix<char> _matrix;

    public ScaffoldIntersectionFinder(string input)
    {
        _matrix = BuildMatrix(input);
    }

    public int GetSumOfAlignmentParameters()
    {
        var intersections = GetIntersections().ToList();
        var sum = intersections.Sum(o => o.X * o.Y);
        return sum;
    }

    private IEnumerable<MatrixAddress> GetIntersections()
    {
        return _matrix.Coords.Where(coord => IsIntersection(coord.X, coord.Y)).ToList();
    }

    private char? GetValueAt(MatrixAddress coord)
    {
        if (_matrix.IsOutOfRange(coord))
            return null;
        return _matrix.ReadValueAt(coord);
    }

    private bool IsIntersection(int x, int y)
    {
        var v = GetValueAt(new MatrixAddress(x, y));
        if (v == '.')
            return false;

        v = GetValueAt(new MatrixAddress(x, y - 1));
        if (v == '.')
            return false;

        v = GetValueAt(new MatrixAddress(x + 1, y));
        if (v == '.')
            return false;

        v = GetValueAt(new MatrixAddress(x, y + 1));
        if (v == '.')
            return false;

        v = GetValueAt(new MatrixAddress(x - 1, y));
        if (v == '.')
            return false;

        return true;
    }

    private IMatrix<char> BuildMatrix(string map)
    {
        var matrix = new QuickMatrix<char>();
        var rows = map.Trim().Split('\n');
        var y = 0;
        foreach (var row in rows)
        {
            var x = 0;
            var chars = row.Trim().ToCharArray();
            foreach (var c in chars)
            {
                matrix.MoveTo(x, y);
                matrix.WriteValue(c);
                x += 1;
            }

            y += 1;
        }

        return matrix;
    }
}