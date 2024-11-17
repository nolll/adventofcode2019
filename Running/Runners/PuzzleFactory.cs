﻿using System.Reflection;
using Pzl.Common;

namespace Pzl.Client.Running.Runners;

public static class PuzzleFactory
{
    public static Puzzle CreateInstance(PuzzleDefinition definition)
    {
        var ctor = definition.Type.GetConstructors().First();
        var args = GetArgs(definition, ctor);

        if (ctor.Invoke(args) is not Puzzle puzzle)
            throw new Exception($"Could not create puzzle: {definition.Type}");

        return puzzle;
    }

    private static object[] GetArgs(PuzzleDefinition definition, MethodBase ctor)
    {
        var parameters = ctor.GetParameters();
        var parameterCount = parameters.Length;
        var inputs = FileReader.ReadInputs(definition);
        return parameterCount switch
        {
            2 => [inputs, ReadAdditionalFile(definition)],
            1 => [inputs],
            _ => []
        };
    }

    private static string ReadAdditionalFile(PuzzleDefinition definition)
    {
        if (definition.CommonFile is not null)
            return FileReader.ReadCommon(definition.CommonFile);

        if (definition.LocalFile is not null)
            return FileReader.ReadLocal(definition.Type, definition.LocalFile);

        return "";
    }
}