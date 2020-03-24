using System.IO;
using System.Collections.Generic;

namespace GoodForNothingCompilerCore
{
    using System.Text;

    public class Scanner
    {
        public Scanner(TextReader input)
        {
            Tokens = new List<object>();
            Scan(input);
        }

        public IList<object> Tokens { get; }

        private void Scan(TextReader input)
        {
            while (input.Peek() != -1)
            {
                var ch = (char)input.Peek();

                if (char.IsWhiteSpace(ch))
                {
                    // eat the current char and skip ahead!
                    input.Read();
                }
                else if (IsIdentifier(ch))
                {
                    // keyword or identifier
                    ScanIdent(input, ch);
                }
                //else if (ch == '"')
                //{
                //    // string literal
                //    ScanString(input);
                //}
                //else if (char.IsDigit(ch))
                //{
                //    // numeric literal
                //    ScanNumber(input, ch);
                //}
                //else if (ch == ';')
                //{
                //    // end of statement
                //    ScanSemi(input);
                //}
                //else
                //{
                //    // arithmetic tokens such as + - / * =
                //    ScanArith(input, ch);
                //}
            }
        }

        private static bool IsIdentifier(char ch) => char.IsLetter(ch) || ch == '_';

        private void ScanIdent(TextReader input, char ch)
        {
            var accum = new StringBuilder();

            while (IsIdentifier(ch))
            {
                accum.Append(ch);
                input.Read();

                if (input.Peek() == -1)
                {
                    break;
                }
                ch = (char)input.Peek();
            }

            Tokens.Add(accum.ToString());
        }
    }
}