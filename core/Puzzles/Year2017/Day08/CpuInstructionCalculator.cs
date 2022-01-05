using System.Collections.Generic;
using System.Linq;
using Core.Common.Strings;

namespace Core.Puzzles.Year2017.Day08;

public class CpuInstructionCalculator
{
    private readonly Dictionary<string, int> _registers;
    public int LargestValueAtEnd { get; }
    public int LargestValueEver { get; }

    public CpuInstructionCalculator(string input)
    {
        _registers = new Dictionary<string, int>();
        var instructions = PuzzleInputReader.ReadLines(input);
            
        foreach (var instruction in instructions)
        {
            var parts = instruction.Split(' ');
            var targetRegister = parts[0];
            var change = parts[1] == "inc" ? 1 : -1;
            var value = int.Parse(parts[2]);
            var readRegister = parts[4];
            var condition = parts[5];
            var compareValue = int.Parse(parts[6]);
                
            var currentValue = ReadValue(targetRegister);
            var targetValue = currentValue + change * value;
                
            if(IsConditionTrue(ReadValue(readRegister), condition, compareValue))
                _registers[targetRegister] = targetValue;

            var largestValue = _registers.Values.Max();
            if (largestValue > LargestValueEver)
                LargestValueEver = largestValue;
        }

        LargestValueAtEnd = _registers.Values.Max();
    }

    private int ReadValue(string key)
    {
        if (!_registers.ContainsKey(key))
            _registers.Add(key, 0);

        return _registers[key];
    }

    private bool IsConditionTrue(int a, string condition, int b)
    {
        if (condition == ">")
            return a > b;
        if (condition == "<")
            return a < b;
        if (condition == ">=")
            return a >= b;
        if (condition == "<=")
            return a <= b;
        if (condition == "==")
            return a == b;
        if (condition == "!=")
            return a != b;
        return false;
    }
}