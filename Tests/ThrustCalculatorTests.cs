using System.Linq;
using Core.Thrust;
using NUnit.Framework;

namespace Tests
{
    public class BoostTest
    {
        //109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99 takes no input and produces a copy of itself as output.
        //1102,34915192,34915192,7,4,7,99,0 should output a 16-digit number.
        //104,1125899906842624,99 should output the large number in the middle.

        [Test]
        public void ReturnsInputMemoryAsOutputMemory()
        {

        }

        [Test]
        public void Outputs16DigitNumber()
        {

        }

        [Test]
        public void ReturnsLargeMiddleNumber()
        {

        }
    }

    public class ThrustCalculatorTests
    {
        [TestCase("3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0", 4, 3, 2, 1, 0, 43210)]
        [TestCase("3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0", 0, 1, 2, 3, 4, 54321)]
        [TestCase("3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0", 1, 0, 4, 3, 2, 65210)]
        public void ThrustCalculationsAreCorrect(string program, int phase0, int phase1, int phase2, int phase3, int phase4, int expectedThrust)
        {
            var sequence = new[] { phase0, phase1, phase2, phase3, phase4 };
            var calculator = new ThrustCalculator(program);
            var thrust = calculator.GetThrust(sequence);

            Assert.That(thrust, Is.EqualTo(expectedThrust));
        }
    }

    public class ThrustCalculator2Tests
    {
        [TestCase("3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5", 9, 8, 7, 6, 5, 139629729)]
        [TestCase("3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10", 9, 7, 8, 5, 6, 18216)]
        public void ThrustCalculationsAreCorrect(string program, int phase0, int phase1, int phase2, int phase3, int phase4, int expectedThrust)
        {
            var sequence = new[] { phase0, phase1, phase2, phase3, phase4 };
            var calculator = new ThrustCalculator(program);
            var thrust = calculator.GetThrust(sequence);

            Assert.That(thrust, Is.EqualTo(expectedThrust));
        }
    }

    public class SequenceGeneratorTests
    {
        [Test]
        public void GeneratesAllCombinations()
        {
            var generator = new SequenceGenerator();
            var sequences = generator.GetSequences(new[] {1, 2, 3}).ToList();

            Assert.That(sequences.Count, Is.EqualTo(6));
        }
    }
}