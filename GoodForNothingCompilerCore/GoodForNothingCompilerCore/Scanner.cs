using System.IO;
using System.Collections.Generic;

namespace GoodForNothingCompilerCore
{
    using System;
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
            while (!input.IsEmpty())
            {
                var ch = input.PeekCh();

                if (char.IsWhiteSpace(ch))
                {
                    // eat the current char and skip ahead!
                    input.Read();
                }
                else if (IsIdentifier(ch))
                {
                    // keyword or identifier
                    ScanIdent(input);
                }
                else if (ch == '"')
                {
                    // string literal
                    ScanString(input);
                }
                else if (char.IsDigit(ch))
                {
                    // numeric literal
                    ScanNumber(input, ch);
                }
                else if (ch == ';')
                {
                    // end of statement
                    ScanSemi(input);
                }
                else
                {
                    // arithmetic tokens such as + - / * =
                    ScanArith(input, ch);
                }
            }
        }

        private static bool IsIdentifier(char ch) => char.IsLetter(ch) || ch == '_';

        private void ScanIdent(TextReader input)
        {
            var accum = new StringBuilder();
            var ch = input.PeekCh();

            while (!input.IsEmpty() && IsIdentifier(ch))
            {
                accum.Append(ch);
                input.ReadCh();

                ch = input.PeekCh();
            }

            Tokens.Add(accum.ToString());
        }

        private void ScanString(TextReader input)
        {
            char ch;
            var accum = new StringBuilder();

            input.Read(); // skip the '"'

            if (input.IsEmpty())
            {
                throw new Exception("unterminated string literal");
            }

            while ((ch = input.PeekCh()) != '"')
            {
                accum.Append(ch);
                input.Read();

                if (input.Peek() == -1)
                {
                    throw new Exception("unterminated string literal");
                }
            }

            // skip the terminating "
            input.Read();
            Tokens.Add(accum);
        }

        private void ScanNumber(TextReader input, char ch)
        {
            var accum = new StringBuilder();

            while (!input.IsEmpty() && char.IsDigit(ch))
            {
                accum.Append(ch);
                input.Read();

                ch = input.PeekCh();
            }

            Tokens.Add(int.Parse(accum.ToString()));
        }

        private void ScanSemi(TextReader input)
        {
            input.Read();
            Tokens.Add(ArithToken.Semi);
        }

        private void ScanArith(TextReader input, char ch)
        {
            switch (ch)
            {
                case '+':
                    input.Read();
                    Tokens.Add(ArithToken.Add);
                    break;
                case '-':
                    input.Read();
                    Tokens.Add(ArithToken.Sub);
                    break;
                case '*':
                    input.Read();
                    Tokens.Add(ArithToken.Mul);
                    break;
                case '/':
                    input.Read();
                    Tokens.Add(ArithToken.Div);
                    break;
                case '=':
                    input.Read();
                    Tokens.Add(ArithToken.Equal);
                    break;
                default:
                    throw new Exception("Scanner encountered unrecognized character '" + ch + "'");
            }
        }
    }
}