﻿using System;

namespace Pzl.Common;

[AttributeUsage(AttributeTargets.Class)]
public class CommentAttribute : Attribute
{
    public string Comment { get; }

    public CommentAttribute(string comment)
    {
        Comment = comment;
    }
}

[AttributeUsage(AttributeTargets.Class)]
public class IsSlowAttribute : Attribute;

[AttributeUsage(AttributeTargets.Class)]
public class IsFunToOptimizeAttribute : Attribute;

[AttributeUsage(AttributeTargets.Class)]
public class NeedsRewriteAttribute : Attribute;