using Core.Common.CoordinateSystems;
using Core.Common.CoordinateSystems.CoordinateSystem3D;

namespace Core.Puzzles.Year2021.Day22;

public class RebootInstruction
{
    public string Mode { get; }
    public Matrix3DAddress From { get; }
    public Matrix3DAddress To { get; }

    public RebootInstruction(string mode, Matrix3DAddress from, Matrix3DAddress to)
    {
        Mode = mode;
        From = from;
        To = to;
    }
}