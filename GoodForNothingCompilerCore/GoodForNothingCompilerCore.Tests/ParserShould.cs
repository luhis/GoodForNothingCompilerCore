namespace GoodForNothingCompilerCore.Tests
{
    using System;
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

        [Fact]
        public void Parse2()
        {
            var parser = new Parser(ExampleTokenStreams.Addition);
            parser.Parse();
            parser.Result.Should().NotBeNull();
            //todo, why does this pass?
            parser.Result.Should().BeEquivalentTo(new Print(new StringLiteral("that's it folks!")));
        }

        [Fact]
        public void FailOnEmpty()
        {
            var parser = new Parser(new object[] { });
            Action a = () => parser.Parse();
            a.Should().Throw<Exception>().WithMessage("expected statement, got EOF");
        }
    }
}