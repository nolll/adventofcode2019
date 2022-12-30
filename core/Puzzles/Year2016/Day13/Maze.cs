using System;
using System.Collections.Generic;
using System.Linq;
using Core.Common.CoordinateSystems.CoordinateSystem2D;

namespace Core.Puzzles.Year2016.Day13;

public class Maze
{
    private readonly IMatrix<char> _matrix;

    public Maze(in int width, in int height, in int secretNumber)
    {
        _matrix = BuildMatrix(width, height, secretNumber);
    }

    public int StepCountToTarget(int targetX, int targetY) => PathFinder.CachedStepCountTo(_matrix, new MatrixAddress(1, 1), new MatrixAddress(targetX, targetY));
    public int LocationCountAfter(int steps) => LocationCountAfter(new MatrixAddress(1, 1), steps);

    private int LocationCountAfter(MatrixAddress from, int steps)
    {
        var queue = new List<MatrixAddress> { from };
        var i = 0;
        while (i <= steps)
        {
            var newQueue = new List<MatrixAddress>();
            foreach (var coord in queue)
            {
                _matrix.MoveTo(coord);
                _matrix.WriteValue('O');
                var adjacentCoords = _matrix.PerpendicularAdjacentCoords.Where(o => _matrix.ReadValueAt(o) == '.').ToList();
                newQueue.AddRange(adjacentCoords);
            }

            queue = newQueue;
            i++;
        }

        return _matrix.Values.Count(o => o == 'O');
    }

    private static IMatrix<char> BuildMatrix(in int width, in int height, in int secretNumber)
    {
        var matrix = new QuickMatrix<char>();
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var value = x * x + 3 * x + 2 * x * y + y + y * y + secretNumber;
                var binary = Convert.ToString(value, 2);
                var numberOfSetBits = binary.Count(o => o == '1');
                var isOpenSpace = numberOfSetBits % 2 == 0;
                var c = isOpenSpace ? '.' : '#';
                matrix.WriteValueAt(x, y, c);
            }
        }

        return matrix;
    }
}