using Core.Duet;
using NUnit.Framework;

namespace Tests
{
    public class AssemblyDuetTests
    {
        [Test]
        public void CorrectFrequencyIsPlayed()
        {
            const string input = @"
set a 1
add a 2
mul a a
mod a 5
snd a
set a 0
rcv a
jgz a -1
set a 1
jgz a -2";
            var duet = new DuetFrequencies(input);
            var frequency = duet.FindFrequency();

            Assert.That(frequency, Is.EqualTo(4));
        }
    }
}