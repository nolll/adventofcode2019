﻿using Pzl.Common;

namespace Pzl.Euler;

public abstract class EulerPuzzle : OnePartPuzzle
{
    public override string Id { get; }
    public override string SortId { get; }
    public override string Title { get; }
    public override string ListTitle { get; }
    protected override string CollectionTag => "euler";

    protected EulerPuzzle()
    {
        var id = EulerPuzzleParser.GetPuzzleId(GetType()).ToString();
        var paddedId = id.PadLeft(3, '0');
        Id = id;
        SortId = paddedId;
        Title = $"Project Euler {id}";
        ListTitle = $"Euler {paddedId}";
    }

    protected override IEnumerable<string> CustomTags
    {
        get
        {
            yield return Id;
        }
    }
}