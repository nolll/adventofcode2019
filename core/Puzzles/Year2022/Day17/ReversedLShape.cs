using Core.Common.CoordinateSystems.CoordinateSystem2D;

namespace Core.Puzzles.Year2022.Day17;

public class ReversedLShape : TetrisShape
{
    private readonly MatrixAddress[] _shape = {
        new(0, 0),
        new(1, 0),
        new(2, 0),
        new(2, -1),
        new(2, -2)
    };

    private readonly MatrixAddress[] _left = {
        new(-1, 0),
        new(1, -1),
        new(1, -2)
    };

    private readonly MatrixAddress[] _right =
    {
        new(3, 0),
        new(3, -1),
        new(3, -2)
    };

    private readonly MatrixAddress[] _down =
    {
        new(0, 1),
        new(1, 1),
        new(2, 1),
    };

    public ReversedLShape() : base(3, 3)
    {
    }

    public override bool CanMoveLeft(IMatrix<char> matrix, MatrixAddress bottomLeft)
    {
        return CheckCoords(matrix, bottomLeft, _left);
    }

    public override bool CanMoveRight(IMatrix<char> matrix, MatrixAddress bottomLeft)
    {
        return CheckCoords(matrix, bottomLeft, _right);
    }

    public override bool CanMoveDown(IMatrix<char> matrix, MatrixAddress bottomLeft)
    {
        return CheckCoords(matrix, bottomLeft, _down);
    }

    public override void Paint(IMatrix<char> matrix, MatrixAddress bottomLeft)
    {
        Paint(matrix, bottomLeft, _shape);
    }
}