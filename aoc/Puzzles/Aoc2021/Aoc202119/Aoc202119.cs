﻿using Common.Puzzles;

namespace Aoc.Puzzles.Aoc2021.Aoc202119;

public class Aoc202119 : AocPuzzle
{
    private BeaconSystemResult? _result;

    public override string Name => "Beacon Scanner";

    protected override PuzzleResult RunPart1()
    {
        var result = GetResult();

        return new PuzzleResult(result.BeaconCount, 353);
    }

    protected override PuzzleResult RunPart2()
    {
        var result = GetResult();

        return new PuzzleResult(result.MaxDistance, 10832);
    }

    private BeaconSystemResult GetResult()
    {
        if(_result == null)
        {
            var system = new BeaconSystem();
            _result = system.GetResult(InputFile);
        }

        return _result;
    }
}