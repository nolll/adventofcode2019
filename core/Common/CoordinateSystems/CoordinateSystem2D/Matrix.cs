using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Common.CoordinateSystems.CoordinateSystem2D;

public class Matrix<T> : BaseMatrix
{
    private readonly T _defaultValue;
    private readonly IDictionary<MatrixAddress, T> _matrix;

    public int Width => XMax - XMin + 1;
    public int Height => YMax - YMin + 1;
    public int XMin { get; private set; }
    public int XMax { get; private set; }
    public int YMin { get; private set; }
    public int YMax { get; private set; }

    public MatrixDirection Direction { get; private set; }
    public MatrixAddress Address { get; private set; }
    public MatrixAddress StartAddress { get; }
    public bool IsAtTop => Address.Y == 0;
    public bool IsAtRightEdge => Address.X == Width - 1;
    public bool IsAtBottom => Address.Y == Height - 1;
    public bool IsAtLeftEdge => Address.X == 0;
    public MatrixAddress Center => new(Width / 2, Height / 2);

    public Matrix(int width = 1, int height = 1, T defaultValue = default)
        : this(
            new MatrixAddress(0, 0), 
            new MatrixAddress(width - 1, height - 1),
            new Dictionary<MatrixAddress, T>(), 
            defaultValue)
    {
    }

    private Matrix(T defaultValue)
    {
        _defaultValue = defaultValue;
        Address = new MatrixAddress(0, 0);
        StartAddress = new MatrixAddress(0, 0);
        Direction = MatrixDirection.Up;
    }

    private Matrix(
        MatrixAddress min, 
        MatrixAddress max, 
        IDictionary<MatrixAddress, T> values, 
        T defaultValue = default)
        : this(defaultValue)
    {
        _matrix = values;
        XMin = min.X;
        XMax = max.X;
        YMin = min.Y;
        YMax = max.Y;
    }

    public IEnumerable<T> Values
    {
        get
        {
            foreach (var coord in Coords)
            {
                if (_matrix.TryGetValue(coord, out var v))
                    yield return v;
                else
                    yield return _defaultValue;
            }
        }
    }

    public IEnumerable<MatrixAddress> Coords
    {
        get
        {
            for (var y = YMin; y <= YMax; y++)
            {
                for (var x = XMin; x <= XMax; x++)
                {
                    yield return new MatrixAddress(x, y);
                }
            }
        }
    }

    public T ReadValue() => ReadValueAt(Address);
    public T ReadValueAt(int x, int y) => ReadValueAt(new MatrixAddress(x, y));

    public T ReadValueAt(MatrixAddress coord)
    {
        return _matrix.TryGetValue(coord, out var v)
            ? v
            : _defaultValue;
    }

    public void WriteValue(T value) => WriteValueAt(Address, value);
    public void WriteValueAt(int x, int y, T value) => WriteValueAt(new MatrixAddress(x, y), value);

    public void WriteValueAt(MatrixAddress coord, T value)
    {
        if (coord.X < XMin)
            XMin = coord.X;
        else if (coord.X > XMax)
            XMax = coord.X;

        if (coord.Y < YMin)
            YMin = coord.Y;
        else if (coord.Y > YMax)
            YMax = coord.Y;

        _matrix[coord] = value;
    }

    public IList<T> PerpendicularAdjacentValues => PerpendicularAdjacentCoords.Select(ReadValueAt).ToList();
    public IList<T> PerpendicularAdjacentValuesTo(MatrixAddress address) => PerpendicularAdjacentCoordsTo(address).Select(ReadValueAt).ToList();
    public IList<MatrixAddress> PerpendicularAdjacentCoords => PerpendicularAdjacentCoordsTo(Address);
    public IList<MatrixAddress> PerpendicularAdjacentCoordsTo(MatrixAddress address) => PossiblePerpendicularAdjacentCoordsTo(address).Where(o => !IsOutOfRange(o)).ToList();

