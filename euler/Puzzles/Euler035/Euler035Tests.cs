﻿using NUnit.Framework;

namespace Euler.Puzzles.Euler035;

public class Euler035Tests
{
    [Test]
    public void Rotations()
    {
        var result = Euler035.GetRotations(12345).ToArray();

        Assert.That(result.Length, Is.EqualTo(5));
        Assert.That(result[0], Is.EqualTo(12345));
        Assert.That(result[1], Is.EqualTo(23451));
        Assert.That(result[2], Is.EqualTo(34512));
        Assert.That(result[3], Is.EqualTo(45123));
        Assert.That(result[4], Is.EqualTo(51234));
    }
}