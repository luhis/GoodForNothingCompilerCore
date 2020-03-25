namespace GoodForNothingCompilerCore.Ast
{
    using System;

    public class StringLiteral : Expr
    {
        public StringLiteral(string value)
        {
            this.Value = value;
        }

        public string Value { get; }
        public override Type GetType()
        {
            return typeof(string);
        }
    }
}