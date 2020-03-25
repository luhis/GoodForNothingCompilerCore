namespace GoodForNothingCompilerCore.Ast.Expression
{
    using System;

    public class Variable : Expr
    {
        public Variable(string ident)
        {
            this.Ident = ident;
        }

        public string Ident { get; }
        public override Type GetType()
        {
            if (!CodeGen.SymbolTable.ContainsKey(this.Ident)) throw new Exception($"undeclared variable '{this.Ident }'");

            var locb = CodeGen.SymbolTable[this.Ident];
            return locb.LocalType;
        }
    }
}