﻿using Common.Puzzles;
using Spectre.Console;

namespace Euler.ConsoleTools;

public class EulerHelpPrinter : IHelpPrinter
{
    public void Print()
    {
        AnsiConsole.WriteLine("My solutions to Project Euler.");
        AnsiConsole.WriteLine("https://github.com/nolll/puzzles/euler");
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine("Usage dotnet run -- [parameters]");
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine("-p    --puzzle    the puzzle to run");
        AnsiConsole.WriteLine("-t    --tags      comma-separated list of tags to filter puzzles");
        AnsiConsole.WriteLine("-h    --help      display this help text");
        AnsiConsole.WriteLine("");
    }
}