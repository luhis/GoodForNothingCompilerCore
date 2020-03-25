namespace GoodForNothingCompilerCore.Ast.Statement
{
    public class ReadString : Stmt
    {
        public ReadString(string ident)
        {
            this.Ident = ident;
        }

        public string Ident { get; }
    }
}