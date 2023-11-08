﻿using Common.Formatting;
using Common.Puzzles;
using Spectre.Console;
using Timer = Common.Timing.Timer;

namespace Common.Runners;

public class InSequenceSinglePuzzleRunner : SinglePuzzleRunner
{
    private const int ResultColumnWidth = 10;
    private const int CommentColumnWidth = 24;
    private const int TruncatedCommentLength = CommentColumnWidth - 3;

    private readonly Puzzle _puzzle;
    private readonly TimeSpan _timeoutTimespan;
    private readonly string _title;
    private readonly string _commentMarkup;
    private readonly string[] _markups;

    public InSequenceSinglePuzzleRunner(Puzzle puzzle, TimeSpan timeoutTimespan)
    {
        _puzzle = puzzle;
        _timeoutTimespan = timeoutTimespan;

        _title = _puzzle.ListTitle.PadRight(11);
        _commentMarkup = MarkupComment(_puzzle.Comment);
        _markups = new string[_puzzle.RunFunctions.Count];
        for (var i = 0; i < _puzzle.RunFunctions.Count; i++)
        {
            _markups[i] = PadResult(string.Empty);
        }
    }

    public void Run()
    {
        PrintRow();
        for (var i = 0; i < _puzzle.RunFunctions.Count; i++)
        {
            var runFunc = _puzzle.RunFunctions[i];
            RunPart(() => runFunc(), i);
        }
        AnsiConsole.WriteLine();
    }

    private void RunPart(Func<PuzzleResult> runFunc, int index)
    {
        var status = PuzzleResultStatus.Empty;
        var timer = new Timer();
        var time = TimeSpan.Zero;
        var waited = false;
        var cancellation = new CancellationTokenSource();
        var task = Task.Run(() => status = runFunc().Status, cancellation.Token);
        while (!task.IsCompleted)
        {
            if (timer.FromStart >= _timeoutTimespan)
            {
                cancellation.Cancel();
                status = PuzzleResultStatus.Timeout;
                break;
            }

            waited = true;
            time = timer.FromStart;
            UpdateResult(index, PadResult(Formatter.FormatTime(time)));
            PrintRow();
            Thread.Sleep(ProgressWaitTime);
        }

        status = task.IsFaulted ? PuzzleResultStatus.Failed : status;
        time = waited ? time : timer.FromStart;
        UpdateResult(index, MarkupTime(time, status));
        PrintRow();
    }

    private void UpdateResult(int index, string markup) => _markups[index] = markup;

    private string MarkupTime(TimeSpan time, PuzzleResultStatus status)
    {
        return status switch
        {
            PuzzleResultStatus.Correct =>
                MarkupColor(PadResult(Formatter.FormatTime(time)), Color.Green),
            PuzzleResultStatus.Failed or PuzzleResultStatus.Wrong => 
                MarkupColor(PadResult(Formatter.FormatTime(time)), Color.Red),
            PuzzleResultStatus.Timeout => 
                MarkupColor(PadResult($">{Formatter.FormatTime(_timeoutTimespan, 0)}"), Color.Red),
            _ => MarkupColor(PadResult(Formatter.FormatTime(time)), Color.Yellow)
        };
    }

    private static string PadResult(string s) => Pad(s, ResultColumnWidth);
    private static string PadComment(string s) => Pad(s, CommentColumnWidth);
    private static string Pad(string s, int width) => s.PadRight(width);

    private void PrintRow()
    {
        var results = string.Join(" | ", _markups);
        AnsiConsole.Markup($"\r| {_title} | {results} | {_commentMarkup} |");
    }

    private static string MarkupComment(string? comment) =>
        comment is null
            ? PadComment(string.Empty)
            : MarkupColor(PadComment(TruncateComment(comment)), Color.Yellow);

    private static string TruncateComment(string fullComment) =>
        fullComment.Length > TruncatedCommentLength
            ? fullComment[..TruncatedCommentLength] + "..."
            : fullComment;
}