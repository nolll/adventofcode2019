using System;

namespace Core.Puzzles.Year2018.Day24;

public class StalemateException : Exception
{
    public StalemateException()
        : base("Stalemate!")
    {
    }
}