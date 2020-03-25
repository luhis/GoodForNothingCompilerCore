namespace GoodForNothingCompilerCore.Ast.Expression
{
    using System;

    public abstract class Expr
    {
        // todo might not need this
        public new abstract Type GetType();
    }
}