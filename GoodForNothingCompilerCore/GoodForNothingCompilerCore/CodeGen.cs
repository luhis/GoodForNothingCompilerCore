using System.Collections.Generic;

namespace GoodForNothingCompilerCore
{
    public class CodeGen
    {
        private readonly IEnumerable<object> result;
        private readonly string moduleName;

        public CodeGen(IEnumerable<object> result, string moduleName)
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