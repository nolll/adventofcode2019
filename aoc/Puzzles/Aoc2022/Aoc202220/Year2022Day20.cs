﻿using System;
using System.Collections.Generic;
using System.Linq;
using Common.Lists;
using Common.Puzzles;
using Common.Strings;

namespace Aoc.Puzzles.Aoc2022.Aoc202220;

public class Year2022Day20 : AocPuzzle
{
    public override string Name => "Grove Positioning System";

    protected override PuzzleResult RunPart1()
    {
        var result = Run(InputFile, 1, 1);

        return new PuzzleResult(result, 18257);
    }

    protected override PuzzleResult RunPart2()
    {
        var result = Run(InputFile, 811_589_153, 10);

        return new PuzzleResult(result, 4_148_032_160_983);
    }

    public static long Run(string input, long multiplier, int iterationCount)
    {
        var numbers = PuzzleInputReader.ReadLines(input, false).Select(s => long.Parse(s) * multiplier).ToList();

        var list = new LinkedList<long>();
        var set = new Dictionary<long, LinkedListNode<long>>();

        var index = 0;
        foreach (var number in numbers)
        {
            var node = list.AddLast(number);
            set.Add(index, node);
            index++;
        }

        for (var iteration = 0; iteration < iterationCount; iteration++)
        {
            for (var i = 0; i < numbers.Count; i++)
            {
                var currentNode = set[i];
                var steps = currentNode.Value % (numbers.Count - 1);

                for (var j = 0; j < Math.Abs(steps); j++)
                {
                    var value = currentNode.Value;
                    var nextNode = steps > 0 ? currentNode.NextOrFirst() : currentNode.PreviousOrLast();
                    list.Remove(currentNode);
                    currentNode = steps > 0 ? list.AddAfter(nextNode, value) : list.AddBefore(nextNode, value);
                }

                set[i] = currentNode;
            }
        }

        var array = list.ToList();
        var offset = array.IndexOf(0);

        return new[] { 1, 2, 3 }.Select(o => array[(offset + o * 1000) % array.Count]).Sum();
    }
}