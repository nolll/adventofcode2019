﻿using Core.Platform;

namespace Core.Puzzles.Year2021.Day03;

public class Year2021Day03 : Puzzle
{
    public override string Title => "Binary Diagnostic";

    public override PuzzleResult RunPart1()
    {
        var diagnostics = new BinaryDiagnostics();
        var result = diagnostics.GetFuelConsumption(FileInput);
            
        return new PuzzleResult(result, 845186);
    }

    public override PuzzleResult RunPart2()
    {
        var diagnostics = new BinaryDiagnostics();
        var result = diagnostics.GetLifeSupportRating(FileInput);

        return new PuzzleResult(result, 4636702);
    }
}