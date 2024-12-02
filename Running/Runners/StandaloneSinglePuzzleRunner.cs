﻿using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Pzl.Client.Formatting;
using Pzl.Client.Running.Results;
using Pzl.Common;
using Spectre.Console;
using Timer = Pzl.Client.Timing.Timer;

namespace Pzl.Client.Running.Runners;

public class StandaloneSinglePuzzleRunner : SinglePuzzleRunner
{
    private readonly PuzzleResultVerifier _resultVerifier;
    private readonly PuzzleDefinition _definition;
    private readonly bool _isDebugMode;
    private const int StatusPadding = 15;

    public StandaloneSinglePuzzleRunner(
        PuzzleDefinition puzzle, 
        string hashSeed,
        bool isDebugMode)
    {
        _definition = puzzle;
        _isDebugMode = isDebugMode;
        _resultVerifier = new PuzzleResultVerifier(hashSeed);
    }

    public void Run()
    {
        if(_isDebugMode)
            RunDebugMode();
        else
            RunStandardMode();
    }

    private void RunStandardMode()
    {
        AnsiConsole.Cursor.Show(false);
        WriteHeader(_definition);
        
        var inputs = FileReader.ReadInputs(_definition);
        var instance = PuzzleFactory.CreateInstance(_definition);
        
        for (var i = 0; i < instance.Funcs.Length; i++)
        {
            var func = instance.Funcs[i];
            var input = _definition.HasUniqueInputsPerPart
                ? inputs[i]
                : inputs[0];
            
            string[] p = [input];
            if (func.ParameterCount > 1)
            {
                var additionalInput = FileReader.ReadAdditionalFile(_definition.Type, func.Method);
                p = [.. inputs, additionalInput];
            }

            AnsiConsole.WriteLine();
            RunAndPrintPuzzleResult(i + 1, func, p);
        }

        AnsiConsole.Cursor.Show(true);
    }

    private void RunDebugMode()
    {
        WriteHeader(_definition);
        var inputs = FileReader.ReadInputs(_definition);
        var instance = PuzzleFactory.CreateInstance(_definition);
        
        for (var i = 0; i < instance.Funcs.Length; i++)
        {
            var func = instance.Funcs[i];
            var input = _definition.HasUniqueInputsPerPart
                ? inputs[i]
                : inputs[0];
            object[] p = [input];
            if (func.ParameterCount > 1)
            {
                var additionalInput = FileReader.ReadAdditionalFile(_definition.Type, func.Method);
                p = [.. inputs, additionalInput];
            }
            var result = func.Invoke(p);

            AnsiConsole.WriteLine(result.Answer);
        }
    }

    private static void WriteHeader(PuzzleDefinition puzzle)
    {
        AnsiConsole.WriteLine($"{puzzle.Title}");
        
        if (!string.IsNullOrEmpty(puzzle.Name))
            AnsiConsole.WriteLine(puzzle.Name);
        
        if (puzzle.Comment is not null)
            AnsiConsole.MarkupLine($"[yellow]{puzzle.Comment}[/]");
    }
    
    private void RunAndPrintPuzzleResult(int puzzleIndex, PuzzleFunction func, string[] inputs)
    {
        var result = RunPuzzle(puzzleIndex, func, inputs);
        AnsiConsole.WriteLine();
        WriteAnswer(result);
    }
    
    private VerifiedPuzzleResult RunPuzzle(int puzzleIndex, PuzzleFunction func, string[] inputs)
    {
        PuzzleResult? result = null;
        PrintTime(puzzleIndex);
        var timer = new Timer();
        
        var task = Task.Run(() =>
        { 
            result = func.Invoke(inputs.Select(object (o) => o).ToArray());
        });
        
        while (!task.IsCompleted)
        {
            PrintTime(puzzleIndex, timer.FromStart);
            Thread.Sleep(ProgressWaitTime);
        }

        if (task.IsFaulted && task.Exception is not null)
            throw task.Exception;

        if (task.IsFaulted)
            return VerifiedPuzzleResult.Failed;

        if (result is not null)
            return _resultVerifier.Verify(result);
            
        return VerifiedPuzzleResult.Empty;
    }

    private static void PrintTime(int puzzleNumber, TimeSpan? time = null)
    {
        var formattedTime = time is not null
            ? Formatter.FormatTime(time.Value)
            : string.Empty;

        AnsiConsole.Write($"\rPart {puzzleNumber}: {formattedTime}".PadRight(StatusPadding));
    }
    
    private static void WriteAnswer(VerifiedPuzzleResult? result)
    {
        if (result is null)
            AnsiConsole.MarkupLine(MarkupColor("Missing", Color.Red));
        else if (result.Status is PuzzleResultStatus.Missing)
            AnsiConsole.MarkupLine(MarkupColor("No puzzle", Color.Grey));
        else if (result.Status is PuzzleResultStatus.Correct)
            WriteAnswer(result, Color.Green);
        else if (result.Status is PuzzleResultStatus.Failed or PuzzleResultStatus.Timeout or PuzzleResultStatus.Wrong)
            WriteAnswer(result, Color.Red);
        else
            WriteAnswer(result, Color.Yellow);
    }

    private static void WriteAnswer(VerifiedPuzzleResult result, Color color)
    {
        AnsiConsole.MarkupLine(MarkupColor(result.Answer.Answer, color));
        if(result.Status == PuzzleResultStatus.Completed)
            AnsiConsole.MarkupLine(MarkupColor(result.Hash, Color.Grey));
    }
}