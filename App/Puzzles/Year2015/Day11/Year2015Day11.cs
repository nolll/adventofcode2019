﻿using App.Platform;

namespace App.Puzzles.Year2015.Day11
{
    public class Year2015Day11: PuzzleDay
    {
        private CorporatePasswordValidator _validator;
        private string _firstPassword;

        private CorporatePasswordValidator Validator => _validator ??= new CorporatePasswordValidator();
        private string FirstPassword => _firstPassword ??= Validator.FindNextPassword(Input);

        public override PuzzleResult RunPart1()
        {
            return new PuzzleResult(FirstPassword, "hxbxxyzz");
        }

        public override PuzzleResult RunPart2()
        {
            var pwd2 = Validator.FindNextPassword(FirstPassword);
            return new PuzzleResult(pwd2, "hxcaabcc");
        }

        private const string Input = "hxbxwxba";
    }
}