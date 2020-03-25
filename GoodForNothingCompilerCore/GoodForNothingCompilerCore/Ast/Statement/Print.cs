namespace GoodForNothingCompilerCore.Ast.Statement
{
    using GoodForNothingCompilerCore.Ast.Expression;

    public class Print : Stmt
    {
        public Print(Expr expr)
        {
            this.Expr = expr;
        }

        public Expr Expr { get; }
    }
}