﻿using System.Collections.Generic;

namespace Aoc.ConsoleTools;

public class Parameters
{
    public int? Day { get; }
    public int? Year { get; }
    public bool RunSlowOnly { get; }
    public bool RunCommentedOnly { get; }
    public bool RunFunOnly { get; }
    public bool ShowHelp { get; }


    public Parameters(int? day = null, int? year = null, bool runSlowOnly = false, bool runCommentedOnly = false, bool runFunOnly = false, bool showHelp = false)
    {
        Day = day;
        Year = year;
        RunSlowOnly = runSlowOnly;
        RunCommentedOnly = runCommentedOnly;
        RunFunOnly = runFunOnly;
        ShowHelp = showHelp;
    }

    public static Parameters Parse(IEnumerable<string> args)
    {
        var parser = new ParameterParser(args);
        var day = parser.GetIntValue("-d", "--day");
        var year = parser.GetIntValue("-y", "--year");
        var runSlow = parser.GetBoolValue("-s", "--slow") ?? false;
        var runCommented = parser.GetBoolValue("-c", "--comment") ?? false;
        var runFun = parser.GetBoolValue("-f", "--fun") ?? false;
        var showHelp = parser.GetBoolValue("-h", "--help") ?? false;

        return new Parameters(day, year, runSlow, runCommented, runFun, showHelp);
    }

    public static Parameters Parse(string args)
    {
        return Parse(args.Split(' '));
    }
}