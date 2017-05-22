using FluentAssertions;
using NUnit.Framework;

namespace Octobot.Tests
{
    [TestFixture]
    public class SampleTest
    {
        [Test]
        public void Test()
        {
            1.Should().Be(1);
        }

    }
}
