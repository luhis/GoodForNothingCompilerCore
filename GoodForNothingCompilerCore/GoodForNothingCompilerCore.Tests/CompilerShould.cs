namespace GoodForNothingCompilerCore.Tests
{
    using System.IO;
    using FluentAssertions;
    using Xunit;

    public class CompilerShould
    {
        [Theory]
        [InlineData("loop.gfn")]
        [InlineData("helloworld.gfn")]
        public void Compile(string fileName)
        {
            var app = File.OpenText($"../../../../CodeSamples/{fileName}");
            var scanner = new Scanner(app);
            var parser = new Parser(scanner.Tokens);
            parser.Parse();
            parser.Result.Should().NotBeNull();
        }
    }
}