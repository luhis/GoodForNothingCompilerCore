namespace GoodForNothingCompilerCore.Ast.Statement
{
    public class ReadInt : Stmt
    {
        public ReadInt(string ident)
        {
            this.Ident = ident;
        }

        public string Ident { get; }
    }
}