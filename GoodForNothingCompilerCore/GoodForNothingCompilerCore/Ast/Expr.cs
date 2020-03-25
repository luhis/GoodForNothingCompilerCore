namespace GoodForNothingCompilerCore.Ast
{
    using System;

    public abstract class Expr
    {
        // todo might not need this
        public new abstract Type GetType();
    }
}