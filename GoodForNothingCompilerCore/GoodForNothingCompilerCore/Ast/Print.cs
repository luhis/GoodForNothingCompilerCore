namespace GoodForNothingCompilerCore.Ast
{
    public class Print : Stmt
    {
        public Print(Expr expr)
        {
            this.Expr = expr;
        }

        public Expr Expr { get; }
    }
}