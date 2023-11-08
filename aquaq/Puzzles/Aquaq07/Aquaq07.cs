﻿using Common.Puzzles;

namespace Aquaq.Puzzles.Aquaq07;

public class Aquaq07 : AquaqPuzzle
{
    public override string Name => "What is best in life?";

    protected override PuzzleResult Run()
    {
        var games = InputFile.Split(Environment.NewLine)
            .Skip(1)
            .Select(o => o.Split(','))
            .ToList();

        var players = games
            .SelectMany(o => o.Take(2))
            .Distinct()
            .ToDictionary(k => k, _ => 1200d);

        foreach (var line in games)
        {
            var results = line[2].Split('-').Select(double.Parse).ToArray();
            var winnerIndex = results[0] > results[1] ? 0 : 1;
            var loserIndex = winnerIndex == 1 ? 0 : 1;
            var winner = line[winnerIndex];
            var loser = line[loserIndex];
            var winnerRank = players[winner];
            var loserRank = players[loser];
            var winrate = ExpectedWinrate(winnerRank, loserRank);
            var ratingChange = RatingChange(winrate);
            var winnerNewRank = winnerRank + ratingChange;
            var loserNewRank = loserRank - ratingChange;
            players[winner] = winnerNewRank;
            players[loser] = loserNewRank;
        }

        var values = players.Values;
        var min = (int)Math.Floor(values.Min());
        var max = (int)Math.Floor(values.Max());
        var result = max - min;

        return new PuzzleResult(result, "28dd2c7955ce926456240b2ff0100bde");
    }
    
    public static double ExpectedWinrate(double a, double b) 
        => 1 / (1 + Math.Pow(10, (b - a) / 400));

    public static double RatingChange(double expectedWinrate) 
        => 20 * (1 - expectedWinrate);
}