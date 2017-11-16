using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown.Readers
{
    class EmReader : Reader
    {
        private readonly Stack<int> leftBoards = new Stack<int>();
        private readonly List<Token> tokens;
        private readonly Lexer lexer;

        public bool IsActive { get; set; } = false;

        public EmReader(List<Token> tokens, Lexer lexer)
        {
            this.tokens = tokens;
            this.lexer = lexer;
        }

        private bool IsStartState(int index, string str)
        {
            return (str[index] == '_' && (index > 0 && str[index - 1] != '\\' && str[index - 1] != '_'  || index == 0)) &&
                   (index != str.Length - 1) &&
                   (!(str[index + 1] == ' ' || str[index + 1] == '_' || Char.IsDigit(str[index + 1])))
                   && (leftBoards.Count == 0 || !IsFinalState(index, str));
        }

        private bool IsFinalState(int index, string str)
        {
            return (leftBoards.Count != 0 && str[index] == '_'  && str[index - 1] != ' ' && str[index - 1] != '_') &&
                (index == str.Length - 1 || (!(str[index + 1] == '_' && lexer.StrongReader.IsActive)));
        }

        private void AddNewToken(Token token)
        {
            int i = tokens.Count - 1;
            while (i >= 0 && tokens[i].Start >= token.Start)
            {
                if (tokens[i].Type == TokenType.StrongTag) 
                    tokens.RemoveAt(i);
                i--;
            }
            tokens.Add(token);
        }
        

        public void ReadChar(int index, string str)
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
