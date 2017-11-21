namespace Markdown.Readers
{
    class EmReader : Reader
    {
        protected override TokenType TokenType { get; } = TokenType.EmTag;

        private static readonly int lengthOfElement = Tag.MardownRepresentation[TokenType.EmTag].Length;

        private readonly StrongReader strongReader;

        public EmReader(StrongReader strongReader)
        {
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
