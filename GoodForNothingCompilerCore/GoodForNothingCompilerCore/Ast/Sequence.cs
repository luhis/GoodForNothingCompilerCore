namespace GoodForNothingCompilerCore.Ast
{
    public class Sequence : Stmt
    {
        public Sequence(Stmt first, Stmt second)
        {
            First = first;
            Second = second;
        }

        public Stmt First { get; }
        public Stmt Second { get; }
    }
}