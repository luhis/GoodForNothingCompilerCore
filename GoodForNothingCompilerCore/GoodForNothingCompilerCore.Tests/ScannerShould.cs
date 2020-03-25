namespace GoodForNothingCompilerCore.Tests
{
    using System.IO;
    using System.Text;
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
        public void ScanAnIdentifier()
        {
            var stream = GenerateStreamReaderFromString("_muhCode");
            var scanner = new Scanner(stream);
            scanner.Tokens.Should().BeEquivalentTo(new[] {"_muhCode"});
        }

        [Fact]
        public void ScanAStringLiteral()
        {
            var stream = GenerateStreamReaderFromString("  \"test\"");
            var scanner = new Scanner(stream);
            scanner.Tokens.Should().BeEquivalentTo( new StringBuilder("test"));
        }

        [Fact]
        public void ScanANumberLiteral()
        {
            var stream = GenerateStreamReaderFromString("  123 ");
            var scanner = new Scanner(stream);
            scanner.Tokens.Should().BeEquivalentTo(123);
        }

        [Fact]
        public void ScanASemi()
        {
            var stream = GenerateStreamReaderFromString("  ; ");
            var scanner = new Scanner(stream);
            scanner.Tokens.Should().BeEquivalentTo(ArithToken.Semi);
        }

        [Fact]
        public void ScanAnEqual()
        {
            var stream = GenerateStreamReaderFromString("  = ");
            var scanner = new Scanner(stream);
            scanner.Tokens.Should().BeEquivalentTo(ArithToken.Equal);
        }

        [Fact]
        public void ScanACombo()
        {
            var stream = GenerateStreamReaderFromString("var x = 2 * 3;");
            var scanner = new Scanner(stream);
            scanner.Tokens.Should().BeEquivalentTo(new object[] {"var", "x", ArithToken.Equal, 2, ArithToken.Mul, 3, ArithToken.Semi});
        }

        [Fact]
        public void ScanASimpleApp()
        {
            var stream = GenerateStreamReaderFromString("print \"that's it folks!\";");
            var scanner = new Scanner(stream);
            scanner.Tokens.Should().BeEquivalentTo(ExampleTokenStreams.PrintThatsItFolks);
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
