namespace GoodForNothingCompilerCore
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Text;
    using GoodForNothingCompilerCore.Ast.Expression;
    using GoodForNothingCompilerCore.Ast.Statement;

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
            if (_index == _tokens.Count())
            {
                throw new Exception("expected statement, got EOF");
            }

            Stmt result;
            var currentElement = _tokens.ElementAt(_index);
            if (currentElement.Equals("print"))
            {
                result = ParsePrint();
            }
            else if (currentElement.Equals("var"))
            {
                result = ParseVar();
            }
            else if (currentElement.Equals("read_int"))
            {
                result = ParseReadInt();
            }
            else if (currentElement.Equals("read_string"))
            {
                result = ParseReadString();
            }
            else if (currentElement.Equals("for"))
            {
                result = ParseForLoop();
            }
            else if (currentElement is string) // variable assignment
            {
                result = ParseAssignment();
            }
            else
            {
                throw new Exception($"parse error at token {_index}: {currentElement}");
            }

            _index++;


            if (_index >= _tokens.Count())    // no more to parse
                return result;

            if (_tokens.ElementAt(_index).Equals("end")) // end of for-loop NOTE: Can this be moved closer to the "for" part above?
                return result;

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

            var nextToken = _tokens.ElementAt(_index + 1);

            //check if this is a arithmetic-expr or simple expr
            if (
                (nextToken is string token && token == "to") ||   // loop
                (nextToken is string s && s == "do") ||   // loop
                (nextToken is ArithToken arithToken && arithToken == ArithToken.Semi)
            )
            {
                return ParseSimpleExpr();
            }

            var binexpr = new ArithExpr();
            switch ((ArithToken)nextToken)
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

        private Stmt ParseVar()
        {
            _index++;

            if (_index >= _tokens.Count() || !(_tokens.ElementAt(_index) is string))
                throw new Exception("expected variable name after 'var'");

            var ident = (string)_tokens.ElementAt(_index);

            _index++;

            if (_index == _tokens.Count() ||
                (ArithToken)_tokens.ElementAt(_index) != ArithToken.Equal)
                throw new Exception($"expected = after 'var {ident}'");

            _index++;

            var expr = ParseExpr();
            return new DeclareVar(ident, expr);
        }
         
        private Stmt ParseReadInt()
        {
            _index++;

            if (_index >= _tokens.Count() || !(_tokens.ElementAt(_index) is string))
                throw new Exception("expected variable name after 'read_int'");

            return new ReadInt((string)_tokens.ElementAt(_index++));
        }
        private Stmt ParseReadString()
        {
            _index++;

            if (_index >= _tokens.Count() || !(_tokens.ElementAt(_index) is string))
                throw new Exception("expected variable name after 'read_string'");

            return new ReadString((string)_tokens.ElementAt(_index++));
        }
        private Stmt ParseForLoop()
        {
            _index++;
            var forLoop = new ForLoop();

            if (_index >= _tokens.Count() || !(_tokens.ElementAt(_index) is string))
                throw new Exception("expected identifier after 'for'");

            forLoop.Ident = (string)_tokens.ElementAt(_index);

            _index++;

            if (_index == _tokens.Count() ||
                (ArithToken)_tokens.ElementAt(_index) != ArithToken.Equal)
                throw new Exception("for missing '='");

            _index++;

            forLoop.From = ParseExpr();

            if (_index == _tokens.Count() ||
                !_tokens.ElementAt(_index).Equals("to"))
                throw new Exception("expected 'to' after for");

            _index++;

            forLoop.To = ParseExpr();   //TODO: Change compiler - loop ends one step early

            if (_index == _tokens.Count() ||
                !_tokens.ElementAt(_index).Equals("do"))
                throw new Exception("expected 'do' after from expression in for loop");

            _index++;

            forLoop.Body = ParseNextStmt();

            if (_index == _tokens.Count() ||
                !_tokens.ElementAt(_index).Equals("end"))
                throw new Exception("unterminated 'for' loop body");

            _index++;

            return forLoop;
        }
    }
}