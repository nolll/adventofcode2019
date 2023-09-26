﻿using Common.Puzzles;

namespace Aoc.Puzzles.Aoc2015.Aoc201518;

public class Aoc201518 : AocPuzzle
{
    public override string Name => "Like a GIF For Your Yard";

    protected override PuzzleResult RunPart1()
    {
        var gif = new AnimatedGif(InputFile);
        gif.RunAnimation(100);
        return new PuzzleResult(gif.LightCount, 821);
    }

    protected override PuzzleResult RunPart2()
    {
        var gif = new AnimatedGif(InputFile, true);
        gif.RunAnimation(100);
        return new PuzzleResult(gif.LightCount, 886);
    }
}