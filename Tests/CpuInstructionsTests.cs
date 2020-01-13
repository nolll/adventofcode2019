using Core.CpuInstructions;
using NUnit.Framework;

namespace Tests
{
    public class CpuInstructionsTests
    {
        [Test]
        public void GetLargestValue()
        {
            const string input = @"
b inc 5 if a > 1
a inc 1 if b < 5
c dec -10 if a >= 1
c inc -20 if c == 10";

            var calculator = new CpuInstructionCalculator(input);

            Assert.That(calculator.LargestValue, Is.EqualTo(1));
        }
    }
}