using System.IO;
using System.Collections.Generic;
using System;
using System.Text;

namespace GoodForNothingCompilerCore
{
    public class Scanner
    {
        private readonly List<object> _tokens;

        public Scanner(TextReader input)
        {
            _tokens = new List<object>();
            Scan(input);
        }

        public IEnumerable<object> Tokens => this._tokens;

        private void Scan(TextReader input)
        {
            while (!input.IsEmpty())
            {
                var ch = input.ReadCh();

                if (char.IsWhiteSpace(ch))
                {
                }
                else if (IsIdentifier(ch))
                {
                    // keyword or identifier
                    ScanIdent(input, ch);
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
                    this.ScanArith(ch);
                }
            }
        }

        private static bool IsIdentifier(char ch) => char.IsLetter(ch) || ch == '_';

        private void ScanIdent(TextReader input, char ch)
        {
            var accum = new StringBuilder(ch.ToString());

            while (!input.IsEmpty() && IsIdentifier(input.PeekCh()))
            {
                ch = input.ReadCh();
                accum.Append(ch);
            }

            this._tokens.Add(accum.ToString());
        }

        private void ScanString(TextReader input)
        {
            if (input.IsEmpty())
            {
                throw new Exception("unterminated string literal");
            }

            char ch;
            var accum = new StringBuilder();
            while ((ch = input.PeekCh()) != '"')
            {
                accum.Append(ch);
                input.Read();

                if (input.IsEmpty())
                {
                    throw new Exception("unterminated string literal");
                }
            }

            // skip the terminating "
            input.Read();
            _tokens.Add(accum);
        }

        private void ScanNumber(TextReader input, char ch)
        {
            var accum = new StringBuilder();
            accum.Append(ch);

            while (!input.IsEmpty() && char.IsDigit(input.PeekCh()))
            {
                ch = input.ReadCh();
                accum.Append(ch);
            }

            _tokens.Add(int.Parse(accum.ToString()));
        }

        private void ScanSemi(TextReader input)
        {
            _tokens.Add(ArithToken.Semi);
        }

        private void ScanArith(char ch)
        {
            switch (ch)
            {
                case '+':
                    _tokens.Add(ArithToken.Add);
                    break;
                case '-':
                    _tokens.Add(ArithToken.Sub);
                    break;
                case '*':
                    _tokens.Add(ArithToken.Mul);
                    break;
                case '/':
                    _tokens.Add(ArithToken.Div);
                    break;
                case '=':
                    _tokens.Add(ArithToken.Equal);
                    break;
                default:
                    throw new Exception($"Scanner encountered unrecognized character '{ch}'");
            }
        }
    }
}