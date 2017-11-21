using System.Collections.Generic;


namespace Markdown.Readers
{
    class EmReader : Reader
    {
        protected override TokenType TokenType { get; } = TokenType.EmTag;

        protected sealed override List<Token> Tokens { get; set; }

        private static int lengthOfElement = 1; // Tag.LengthOfMarkdownRepresentation[TagType.Em].Length

        private readonly StrongReader strongReader;

        public EmReader(List<Token> tokens, StrongReader strongReader)
        {
            Tokens = tokens;
            this.strongReader = strongReader;
        }

        public override bool IsStartState(int index, string str)
        {
            return (str[index] == Underscore
                   && (!IsEscapedSymbol(index, str)
                   && (!(SymbolBeforeIndex(index, str, Underscore) || DigitBeforeSymbol(index, str))) || index == 0))
                   && !EndOfString(index, str)
                   && (!(WhiteSpaceAfterIndex(index, str)
                        || SymbolAfterIndex(index, str, Underscore)
                        || DigitAfterSymbol(index, str) && !(WhiteSpaceBeforeIndex(index, str) || index == 0)))
                   && (LeftBoards.Count == 0 || !IsFinalState(index, str));
        }

        public override bool IsFinalState(int index, string str)
        {
            return (LeftBoards.Count != 0 && str[index] == Underscore
                && !(WhiteSpaceBeforeIndex(index, str) || DigitBeforeSymbol(index, str)
                    && !(WhiteSpaceAfterIndex(index, str) || EndOfString(index, str)))
                && !SymbolBeforeIndex(index, str, Underscore)
                && (index == str.Length - lengthOfElement || (!(SymbolAfterIndex(index, str, Underscore) && strongReader.IsActive))));
        }
    }
}
