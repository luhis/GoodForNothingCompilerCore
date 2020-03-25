namespace GoodForNothingCompilerCore.Ast.Statement
{
    using GoodForNothingCompilerCore.Ast.Expression;

    public class DeclareVar : Stmt
    {
        public DeclareVar(string ident, Expr expr)
        {
            this.Expr = expr;
            this.Ident = ident;
        }

        public Expr Expr { get; }
        public string Ident { get; }
    }
}