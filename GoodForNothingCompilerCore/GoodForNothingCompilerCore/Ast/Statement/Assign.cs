namespace GoodForNothingCompilerCore.Ast.Statement
{
    using GoodForNothingCompilerCore.Ast.Expression;

    public class Assign : Stmt
    {
        public Assign(string ident, Expr expr)
        {
            this.Expr = expr;
            this.Ident = ident;
        }

        public Expr Expr { get; }
        public string Ident { get; }
    }
}