using System.Collections.Generic;
using System.Xml;


namespace Markdown.Readers
{
    class StrongReader : Reader
    {
        protected override TokenType TokenType { get; } = TokenType.StrongTag;

        protected sealed override List<Token> Tokens { get; set; } 
        

        public StrongReader(List<Token> tokens)
        {
            Tokens = tokens;
        }

        public override bool IsStartState(int index, string str)
        {
            // 2 - магическая константа
            // вводит в заблуждение форматирование блока с ||
            return index + 2 < str.Length && str[index] == '_' 
                && SymbolAfterIndex(index, str, '_') 
                && (!(Screened(index, str) || DigitBeforeSymbol(index, str)) || index == 0) 
                && (!(SymbolAfterIndex(index + 1, str, ' ') 
                || SymbolAfterIndex(index + 1, str, '_')
                || DigitAfterSymbol(index + 1, str) && !(SymbolBeforeIndex(index, str, ' ') || index == 0)))
                && (LeftBoards.Count == 0 || !IsFinalState(index, str));
        }

        public override bool IsFinalState(int index, string str)
        {
            // можно не хардкодить '_'
            // избавиться от непонятных констант
            return  str[index] == '_' 
                && index + 1 < str.Length 
                && SymbolAfterIndex(index, str, '_')
                && !(SymbolBeforeIndex(index, str, ' ')  // а если не пробел, а таб? 
                || DigitBeforeSymbol(index, str) 
                && !(SymbolAfterIndex(index + 1, str, ' ') || index == str.Length - 2))
                && LeftBoards.Count != 0;
        }
    }
}
