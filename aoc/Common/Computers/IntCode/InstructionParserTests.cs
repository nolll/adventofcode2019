using System.Collections.Generic;
using Aoc.Common.Computers.IntCode.Instructions;
using NUnit.Framework;

namespace Aoc.Common.Computers.IntCode;

public class InstructionParserTests
{
    [Test]
    public void ReturnsNull()
    {
        var result = InstructionParser.Parse(new List<long>(), 0, 0);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void ReturnsAdditionInstruction()
    {
        const string input = "1,0,0,0";
        var memory = MemoryParser.Parse(input);
        var result = InstructionParser.Parse(memory, 0, 0);

        Assert.That(result, Is.TypeOf<AdditionInstruction>());
        Assert.That(result.Parameters.Count, Is.EqualTo(3));
        Assert.That(result.Parameters[0].Type, Is.EqualTo(ParameterType.Position));
        Assert.That(result.Parameters[1].Type, Is.EqualTo(ParameterType.Position));
        Assert.That(result.Parameters[2].Type, Is.EqualTo(ParameterType.Position));
    }

    [Test]
    public void ReturnsMultiplicationInstruction()
    {
        const string input = "2,0,0,0";
        var memory = MemoryParser.Parse(input);
        var result = InstructionParser.Parse(memory, 0, 0);

        Assert.That(result, Is.TypeOf<MultiplicationInstruction>());
        Assert.That(result.Parameters.Count, Is.EqualTo(3));
        Assert.That(result.Parameters[0].Type, Is.EqualTo(ParameterType.Position));
        Assert.That(result.Parameters[1].Type, Is.EqualTo(ParameterType.Position));
        Assert.That(result.Parameters[2].Type, Is.EqualTo(ParameterType.Position));
    }

    [Test]
    public void ReturnsInputInstruction()
    {
        const string input = "3,0,0,0";
        var memory = MemoryParser.Parse(input);
        var result = InstructionParser.Parse(memory, 0, 0);

        Assert.That(result.Type, Is.EqualTo(InstructionType.Input));
        Assert.That(result.Parameters.Count, Is.EqualTo(1));
        Assert.That(result.Parameters[0].Type, Is.EqualTo(ParameterType.Position));
    }

    [Test]
    public void ReturnsOutputInstruction()
    {
        const string input = "4,0,0,0";
        var memory = MemoryParser.Parse(input);
        var result = InstructionParser.Parse(memory, 0, 0);

        Assert.That(result, Is.TypeOf<OutputInstruction>());
        Assert.That(result.Parameters.Count, Is.EqualTo(1));
        Assert.That(result.Parameters[0].Type, Is.EqualTo(ParameterType.Position));
    }

    [Test]
    public void ReturnsHaltInstruction()
    {
        const string input = "99,0,0,0";
        var memory = MemoryParser.Parse(input);
        var result = InstructionParser.Parse(memory, 0, 0);

        Assert.That(result, Is.TypeOf<HaltInstruction>());
        Assert.That(result.Parameters.Count, Is.EqualTo(0));
    }

    [Test]
    public void ReturnsAdditionInstructionWithImmediateParameters()
    {
        const string input = "01101,0,0,0";
        var memory = MemoryParser.Parse(input);
        var result = InstructionParser.Parse(memory, 0, 0);

        Assert.That(result.Type, Is.EqualTo(InstructionType.Addition));
        Assert.That(result.Parameters.Count, Is.EqualTo(3));
        Assert.That(result.Parameters[0].Type, Is.EqualTo(ParameterType.Immediate));
        Assert.That(result.Parameters[1].Type, Is.EqualTo(ParameterType.Immediate));
        Assert.That(result.Parameters[2].Type, Is.EqualTo(ParameterType.Position));
    }
}