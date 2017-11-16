using System;
using System.Collections.Generic;


namespace Markdown.Readers
{
    class StrongReader : Reader
    {
        private readonly Stack<int> leftBoards = new Stack<int>();
        private readonly List<Token> tokens;
        private readonly Lexer lexer;

        public override bool IsActive { get; set; }


        public StrongReader(List<Token> tokens, Lexer lexer)
        {
            this.tokens = tokens;
            this.lexer = lexer;
        }

        private bool IsStartState(int index, string str)
        {
            var startState = index + 2 < str.Length && str[index] == '_' && 
                UnderScoreAfterSymbol(index, str) &&
                (!Screened(index, str) || index == 0) &&
                   (!(WhiteSpaceAfterSymbol(index + 1, str) || 
                   UnderScoreAfterSymbol(index + 1, str) || 
                   Char.IsDigit(str[index + 2])))
                   && (leftBoards.Count == 0 || !IsFinalState(index, str));
            return startState;
        }

        private bool IsFinalState(int index, string str)
        {
            return index + 1 < str.Length && str[index] == '_' && 
                UnderScoreAfterSymbol(index, str) &&
                   (!WhiteSpaceBeforeSymbol(index, str));
        }



        public override void ReadChar(int index, string str)
        {
            if (IsStartState(index, str))
            {
                leftBoards.Push(index);
                IsActive = true;
            }

            else if (IsFinalState(index, str))
            {
                IsActive = false;
                tokens.Add(new Token(TokenType.StrongTag, leftBoards.Pop(), index));
            }

        }
    }
}
