using System.Collections.Generic;

namespace GoodForNothingCompilerCore
{
    public class Parser
    {
        private IEnumerable<object> tokens;

        public Parser(IEnumerable<object> tokens)
        {
            this.tokens = tokens;
        }

        public IEnumerable<object> Result { get; set; }

        public void Parse()
        {
            throw new System.NotImplementedException();
        }
    }
}