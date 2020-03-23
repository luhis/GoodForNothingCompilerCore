using System.IO;
    using System.Collections.Generic;

namespace GoodForNothingCompilerCore
{

    public class Scanner
    {
        private readonly TextReader input;

        public Scanner(TextReader input)
        {
            this.input = input;
        }

        public IEnumerable<string> Tokens { get; }
    }
}