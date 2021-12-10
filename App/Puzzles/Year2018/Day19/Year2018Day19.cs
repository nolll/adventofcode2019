﻿using App.Common.Computers.Operation;
using App.Platform;

namespace App.Puzzles.Year2018.Day19
{
    public class Year2018Day19 : Puzzle
    {
        public override PuzzleResult RunPart1()
        {
            var computer = new OpComputer();
            var value1 = computer.RunInstructionPointerProgram(FileInput, 0, true, false);
            return new PuzzleResult(value1, 1872);
        }

        public override PuzzleResult RunPart2()
        {
            var computer2 = new OpComputer();
            var value2 = computer2.RunInstructionPointerProgram(FileInput, 1, true, false);
            return new PuzzleResult(value2, 18_992_592);
        }
    }
}