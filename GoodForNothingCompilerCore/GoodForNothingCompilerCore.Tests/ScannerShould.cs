namespace GoodForNothingCompilerCore.Tests
{
    using System.IO;
    using FluentAssertions;
    using Xunit;

    public class ScannerShould
    {
        [Fact]
        public void ScanAllWhiteSpace()
        {
            var stream = GenerateStreamReaderFromString("   ");
            var scanner = new Scanner(stream);
            scanner.Tokens.Should().BeEmpty();
        }

        [Fact]
        public void ScanAllAnIdentifier()
        {
            var stream = GenerateStreamReaderFromString("_muhCode");
            var scanner = new Scanner(stream);
            scanner.Tokens.Should().BeEquivalentTo(new[] {"_muhCode"});
        }

        private static StreamReader GenerateStreamReaderFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return new StreamReader(stream);
        }
    }
}
