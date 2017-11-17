using System;
using System.Collections.Generic;


namespace Markdown.Readers
{
    class EmReader : Reader
    {
        protected override TokenType TokenType { get; } = TokenType.EmTag;

        protected sealed override List<Token> Tokens { get; set; }

        private readonly StrongReader strongReader;

        public EmReader(List<Token> tokens, StrongReader strongReader)
        {
            Tokens = tokens;
            this.strongReader = strongReader;
        }

        public override bool IsStartState(int index, string str)
        {
            return (str[index] == '_' 
                && (!Screened(index, str) 
                && (!(SymbolBeforeIndex(index, str, '_') || DigitBeforeSymbol(index, str))) || index == 0)) 
                && !EndOfString(index, str) 
                &&(!(SymbolAfterIndex(index, str, ' ') || SymbolAfterIndex(index, str, '_') || char.IsDigit(str[index + 1])))
                && (LeftBoards.Count == 0 || !IsFinalState(index, str));
        }

        public override bool IsFinalState(int index, string str)
        {
            return (LeftBoards.Count != 0 && str[index] == '_'  
                && !(SymbolBeforeIndex(index, str, ' ') || DigitBeforeSymbol(index, str)) 
                && !SymbolBeforeIndex(index, str, '_')) 
                && (index == str.Length - 1 || (!(SymbolAfterIndex(index, str, '_') && strongReader.IsActive)));                
        }
    }
}
