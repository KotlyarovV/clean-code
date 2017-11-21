using System.Collections.Generic;

namespace Markdown.Readers
{
    public abstract class Reader
    {
        public abstract bool IsStartState(int index, string str);

        public abstract bool IsFinalState(int index, string str);

        public bool IsActive { get; private set; }

        protected Stack<int> LeftBoards { get; set; } = new Stack<int>();

        protected abstract TokenType TokenType { get; }
        
        protected const char Underscore = '_';

        public static bool IsEscapedSymbol(int index, string str) => index > 0 && str[index - 1] == '\\';

        protected static bool SymbolAfterIndex(int index, string str, char symbol) => index < str.Length - 1 && str[index + 1] == symbol;

        protected static bool WhiteSpaceAfterIndex(int index, string str) => SymbolAfterIndex(index, str, ' ') || SymbolAfterIndex(index, str, '\t');

        protected static bool WhiteSpaceBeforeIndex(int index, string str) => SymbolBeforeIndex(index, str, ' ') || SymbolBeforeIndex(index, str, '\t');

        protected static bool SymbolBeforeIndex(int index, string str, char symbol) => index > 0 && str[index - 1] == symbol;

        protected static bool DigitBeforeSymbol(int index, string str) => index > 0 && char.IsDigit(str[index - 1]);

        protected static bool DigitAfterSymbol(int index, string str) => index < str.Length - 1 && char.IsDigit(str[index + 1]);

        protected static bool EndOfString(int index, string str) => index == str.Length - 1;

        public Token ReadChar(int index, string str)
        {
            if (IsStartState(index, str))
            {
                LeftBoards.Push(index);
                IsActive = true;
            }

            else if (IsFinalState(index, str))
            {
                IsActive = false;
                return new Token(TokenType, LeftBoards.Pop(), index);
            }
            return null;
        }
    }
}
