namespace GoodForNothingCompilerCore.Ast.Statement
{
    using GoodForNothingCompilerCore.Ast.Expression;

    public class ForLoop : Stmt
    {
        public Stmt Body { get; set; }
        public Expr From { get; set; }
        public string Ident { get; set; }
        public Expr To { get; set; }
    }
}