namespace GoodForNothingCompilerCore.Ast
{
    using System;

    public class ArithExpr : Expr
    {
        public Expr Left { get; set; }
        public ArithOp Op { get; set; }
        public Expr Right { get; set; }

        public override Type GetType()
        {
            return this.Left.GetType();
        }
    }
}