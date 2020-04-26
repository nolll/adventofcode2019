using Core.AdventCoins;
using NUnit.Framework;

namespace Tests
{
    public class TuringDiagnosticsTests
    {
        [Test]
        public void ChecksumIsCorrect()
        {
            const string input = @"
Begin in state A.
Perform a diagnostic checksum after 6 steps.

In state A:
  If the current value is 0:
    - Write the value 1.
    - Move one slot to the right.
    - Continue with state B.
  If the current value is 1:
    - Write the value 0.
    - Move one slot to the left.
    - Continue with state B.

In state B:
  If the current value is 0:
    - Write the value 1.
    - Move one slot to the left.
    - Continue with state A.
  If the current value is 1:
    - Write the value 1.
    - Move one slot to the right.
    - Continue with state A.";

            var turingMachine = new TuringMachine(input);
            turingMachine.Run();

            Assert.That(turingMachine.Checksum, Is.EqualTo(3));
        }
    }

    public class AdventCoinTests
    {
        [TestCase("abcdef", 609043)]
        [TestCase("pqrstuv", 1048970)]
        public void CoinMined(string secretKey, int expected)
        {
            var miner = new AdventCoinMiner();
            var coin = miner.Mine(secretKey, 5);

            Assert.That(coin, Is.EqualTo(expected));
        }
    }
}