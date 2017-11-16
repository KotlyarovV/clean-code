using System;
using System.Collections.Generic;


namespace Markdown.Readers
{
    class EmReader : Reader
    {
        private readonly Stack<int> leftBoards = new Stack<int>();
        private readonly List<Token> tokens;
        private readonly Lexer lexer;

        public override bool IsActive { get; set; } 

        public EmReader(List<Token> tokens, Lexer lexer)
        {
            this.tokens = tokens;
            this.lexer = lexer;
        }

        private bool IsStartState(int index, string str)
        {
            return (str[index] == '_' && (!Screened(index, str) 
                && !UnderScoreBeforeSymbol(index, str) || index == 0)) &&
                   !EndOfString(index, str) &&
                   (!(WhiteSpaceAfterSymbol(index, str)
                   || UnderScoreAfterSymbol(index, str)
                   || Char.IsDigit(str[index + 1])))
                   && (leftBoards.Count == 0 || !IsFinalState(index, str));
        }

        private bool IsFinalState(int index, string str)
        {
            return (leftBoards.Count != 0 && str[index] == '_'  && 
                !WhiteSpaceBeforeSymbol(index, str) &&
                !UnderScoreBeforeSymbol(index, str)) &&
                (index == str.Length - 1 || (!(UnderScoreAfterSymbol(index, str) && lexer.StrongReader.IsActive)));
        }

        private void AddNewToken(Token token)
        {
            var i = tokens.Count - 1;
            while (i >= 0 && tokens[i].Start >= token.Start)
            {
                if (tokens[i].Type == TokenType.StrongTag) 
                    tokens.RemoveAt(i);
                i--;
            }
            tokens.Add(token);
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
                AddNewToken(new Token(TokenType.EmTag, leftBoards.Pop(), index));
                IsActive = false;
            }
            
        }
        
    }
}
