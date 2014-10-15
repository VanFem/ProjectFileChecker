using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using RecursiveFileProcessor.Kendo.CodeFrame;

namespace RecursiveFileProcessor.Kendo.Parser
{
    public class CodeParser
    {
        public const string AcceptableCharacters = "=+-/*<>![]@";
        public MethodBody Code { get; set; }

        public int _parsingIndex;

        public bool _inSingleStatementLambda;

        public string _parsingText;

        public CodeParser()
        {
            Code = new MethodBody(false);
        }

        public void ParseCode(string text)
        {
            Code = new MethodBody(false);
            if (string.IsNullOrEmpty(text)) return;
            _parsingText = text;
            _parsingIndex = 0;
            ReadMethodBody(Code, false);
        }

        public Statement ParseSingleStatement(string text)
        {
            Code = new MethodBody(false);
            if (string.IsNullOrEmpty(text)) return new Statement();
            _parsingText = text;
            _parsingIndex = 0;
            Code.Statements.Add(ReadStatement(true));
            return Code.Statements[0];
        }

        public void ReadMethodBody(MethodBody mb, bool inSingleStatement)
        {
            if (inSingleStatement)
            {
                mb.Statements.Add(ReadStatement(inSingleStatement));
                return;
            }
            while ((_parsingIndex < _parsingText.Length)
                   && (_parsingText.Substring(_parsingIndex).Trim() != string.Empty)
                   && (NextParsedChar() != '}'))
            {
                mb.Statements.Add(ReadStatement(inSingleStatement));
                SkipWhitespace();
            }

            if (NextParsedChar() == '}') ParseNextChar();
        }

        public bool IsAtEndOfFile()
        {
            return _parsingIndex == _parsingText.Length;
        }

        public void SkipWhitespace()
        {
            while (_parsingIndex < _parsingText.Length && char.IsWhiteSpace(NextParsedChar()))
            {
                ParseNextChar();
            }
        }

        public void ReadCode()
        {
            while (_parsingIndex < _parsingText.Length)
            {
                Code.Statements.Add(ReadStatement(false));
            }
        }

        public Statement ReadStatement(bool inSingleStatement)
        {
            var st = new Statement();
            st.Obj = ReadObjectName(st, inSingleStatement);
            return st;
        }

        private char ParseNextChar()
        {
            if (_parsingIndex < _parsingText.Length)
                return _parsingText[_parsingIndex++];
            throw new Exception("Unexpected end of code");
        }

        private char CurrentChar()
        {
            if (_parsingIndex == 0) return _parsingText[0];
            return _parsingText[_parsingIndex - 1];
        }

        private char NextParsedChar()
        {
            if (_parsingIndex < _parsingText.Length)
            {
                return _parsingText[_parsingIndex];
            }
            return '\0';
        }

        public string ReadObjectName(IMethodStatement st, bool inSingleStatement)
        {
            var objNameBuilder = new StringBuilder();
            bool splitObject = false;
            bool inString = false, inChar = false, nextCharEscaped = false;
            SkipWhitespace();
            int inTypeDepth=0;
            int inSquaresDepth = 0;
            char c = ParseNextChar();
            while ((c != ';') && (!inSingleStatement || (c != ',' && c != ')')) || inString || inChar)
            {
                if (inString || inChar)
                {
                    if (c == '\\' && !nextCharEscaped)
                    {
                        nextCharEscaped = true;
                    }

                    if (c == '\'' && inChar && !nextCharEscaped)
                    {
                        inChar = false;
                    }

                    if (c == '\"' && inString && !nextCharEscaped)
                    {
                        inString = false;
                    }
                    objNameBuilder.Append(c);
                    c = ParseNextChar();
                    continue;
                }

                if (c == '\"')
                {
                    inString = true;
                    objNameBuilder.Append(c);
                    c = ParseNextChar();
                    continue;
                }

                if (c == '\'')
                {
                    inChar = true;
                    objNameBuilder.Append(c);
                    c = ParseNextChar();
                    continue;
                }

                if (char.IsWhiteSpace(c))
                {
                    c = ParseNextChar();
                    continue;
                }

                if (IsAcceptableCharacter(c) || c == ',')
                {
                    //if (splitObject && !char.IsLetterOrDigit(c)) throw new Exception(string.Format("Invalid object name {0}", objNameBuilder));
                    objNameBuilder.Append(c);
                }

                if (c == '.')
                {
                    //splitObject = false;
                    ReadMethodCall(st);
                }
                c = ParseNextChar();
            }

            return objNameBuilder.ToString();
        }

        public void ReadMethodCall(IMethodStatement st)
        {
            var mc = new MethodCall();
            ReadMethodName(mc);
            if (!mc.IsProperty)
                ReadArgument(mc);
            st.MethodCalls.Add(mc);
        }

