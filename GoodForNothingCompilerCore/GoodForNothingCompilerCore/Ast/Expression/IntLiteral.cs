namespace GoodForNothingCompilerCore.Ast.Expression
{
    using System;

    public class IntLiteral : Expr
    {
        public IntLiteral(int value)
        {
            this.Value = value;
        }

        public int Value { get; }
        public override Type GetType()
        {
            return typeof(int);
        }
    }
}