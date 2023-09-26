﻿using Common.Puzzles;

namespace Aoc.Puzzles.Aoc2019.Aoc201922;

public class Aoc201922 : AocPuzzle
{
    public override string Name => "Slam Shuffle";
    public override string Comment => "Learn more math";
    public override bool NeedsRewrite => true;

    protected override PuzzleResult RunPart1()
    {
        var shuffler1 = new CardShuffler();
        var deck = shuffler1.Shuffle(10_007, InputFile);
        var positionOfCard2019 = deck.IndexOf(2019);
        return new PuzzleResult(positionOfCard2019, 1822);
    }

    protected override PuzzleResult RunPart2()
    {
        var shuffler2 = new CardShuffler();
        var cardAtPosition2020 = shuffler2.ShuffleBig(InputFile);
        return new PuzzleResult((long)cardAtPosition2020, 49_174_686_993_380);
    }
}