    private static IEnumerable<MatrixAddress> PossiblePerpendicularAdjacentCoordsTo(MatrixAddress address) => new List<MatrixAddress>
    {
        new(address.X, address.Y - 1),
        new(address.X + 1, address.Y),
        new(address.X, address.Y + 1),
        new(address.X - 1, address.Y)
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
                var coord = new MatrixAddress(address.X + dx, address.Y + dy);
                if (!coord.Equals(address))
                    yield return coord;
            }
        }
    }

    public bool MoveTo(MatrixAddress address) => MoveTo(address, true);
    public bool MoveTo(int x, int y) => MoveTo(new MatrixAddress(x, y), true);
    public bool TryMoveTo(MatrixAddress address) => MoveTo(address, false);
    public bool TryMoveTo(int x, int y) => MoveTo(new MatrixAddress(x, y), false);
    public bool MoveForward() => MoveForward(true);
    private bool MoveForward(bool extend) => MoveTo(new MatrixAddress(Address.X + Direction.X, Address.Y + Direction.Y), extend);
    public bool TryMoveForward() => MoveForward(false);
    public bool MoveBackward() => MoveBackward(true);
    private bool MoveBackward(bool extend) => MoveTo(new MatrixAddress(Address.X - Direction.X, Address.Y - Direction.Y), extend);
    public bool TryMoveBackward() => MoveBackward(false);
    public bool MoveUp(int steps = 1) => MoveUp(steps, true);
    private bool MoveUp(int steps, bool extend) => MoveTo(new MatrixAddress(Address.X, Address.Y - steps), extend);
    public bool TryMoveUp(int steps = 1) => MoveUp(steps, false);
    public bool MoveRight(int steps = 1) => MoveRight(steps, true);
    private bool MoveRight(int steps, bool extend) => MoveTo(new MatrixAddress(Address.X + steps, Address.Y), extend);
    public bool TryMoveRight(int steps = 1) => MoveRight(steps, false);
    public bool MoveDown(int steps = 1) => MoveDown(steps, true);
    private bool MoveDown(int steps, bool extend) => MoveTo(new MatrixAddress(Address.X, Address.Y + steps), extend);
    public bool TryMoveDown(int steps = 1) => MoveDown(steps, false);
    public bool MoveLeft(int steps = 1) => MoveLeft(steps, true);
    private bool MoveLeft(int steps, bool extend) => MoveTo(new MatrixAddress(Address.X - steps, Address.Y), extend);
    public bool TryMoveLeft(int steps = 1) => MoveLeft(steps, false);

    private bool MoveTo(MatrixAddress address, bool extend)
    {
        if (IsOutOfRange(address))
        {
            if (extend)
                ExtendMatrix(address);
            else
                return false;
        }

        var x = address.X > XMin ? address.X : XMin;
        var y = address.Y > YMin ? address.Y : YMin;
        Address = new MatrixAddress(x, y);
        return true;
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

    private void ExtendMatrix(MatrixAddress address)
    {
        ExtendX(address);
        ExtendY(address);
    }

    private void ExtendX(MatrixAddress address)
    {
        if (address.X < XMin)
            ExtendLeft(address);
        ExtendRight(address);
    }

    private void ExtendLeft(MatrixAddress address) => AddCols(-address.X, MatrixAddMode.Prepend);

    private void ExtendRight(MatrixAddress address)
    {
        var extendBy = address.X - XMax;
        if (extendBy > 0)
            AddCols(extendBy, MatrixAddMode.Append);
    }

    private void ExtendY(MatrixAddress address)
    {
        if (address.Y < YMin)
            ExtendTop(address);
        ExtendBottom(address);
    }

    private void ExtendTop(MatrixAddress address) => AddRows(-address.Y, MatrixAddMode.Prepend);

    private void ExtendBottom(MatrixAddress address)
    {
        var extendBy = address.Y - YMax;
        if (extendBy > 0)
            AddRows(extendBy, MatrixAddMode.Append);
    }

    private void AddRows(int numberOfRows, MatrixAddMode addMode)
    {
        if (addMode == MatrixAddMode.Prepend)
            YMin -= numberOfRows;
        else
            YMax += numberOfRows;
    }

    private void AddCols(int numberOfCols, MatrixAddMode addMode)
    {
        if (addMode == MatrixAddMode.Prepend)
            XMin -= numberOfCols;
        else
            XMax += numberOfCols;
    }

    public void ExtendAllDirections(int steps = 1)
    {
        ExtendUp(steps);
        ExtendRight(steps);
        ExtendDown(steps);
        ExtendLeft(steps);
    }

    public void ExtendUp(int steps = 1) => AddRows(steps, MatrixAddMode.Prepend);
    public void ExtendRight(int steps = 1) => AddCols(steps, MatrixAddMode.Append);
    public void ExtendDown(int steps = 1) => AddRows(steps, MatrixAddMode.Append);
    public void ExtendLeft(int steps = 1) => AddCols(steps, MatrixAddMode.Prepend);

    public string Print(bool markCurrentAddress = false, bool markStartAddress = false, T currentAddressMarker = default, T startAddressMarker = default, bool spacing = false)
    {
        var sb = new StringBuilder();

        for (var y = YMin; y <= YMax; y++)
        {
            for (var x = XMin; x <= XMax; x++)
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

    public IList<MatrixAddress> FindAddresses(T value)
    {
        var addresses = new List<MatrixAddress>();
        for (var y = YMin; y <= YMax; y++)
        {
            for (var x = XMin; x <= XMax; x++)
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
        return address.Y > YMax ||
               address.Y < YMin ||
               address.X > XMax ||
               address.X < XMin;
    }

    public Matrix<T> Copy()
    {
        var values = _matrix.ToDictionary(item => item.Key, item => item.Value);
        var min = new MatrixAddress(XMin, YMin);
        var max = new MatrixAddress(XMax, YMax);
        return new Matrix<T>(min, max, values, _defaultValue);
    }

    public Matrix<T> RotateLeft()
    {
        var values = _matrix.ToDictionary(item => new MatrixAddress(item.Key.Y, YMax - item.Key.X), item => item.Value);
        var min = new MatrixAddress(YMin, YMin);
        var max = new MatrixAddress(XMax, YMax);
        return new Matrix<T>(min, max, values, _defaultValue);
    }

    public Matrix<T> RotateRight()
    {
        var values = _matrix.ToDictionary(item => new MatrixAddress(XMax - item.Key.Y, item.Key.X), item => item.Value);
        var min = new MatrixAddress(YMin, YMin);
        var max = new MatrixAddress(XMax, YMax);
        return new Matrix<T>(min, max, values, _defaultValue);
    }

    public Matrix<T> Slice(MatrixAddress from = null, MatrixAddress to = null)
    {
        from ??= new MatrixAddress(XMin, YMin);
        to ??= new MatrixAddress(XMax, YMax);
        var dx = from.X;
        var dy = from.Y;
        var values = _matrix
            .Where(item => item.Key.Y >= from.X && item.Key.Y >= from.Y && item.Key.X <= to.X && item.Key.Y <= to.Y)
            .ToDictionary(item => new MatrixAddress(item.Key.X - dx, item.Key.Y - dy), item => item.Value);
        var slicedFrom = new MatrixAddress(from.X - dx, from.Y - dy);
        var slicedTo = new MatrixAddress(to.X - dx, to.Y - dy);
        return new Matrix<T>(slicedFrom, slicedTo, values, _defaultValue);
    }

    public Matrix<T> Slice(MatrixAddress from, int width, int height)
    {
        var to = new MatrixAddress(from.X + width, from.Y + height);
        return Slice(from, to);
    }

    public Matrix<T> FlipVertical()
    {
        var values = _matrix.ToDictionary(item => new MatrixAddress(item.Key.X, YMax - item.Key.Y), item => item.Value);
        var min = new MatrixAddress(YMin, YMin);
        var max = new MatrixAddress(XMax, YMax);
        return new Matrix<T>(min, max, values, _defaultValue);
    }

    public Matrix<T> FlipHorizontal()
    {
        var values = _matrix.ToDictionary(item => new MatrixAddress(XMax - item.Key.X, item.Key.Y), item => item.Value);
        var min = new MatrixAddress(YMin, YMin);
        var max = new MatrixAddress(XMax, YMax);
        return new Matrix<T>(min, max, values, _defaultValue);
    }
}