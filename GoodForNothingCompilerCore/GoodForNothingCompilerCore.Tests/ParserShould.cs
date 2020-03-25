namespace GoodForNothingCompilerCore.Tests
{
    using FluentAssertions;
    using GoodForNothingCompilerCore.Ast.Expression;
    using GoodForNothingCompilerCore.Ast.Statement;
    using Xunit;

    public class ParserShould
    {
        [Fact]
        public void Parse()
        {
            var parser = new Parser(ExampleTokenStreams.PrintThatsItFolks);
            parser.Parse();
            parser.Result.Should().NotBeNull();
            parser.Result.Should().BeEquivalentTo(new Print(new StringLiteral("that's it folks!")));
        }
    }
}