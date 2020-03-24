namespace GoodForNothingCompilerCore.Tests
{
    using FluentAssertions;
    using Xunit;

    public class Test
    {
        [Fact]
        public void Test1()
        {
            true.Should().BeTrue();
        }
    }
}
