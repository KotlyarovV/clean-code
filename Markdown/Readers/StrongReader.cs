namespace Markdown.Readers
{
    class StrongReader : Reader
    {
        protected override TokenType TokenType { get; } = TokenType.StrongTag;

        private static bool AtInEndOfString(int index, string str) => index + 1 >= str.Length;

        private static readonly int lengthOfElement = Tag.MardownRepresentation[TokenType.StrongTag].Length;

        private static bool HasSymbolAfterElement(int index, string str) => index + lengthOfElement < str.Length;

        public override bool IsStartState(int index, string str)
        {
            var nextIndex = index + 1;

            return str[index] == Underscore && HasSymbolAfterElement(index, str)
                && SymbolAfterIndex(index, str, Underscore)
                && (!(IsEscapedSymbol(index, str) || DigitBeforeSymbol(index, str)) || index == 0)
                && (!(WhiteSpaceAfterIndex(nextIndex, str)
                    || SymbolAfterIndex(nextIndex, str, Underscore)
                    || DigitAfterSymbol(nextIndex, str) && !(WhiteSpaceBeforeIndex(index, str) || index == 0)))
                && (LeftBoards.Count == 0 || !IsFinalState(index, str));
        }

        public override bool IsFinalState(int index, string str)
        {
            var nextIndex = index + 1;

            return str[index] == Underscore
                && !AtInEndOfString(index, str)
                && SymbolAfterIndex(index, str, Underscore)
                && !(WhiteSpaceBeforeIndex(index, str)
                    || DigitBeforeSymbol(index, str)
                    && !(WhiteSpaceAfterIndex(nextIndex, str) || index == str.Length - lengthOfElement))
                && LeftBoards.Count != 0;
        }
    }
}