        public void ReadMethodName(MethodCall mc)
        {
            var methodNameBuilder = new StringBuilder();
            char c;
            SkipWhitespace();
            while ((c = ParseNextChar()) != '(' && c!='.' && c!=';' && c!=')' && c!='}')
            {
                if (char.IsWhiteSpace(c))
                    continue;

                if (IsAcceptableCharacter(c))
                {
                    methodNameBuilder.Append(c);
                }
                else
                {
                    throw new Exception(string.Format("Unexpected character {0} in method name", c));
                }
            }
            if (c == '.' || c==';' || c == ')' || c == '}')  
            {
                mc.IsProperty = true;
                _parsingIndex--;
            }
            mc.MethodName = methodNameBuilder.ToString();
        }

        public bool IsAcceptableCharacter(char c)
        {
            return char.IsLetterOrDigit(c) || AcceptableCharacters.Contains(c);
        }

        public int FindClosureIndex()
        {
            var currentIndex = _parsingIndex;
            bool inString = false, inChar = false, nextCharEscaped = false;
            while (currentIndex < _parsingText.Length)
            {
                if (inString || inChar)
                {
                    if (_parsingText[currentIndex] == '\\' && !nextCharEscaped)
                    {
                        nextCharEscaped = true;
                    }

                    if (_parsingText[currentIndex] == '\'' && inChar && !nextCharEscaped)
                    {
                        inChar = false;
                    }

                    if (_parsingText[currentIndex] == '\"' && inString && !nextCharEscaped)
                    {
                        inString = false;
                    }
                    currentIndex++;
                    continue;
                }

                if (_parsingText[currentIndex] == '\"')
                {
                    inString = true;
                    currentIndex++;
                    continue;
                }

                if (_parsingText[currentIndex] == '\'')
                {
                    inChar = true;
                    currentIndex++;
                    continue;
                }

                if (_parsingText[currentIndex] == '}')
                    return currentIndex;

                currentIndex++;
            }
            throw new Exception(string.Format("Object initializer syntax invalid at `{0}`", _parsingText.Substring(_parsingIndex)));
        }

        public int FindNextSquareBracketClosureIndex()
        {
            var currentIndex = _parsingIndex;
            while (_parsingText[currentIndex] != ']' && currentIndex < _parsingText.Length) currentIndex++;
            if (currentIndex >= _parsingText.Length) throw new Exception(string.Format("Did not find closing bracket from index {0}", _parsingIndex));
            return currentIndex;
        }

