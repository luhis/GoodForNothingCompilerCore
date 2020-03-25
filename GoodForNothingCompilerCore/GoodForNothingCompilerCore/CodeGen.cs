using System.Collections.Generic;

namespace GoodForNothingCompilerCore
{
    using System.Reflection.Emit;
    using GoodForNothingCompilerCore.Ast;
    using GoodForNothingCompilerCore.Ast.Statement;

    public class CodeGen
    {
        private readonly Stmt result;
        private readonly string moduleName;
        public static IReadOnlyDictionary<string, LocalBuilder> SymbolTable;

        public CodeGen(Stmt result, string moduleName)
        {
            this.result = result;
            this.moduleName = moduleName;
        }

        public void Compile()
        {
            throw new System.NotImplementedException();
        }
    }
}