using System.Collections.Immutable;

namespace Pzl.Aoc.Puzzles.Aoc2018.Aoc201825;

public class Constellation
{
    private readonly IList<Point4d> _points;
    public IReadOnlyList<Point4d> Points => _points.ToImmutableList();

    public Constellation()
    {
        _points = new List<Point4d>();
    }

    public bool IsClose(Point4d point)
    {
        return Points.Any(o => o.ManhattanDistanceTo(point) < 4);
    }

    public bool IsClose(Constellation otherConstellation)
    {
        foreach (var point in otherConstellation.Points)
        {
            if (IsClose(point))
            {
                return true;
            }
        }

        return false;
    }

    public void Add(Point4d point)
    {
        _points.Add(point);
    }

    public void Add(Constellation constellation)
    {
        foreach (var point in constellation.Points)
        {
            _points.Add(point);
        }
    }
}