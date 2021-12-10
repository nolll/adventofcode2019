using App.Common.CoordinateSystems;

namespace App.Puzzles.Year2019.Day20
{
    public enum PortalType
    {
        Inner,
        Outer
    }

    public abstract class DonutPortal
    {
        public string Name { get; }
        public MatrixAddress Location { get; }
        public MatrixAddress Target { get; }
        public abstract int DepthChange { get; }
        public abstract PortalType Type { get; }

        protected DonutPortal(string name, MatrixAddress location, MatrixAddress target)
        {
            Name = name;
            Location = location;
            Target = target;
        }
    }
}