        public void ReadArgument(MethodCall mc)
        {
            var argBuilder = new StringBuilder();
            char c;
            bool nextCharEscaped = false;
            bool inString = false;
            bool inChar = false;
            int inTypeDepth=0;
            int inSquaresDepth = 0;

            while (_parsingText.Substring(_parsingIndex).Trim().StartsWith("new ") || _parsingText.Substring(_parsingIndex).Trim().StartsWith("new["))
            {   
                _parsingIndex += _parsingText.Substring(_parsingIndex).IndexOf("new", StringComparison.Ordinal) + 3;
                SkipWhitespace();
                
                Regex.IsMatch(_parsingText.Substring(_parsingIndex), @"^[\w\s\<\>\(\)]*\{");
                

                if (NextParsedChar() == '[')
                {
                    int startIndex = _parsingIndex;
                    _parsingIndex = FindNextSquareBracketClosureIndex();
                    _parsingIndex++;
                    SkipWhitespace();
                    int closureIndex = FindClosureIndex();
                    mc.Arguments.Add(
                        new StringArgument("new " +
                                           _parsingText.Substring(startIndex, closureIndex - startIndex + 1)));
                    _parsingIndex = closureIndex + 1;
                    
                } else 
                    if (Regex.IsMatch(_parsingText.Substring(_parsingIndex), @"^[\w\s\<\>\(\)\[\]]*\{"))
                {
                    int closureIndex = FindClosureIndex();
                    mc.Arguments.Add(
                        new StringArgument("new " +
                                           _parsingText.Substring(_parsingIndex, closureIndex - _parsingIndex + 1)));
                    _parsingIndex = closureIndex + 1;
                }
                else
                {
                    var starg = new StatementTypeArgument(ReadStatement(true)) {IsNew = true};
                    mc.Arguments.Add(starg);
                }
            }

            while (CurrentChar() != ')' || inChar || inString)
            {
                c = ParseNextChar();
                if (!inChar && !inString && CurrentChar() == ')') break;

                if (inString || inChar)
                {
                    if (c == '\\' && !nextCharEscaped)
                    {
                        nextCharEscaped = true;
                    }
                    
                    if (c == '\'' && inChar && !nextCharEscaped)
                    {
                        inChar = false;
                    }

                    if (c == '\"' && inString && !nextCharEscaped)
                    {
                        inString = false;
                    }
                    argBuilder.Append(c);
                    continue;
                }

                if (c == '\"')
                {
                    inString = true;
                    argBuilder.Append(c);
                    continue;
                }

                if (c == '\'')
                {
                    inChar = true;
                    argBuilder.Append(c);
                    continue;
                }

                if (char.IsWhiteSpace(c)) continue;
                if (c == '(')
                {
                    ReadLambdaTypeArgument(mc);
                    continue;
                }

                if (c == '=' && NextParsedChar() == '>')
                {
                    _parsingIndex--;
                    var lta = new LambdaTypeArgument();
                    lta.LambdaArguments.Add(argBuilder.ToString());
                    argBuilder.Clear();
                    ReadLambdaTypeArgument(mc, lta);
                    continue;
                }

                if (c == '[')
                {
                    inSquaresDepth++;
                    argBuilder.Append(c);
                    continue;
                }

                if (c == '<')
                {
                    inTypeDepth++;
                    argBuilder.Append(c);
                    continue;
                }
                if (c == '>')
                {
                    inTypeDepth--;
                    if (inTypeDepth < 0)
                        throw new Exception(string.Format("Invalid type argument specification around {0}", argBuilder));
                    argBuilder.Append(c);
                    continue;
                }

                if (c == ']')
                {
                    inSquaresDepth--;
                    if (inSquaresDepth < 0)
                        throw new Exception(string.Format("Invalid type argument specification around {0}", argBuilder));
                    argBuilder.Append(c);
                    continue;
                }


                if (c == ',')
                {
                    if (inTypeDepth != 0 || inSquaresDepth != 0)
                    {
                        argBuilder.Append(c);
                        continue;
                    }

                    if (argBuilder.Length > 0)
                    {
                        mc.Arguments.Add(new StringArgument(argBuilder.ToString()));
                        ReadArgument(mc);
                        return;
                    }
                    throw new Exception("Argument name expected, `,` found");
                }

                if (char.IsLetterOrDigit(c) || c == '.')
                {
                    argBuilder.Append(c);
                } 
                else 
                    throw new Exception(string.Format("Unexpected character `{0}` in argument name",c));
            }

            if (argBuilder.Length > 0)
                mc.Arguments.Add(new StringArgument(argBuilder.ToString()));
        }


        public void ReadLambdaTypeArgument(MethodCall mc, LambdaTypeArgument lta)
        {
            if (!_parsingText.Substring(_parsingIndex).Trim().StartsWith("=>")) throw new Exception("Presumably lambda argument invalid.");
            _parsingIndex += _parsingText.Substring(_parsingIndex).IndexOf("=>", StringComparison.Ordinal) + 2;
            ReadLambdaArgumentBody(lta);
            mc.Arguments.Add(lta);
        }

        public void ReadLambdaTypeArgument(MethodCall mc)
        {
            var lta = new LambdaTypeArgument();
            ReadLambdaArguments(lta);
            if (!_parsingText.Substring(_parsingIndex).Trim().StartsWith("=>")) throw new Exception("Presumably lambda argument invalid.");
            _parsingIndex += _parsingText.Substring(_parsingIndex).IndexOf("=>", StringComparison.Ordinal) + 2;
            ReadLambdaArgumentBody(lta);
            mc.Arguments.Add(lta);
        }

        public void ReadLambdaArguments(LambdaTypeArgument lta)
        {
            var argSet = _parsingText.Substring(_parsingIndex, _parsingText.Substring(_parsingIndex).IndexOf(')')).Split(',');
            _parsingIndex = _parsingText.Substring(_parsingIndex).IndexOf(')') + _parsingIndex + 1;
            lta.LambdaArguments.AddRange(argSet.Select(x => x.Trim()));
            ValidateLambdaArgs(lta);
        }

        public void ValidateLambdaArgs(LambdaTypeArgument lta)
        {
            var invalidArg = lta.LambdaArguments.FirstOrDefault(x => !x.All(char.IsLetterOrDigit));
            if (invalidArg != null)
            {
                throw new Exception(string.Format("Lambda argument `{0}` is invalid", invalidArg));
            }
        }

        public void ReadArgumentName() { }

        public void ReadLambdaArgumentBody(LambdaTypeArgument lta)
        {
            SkipWhitespace();
            if ((NextParsedChar() == '{'))
            {
                ParseNextChar();
                ReadMethodBody(lta.LambdaBody, false);
            }
            else
            {
                _inSingleStatementLambda = true;
                ReadMethodBody(lta.LambdaBody, true);
            }
        }
    }
}
