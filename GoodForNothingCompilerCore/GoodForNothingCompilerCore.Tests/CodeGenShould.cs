namespace GoodForNothingCompilerCore.Tests
{
    using GoodForNothingCompilerCore.Ast.Expression;
    using GoodForNothingCompilerCore.Ast.Statement;
    using Xunit;

    public class CodeGenShould
    {
        [Fact]
        public void NotBlowUp()
        {
            new CodeGen(new Print(new StringLiteral("test")), "test");
        }
    }
}