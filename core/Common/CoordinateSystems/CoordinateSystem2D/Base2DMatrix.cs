using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Common.CoordinateSystems.CoordinateSystem2D;

public abstract class Base2DMatrix<T> : BaseMatrix
{
    protected readonly T DefaultValue;

    public abstract int Width { get; }
    public abstract int Height { get; }
    public MatrixDirection Direction { get; private set; }
    public MatrixAddress Address { get; private set; }
    public MatrixAddress StartAddress { get; protected set; }
    public bool IsAtTop => Address.Y == 0;
    public bool IsAtRightEdge => Address.X == Width - 1;
    public bool IsAtBottom => Address.Y == Height - 1;
    public bool IsAtLeftEdge => Address.X == 0;
    public MatrixAddress Center => new(Width / 2, Height / 2);

    protected Base2DMatrix(T defaultValue)
    {
        DefaultValue = defaultValue;
        Address = new MatrixAddress(0, 0);
        StartAddress = new MatrixAddress(0, 0);
        Direction = MatrixDirection.Up;
    }

    public IEnumerable<MatrixAddress> Coords
    {
        get
        {
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    yield return new MatrixAddress(x, y);
                }
            }
        }
    }

    public abstract IEnumerable<T> Values { get; }

    public bool TryMoveTo(MatrixAddress address)
    {
        return MoveTo(address, false);
    }

    public bool MoveTo(MatrixAddress address)
    {
        return MoveTo(address, true);
    }

    private bool MoveTo(MatrixAddress address, bool extend)
    {
        if (IsOutOfRange(address))
        {
            if (extend)
                HandleExtend(address);
            else
                return false;
        }

        var x = address.X > 0 ? address.X : 0;
        var y = address.Y > 0 ? address.Y : 0;
        Address = new MatrixAddress(x, y);
        return true;
    }

    protected abstract void HandleExtend(MatrixAddress address);

    public bool TryMoveTo(int x, int y)
    {
        return MoveTo(new MatrixAddress(x, y), false);
    }

    public bool MoveTo(int x, int y)
    {
        return MoveTo(new MatrixAddress(x, y), true);
    }

    public bool TryMoveForward()
    {
        return MoveForward(false);
    }

    public bool MoveForward()
    {
        return MoveForward(true);
    }

    private bool MoveForward(bool extend)
    {
        return MoveTo(new MatrixAddress(Address.X + Direction.X, Address.Y + Direction.Y), extend);
    }

    public bool TryMoveBackward()
    {
        return MoveBackward(false);
    }

    public bool MoveBackward()
    {
        return MoveBackward(true);
    }

    private bool MoveBackward(bool extend)
    {
        return MoveTo(new MatrixAddress(Address.X - Direction.X, Address.Y - Direction.Y), extend);
    }

    public bool TryMoveUp(int steps = 1)
    {
        return MoveUp(steps, false);
    }

    public bool MoveUp(int steps = 1)
    {
        return MoveUp(steps, true);
    }

    private bool MoveUp(int steps, bool extend)
    {
        return MoveTo(new MatrixAddress(Address.X, Address.Y - steps), extend);
    }

    public bool TryMoveRight(int steps = 1)
    {
        return MoveRight(steps, false);
    }

    public bool MoveRight(int steps = 1)
    {
        return MoveRight(steps, true);
    }

    private bool MoveRight(int steps, bool extend)
    {
        return MoveTo(new MatrixAddress(Address.X + steps, Address.Y), extend);
    }

    public bool TryMoveDown(int steps = 1)
    {
        return MoveDown(steps, false);
    }

    public bool MoveDown(int steps = 1)
    {
        return MoveDown(steps, true);
    }

    private bool MoveDown(int steps, bool extend)
    {
        return MoveTo(new MatrixAddress(Address.X, Address.Y + steps), extend);
    }

    public bool TryMoveLeft(int steps = 1)
    {
        return MoveLeft(steps, false);
    }

    public bool MoveLeft(int steps = 1)
    {
        return MoveLeft(steps, true);
    }

    private bool MoveLeft(int steps, bool extend)
    {
        return MoveTo(new MatrixAddress(Address.X - steps, Address.Y), extend);
    }

    public MatrixDirection TurnLeft()
    {
        if (Direction.Equals(MatrixDirection.Up))
            return TurnTo(MatrixDirection.Left);
        if (Direction.Equals(MatrixDirection.Right))
            return TurnTo(MatrixDirection.Up);
        if (Direction.Equals(MatrixDirection.Down))
            return TurnTo(MatrixDirection.Right);
        return TurnTo(MatrixDirection.Down);
    }

    public MatrixDirection TurnRight()
    {
        if (Direction.Equals(MatrixDirection.Up))
            return TurnTo(MatrixDirection.Right);
        if (Direction.Equals(MatrixDirection.Right))
            return TurnTo(MatrixDirection.Down);
        if (Direction.Equals(MatrixDirection.Down))
            return TurnTo(MatrixDirection.Left);
        return TurnTo(MatrixDirection.Up);
    }

    public MatrixDirection TurnTo(MatrixDirection direction)
    {
        Direction = direction;
        return direction;
    }

    public string Print(bool markCurrentAddress = false, bool markStartAddress = false, T currentAddressMarker = default, T startAddressMarker = default, bool spacing = false)
    {
        var sb = new StringBuilder();
        
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (markCurrentAddress && x == Address.X && y == Address.Y)
                    sb.Append('D');
                else if (markStartAddress && x == StartAddress.X && y == StartAddress.Y)
                    sb.Append('S');
                else
                    sb.Append(ReadValueAt(x, y));

                if (spacing)
                    sb.Append('.');
            }

            sb.AppendLine();
        }

        return sb.ToString().Trim();
    }

    public T ReadValue()
    {
        return ReadValueAt(Address.X, Address.Y);
    }

    public T ReadValueAt(MatrixAddress address)
    {
        return ReadValueAt(address.X, address.Y);
    }

    public abstract T ReadValueAt(int x, int y);

    public void WriteValue(T value)
    {
        WriteValueAt(Address.X, Address.Y, value);
    }

    public void WriteValueAt(MatrixAddress address, T value)
    {
        WriteValueAt(address.X, address.Y, value);
    }

    public abstract void WriteValueAt(int x, int y, T value);

    protected abstract IMatrix<T> Create(int width, int height, T defaultValue = default);

    public IList<MatrixAddress> FindAddresses(T value)
    {
        var addresses = new List<MatrixAddress>();
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var address = new MatrixAddress(x, y);
                var val = ReadValueAt(address);
                if (val.Equals(value))
                    addresses.Add(address);
            }
        }

        return addresses;
    }

    public bool IsOutOfRange(MatrixAddress address)
    {
        return address.Y >= Height ||
               address.Y < 0 ||
               address.X >= Width ||
               address.X < 0;
    }

    public IList<T> PerpendicularAdjacentValues => PerpendicularAdjacentCoords.Select(ReadValueAt).ToList();
    public IList<T> PerpendicularAdjacentValuesTo(MatrixAddress address) => PerpendicularAdjacentCoordsTo(address).Select(ReadValueAt).ToList();
    public IList<MatrixAddress> PerpendicularAdjacentCoords => PerpendicularAdjacentCoordsTo(Address);
    public IList<MatrixAddress> PerpendicularAdjacentCoordsTo(MatrixAddress address) => PossiblePerpendicularAdjacentCoordsTo(address).Where(o => !IsOutOfRange(o)).ToList();

    private IEnumerable<MatrixAddress> PossiblePerpendicularAdjacentCoordsTo(MatrixAddress address) => new List<MatrixAddress>
    {
        new MatrixAddress(address.X, address.Y - 1),
        new MatrixAddress(address.X + 1, address.Y),
        new MatrixAddress(address.X, address.Y + 1),
        new MatrixAddress(address.X - 1, address.Y)
    };

    public IList<T> AllAdjacentValues => AllAdjacentCoordsTo(Address).Select(ReadValueAt).ToList();
    public IList<T> AllAdjacentValuesTo(MatrixAddress address) => AllAdjacentCoordsTo(address).Select(ReadValueAt).ToList();
    public IList<MatrixAddress> AllAdjacentCoords => AllAdjacentCoordsTo(Address);
    public IList<MatrixAddress> AllAdjacentCoordsTo(MatrixAddress address) => AllPossibleAdjacentCoordsTo(address).Where(o => !IsOutOfRange(o)).ToList();

    private IEnumerable<MatrixAddress> AllPossibleAdjacentCoordsTo(MatrixAddress address)
    {
        foreach (var dy in AdjacentDeltas)
        {
            foreach (var dx in AdjacentDeltas)
            {
                var coord = new MatrixAddress(address.X + dx, address.Y - dy);
                if (!coord.Equals(address))
                    yield return coord;
            }
        }
    }

    public IMatrix<T> Copy()
    {
        var matrix = Create(Width, Height);
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                matrix.WriteValueAt(x, y, ReadValueAt(x, y));
            }
        }

        matrix.MoveTo(Address);
        return matrix;
    }

    public IMatrix<T> RotateLeft()
    {
        var newMatrix = Create(Height, Width, DefaultValue);
        var oy = 0;
        for (var ox = Width - 1; ox >= 0; ox--)
        {
            var nx = 0;
            for (var ny = 0; ny < Height; ny++)
            {
                newMatrix.WriteValueAt(nx, oy, ReadValueAt(ox, ny));
                nx++;
            }
            oy++;
        }
        return newMatrix;
    }

    public IMatrix<T> RotateRight()
    {
        return RotateLeft().RotateLeft().RotateLeft();
    }

    public IMatrix<T> Slice(MatrixAddress from = null, MatrixAddress to = null)
    {
        from ??= new MatrixAddress(0, 0);
        to ??= new MatrixAddress(Width - 1, Height - 1);
        var xNew = 0;
        var yNew = 0;
        var width = to.X - from.X;
        var height = to.Y - from.Y;
        var newMatrix = Create(width, height, DefaultValue);
        for (var y = from.Y; y <= to.Y; y++)
        {
            for (var x = from.X; x <= to.X; x++)
            {
                newMatrix.MoveTo(xNew, yNew);
                newMatrix.WriteValue(ReadValueAt(x, y));

                xNew++;
            }

            xNew = 0;
            yNew++;
        }
        return newMatrix;
    }

    //public IMatrix<T> Slice(MatrixAddress from = null, MatrixAddress to = null)
    //{
    //    from ??= new MatrixAddress(0, 0);
    //    to ??= new MatrixAddress(Width - 1, Height - 1);
    //    var xNew = 0;
    //    var yNew = 0;
    //    var width = to.X - from.X;
    //    var height = to.Y - from.Y;
    //    var newMatrix = Create(width, height, _defaultValue);
    //    for (var y = from.Y; y <= to.Y; y++)
    //    {
    //        for (var x = from.X; x <= to.X; x++)
    //        {
    //            newMatrix.WriteValueAt(xNew, yNew, ReadValueAt(x, y));

    //            xNew++;
    //        }

    //        xNew = 0;
    //        yNew++;
    //    }
    //    return newMatrix;
    //}

    public IMatrix<T> Slice(MatrixAddress from, int width, int height)
    {
        var to = new MatrixAddress(from.X + width, from.Y + height);
        return Slice(from, to);
    }

    public IMatrix<T> FlipVertical()
    {
        var width = Width;
        var height = Height;
        var newMatrix = Create(width, height, DefaultValue);
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var ny = height - y - 1;
                newMatrix.WriteValueAt(x, ny, ReadValueAt(x, y));
            }
        }
        return newMatrix;
    }

    public IMatrix<T> FlipHorizontal()
    {
        var width = Width;
        var height = Height;
        var newMatrix = Create(width, height, DefaultValue);
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var nx = width - x - 1;
                newMatrix.WriteValueAt(nx, y, ReadValueAt(x, y));
            }
        }
        return newMatrix;
    }
}