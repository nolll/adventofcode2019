﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Core.Common.CoordinateSystems;
using Core.Common.CoordinateSystems.CoordinateSystem2D;
using Core.Common.Strings;

namespace Core.Puzzles.Year2016.Day22;

public class StorageGrid
{
    private readonly Regex _whiteSpaceRegex = new Regex("[ ]{2,}", RegexOptions.None);
    private readonly DynamicMatrix<StorageNode> _storage;

    public StorageGrid(string input)
    {
        _storage = ParseGrid(input);
    }

    public int GetViablePairCount()
    {
        return GetNodesThatCanMove().Count;
    }

    private IList<MatrixAddress> GetNodesThatCanMove()
    {
        var nodes = new List<MatrixAddress>();

        for (var ya = 0; ya < _storage.Height; ya++)
        {
            for (var xa = 0; xa < _storage.Width; xa++)
            {
                for (var yb = 0; yb < _storage.Height; yb++)
                {
                    for (var xb = 0; xb < _storage.Width; xb++)
                    {
                        if (xa != xb || ya != yb)
                        {
                            var nodeA = _storage.ReadValueAt(xa, ya);
                            var nodeB = _storage.ReadValueAt(xb, yb);
                            var nodeAHasData = nodeA.Used > 0;
                            var nodeACanFitOnNodeB = nodeA.Used <= nodeB.Avail;
                            if (nodeAHasData && nodeACanFitOnNodeB)
                            {
                                nodes.Add(new MatrixAddress(xa, ya));
                            }
                        }
                    }
                }
            }
        }

        return nodes;
    }

    public int MoveStorage()
    {
        var matrix = new DynamicMatrix<char>(_storage.Width, _storage.Height, '#');
        var nodesThatCanMove = GetNodesThatCanMove();
        foreach (var address in nodesThatCanMove)
        {
            matrix.MoveTo(address);
            matrix.WriteValue('.');
        }

        var startAddress = new MatrixAddress(0, 0);
        for (var y = 0; y < _storage.Height; y++)
        {
            for (var x = 0; x < _storage.Width; x++)
            {
                var node = _storage.ReadValueAt(x, y);
                if (node.Used == 0)
                    startAddress = new MatrixAddress(x, y);
            }
        }

        var topLeft = new MatrixAddress(0, 0);
        var topRight = new MatrixAddress(matrix.Width - 1, 0);
        var goal = new MatrixAddress(topRight.X - 1, topRight.Y);
        var distance1 = PathFinder.CachedStepCountTo(matrix, startAddress, goal);
        var distance2 = PathFinder.CachedStepCountTo(matrix, goal, topLeft);
        return distance1 + distance2 * 5 + 1;
    }

    private DynamicMatrix<StorageNode> ParseGrid(string input)
    {
        var rows = PuzzleInputReader.ReadLines(input);
        var dataRows = rows.Skip(2);
        var matrix = new DynamicMatrix<StorageNode>();

        foreach (var row in dataRows)
        {
            var parts = RemoveExtraSpaces(row).Split(' ');

            var nodeName = parts[0];
            var lastPartOfName = nodeName.Split('/').Last();
            var coordParts = lastPartOfName.Split('-');
            var x = int.Parse(coordParts[1].Replace("x", ""));
            var y = int.Parse(coordParts[2].Replace("y", ""));
            var size = int.Parse(parts[1].Replace("T", ""));
            var used = int.Parse(parts[2].Replace("T", ""));
            var avail = int.Parse(parts[3].Replace("T", ""));

            matrix.MoveTo(x, y);
            matrix.WriteValue(new StorageNode(size, used, avail));
        }

        return matrix;
    }

    private string RemoveExtraSpaces(string s)
    {
        return _whiteSpaceRegex.Replace(s, " ");
    }
}