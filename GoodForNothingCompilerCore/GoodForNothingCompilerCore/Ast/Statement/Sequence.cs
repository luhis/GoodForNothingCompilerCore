namespace GoodForNothingCompilerCore.Ast.Statement
{
    public class Sequence : Stmt
    {
        public Sequence(Stmt first, Stmt second)
        {
            this.First = first;
            this.Second = second;
        }

        public Stmt First { get; }
        public Stmt Second { get; }
    }
}