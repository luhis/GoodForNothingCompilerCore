using System.Collections.Generic;
using System;
    using System.Linq;
    using GoodForNothingCompilerCore.Ast;

namespace GoodForNothingCompilerCore
{
    using System.Text;

    public class Parser
    {
        private readonly IEnumerable<object> _tokens;
        private int _index;

        public Parser(IEnumerable<object> tokens)
        {
            this._tokens = tokens;
            _index = 0;
        }

        public Stmt Result { get; private set; }

        public void Parse()
        {
            Result = ParseNextStmt();

            if (_index != _tokens.Count())
                throw new Exception("expected EOF");
        }

        private Stmt ParseNextStmt()
        {
            Stmt result = null;

            if (_index == _tokens.Count())
            {
                throw new Exception("expected statement, got EOF");
            }

            if (_tokens.ElementAt(_index).Equals("print"))
            {
                result = ParsePrint();
            }
            //else if (_tokens[_index].Equals("var"))
            //{
            //    result = ParseVar();
            //}
            //else if (_tokens[_index].Equals("read_int"))
            //{
            //    result = ParseReadInt();
            //}
            //else if (_tokens[_index].Equals("read_string"))
            //{
            //    result = ParseReadString();
            //}
            //else if (_tokens[_index].Equals("for"))
            //{
            //    result = ParseForLoop();
            //}
            else if (_tokens.ElementAt(_index) is string) // variable assignment
            {
                result = ParseAssignment();
            }
            else
            {
                throw new Exception($"parse error at token {_index}: {_tokens.ElementAt(_index)}");
            }

            _index++;


            if (_index >= _tokens.Count())    // no more to parse
                return result;

            //if (_tokens[_index].Equals("end")) // end of for-loop NOTE: Can this be moved closer to the "for" part above?
            //    return result;

            return new Sequence(result, ParseNextStmt());
        }

        private Stmt ParsePrint()
        {
            _index++;
            return new Print(ParseExpr());
        }

        private Expr ParseExpr()
        {
            if (_index == _tokens.Count()) throw new Exception("expected expression, got EOF");

            var nexttoken = _tokens.ElementAt(_index + 1);

            //check if this is a arithmetic-expr or simple expr
            if (
                (nexttoken is string && (string)nexttoken == "to") ||   // loop
                (nexttoken is string && (string)nexttoken == "do") ||   // loop
                (nexttoken is ArithToken && (ArithToken)nexttoken == ArithToken.Semi)
            )
            {
                return ParseSimpleExpr();
            }

            var binexpr = new ArithExpr();
            switch ((ArithToken)nexttoken)
            {
                case ArithToken.Add:
                    binexpr.Op = ArithOp.Add;
                    break;
                case ArithToken.Sub:
                    binexpr.Op = ArithOp.Sub;
                    break;
                case ArithToken.Mul:
                    binexpr.Op = ArithOp.Mul;
                    break;
                case ArithToken.Div:
                    binexpr.Op = ArithOp.Div;
                    break;
            }
            binexpr.Left = ParseSimpleExpr();
            _index++;
            binexpr.Right = ParseExpr();
            return binexpr;
        }

        private Expr ParseSimpleExpr()
        {
            if (_index == _tokens.Count())
                throw new Exception("expected expression, got EOF");

            if (_tokens.ElementAt(_index) is StringBuilder)
            {
                var value = _tokens.ElementAt(_index++).ToString();
                var stringLiteral = new StringLiteral(value);
                return stringLiteral;
            }

            if (_tokens.ElementAt(_index) is int)
            {
                var intValue = (int)_tokens.ElementAt(_index++);
                var intLiteral = new IntLiteral(intValue);
                return intLiteral;
            }

            if (_tokens.ElementAt(_index) is string)
            {
                var ident = (string)_tokens.ElementAt(_index++);
                var var = new Variable(ident);
                return var;
            }

            throw new Exception("expected string literal, int literal, or variable");
        }

        private Stmt ParseAssignment()
        {
            var ident = (string)_tokens.ElementAt(_index++);

            if (_index == _tokens.Count() ||
                (ArithToken)_tokens.ElementAt(_index) != ArithToken.Equal)
                throw new Exception("expected '='");

            _index++;

            return new Assign(ident, ParseExpr());
        }
    }
}