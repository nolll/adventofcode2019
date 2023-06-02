﻿using Aoc.Platform;

namespace Aoc.Puzzles.Year2016.Day14;

public class Year2016Day14 : Puzzle
{
    public override string Title => "One-Time Pad";

    public override string Comment => "Slow hashing";
    public override bool IsSlow => true; // 26s for part 2

    public override PuzzleResult RunPart1()
    {
        var generator = new KeyGenerator();
        var index = generator.GetIndexOfNThKey(Input, 64, 0);
            
        return new PuzzleResult(index, 16_106);
    }

    public override PuzzleResult RunPart2()
    {
        var generator = new KeyGenerator();
        var index = generator.GetIndexOfNThKey(Input, 64, 2016);
            
        return new PuzzleResult(index, 22_423);
    }

    private static string Input => "zpqevtbw";
}