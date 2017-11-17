﻿using System.Collections.Generic;

namespace Markdown.Readers
{
    public abstract class Reader
    {
        public abstract bool IsStartState(int index, string str);

        public abstract bool IsFinalState(int index, string str);

        public bool IsActive { get; set; }

        protected Stack<int> LeftBoards { get; set; } = new Stack<int>();

        protected abstract TokenType TokenType { get; }

        protected abstract List<Token> Tokens { get; set; }

        protected static bool Screened(int index, string str) => index > 0 && str[index - 1] == '\\';

        protected static bool SymbolAfterIndex(int index, string str, char symbol) => index < str.Length && str[index + 1] == symbol;

        protected static bool SymbolBeforeIndex(int index, string str, char symbol) => index > 0 && str[index - 1] == symbol;

        protected static bool DigitBeforeSymbol(int index, string str) => index > 0 && char.IsDigit(str[index - 1]);

        protected static bool EndOfString(int index, string str) => index == str.Length - 1;

        public virtual void ReadChar(int index, string str)
        {
            if (IsStartState(index, str))
            {
                LeftBoards.Push(index);
                IsActive = true;
            }

            else if (IsFinalState(index, str))
            {
                Tokens.Add(new Token(TokenType, LeftBoards.Pop(), index));
                IsActive = false;
            }

        }
    }
}
