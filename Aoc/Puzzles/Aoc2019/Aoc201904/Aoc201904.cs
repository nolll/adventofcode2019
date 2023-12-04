﻿using Puzzles.Common.Puzzles;

namespace Puzzles.Aoc.Puzzles.Aoc2019.Aoc201904;

public class Aoc201904 : AocPuzzle
{
    public override string Name => "Secure Container";

    protected override PuzzleResult RunPart1()
    {
        var passwordBounds = Input.Split('-');
        var passwordLowerbound = int.Parse(passwordBounds[0]);
        var passwordUpperbound = int.Parse(passwordBounds[1]);

        var passwordFinder = new PasswordFinder();
        var passwords = passwordFinder.FindPart1(passwordLowerbound, passwordUpperbound);
        var passwordCount = passwords.Count();
        return new PuzzleResult(passwordCount, "130c5099df019116c1fa98e589523b7c");
    }

    protected override PuzzleResult RunPart2()
    {
        var passwordBounds = Input.Split('-');
        var passwordLowerbound = int.Parse(passwordBounds[0]);
        var passwordUpperbound = int.Parse(passwordBounds[1]);

        var passwordFinder = new PasswordFinder();
        var passwords = passwordFinder.FindPart2(passwordLowerbound, passwordUpperbound);
        var passwordCount = passwords.Count();
        return new PuzzleResult(passwordCount, "a91290a19800def81b170a8a45592c43");
    }

    private const string Input = "357253-892942";
}