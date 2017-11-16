namespace Markdown
{
    public enum TokenType
    {
        EmTag,
        StrongTag
    }

    public class Token
    {
        public readonly int Start;
        public readonly int End;
        public readonly TokenType Type;

        public Token(TokenType type, int start, int end)
        {
            End = end;
            Start = start;
            Type = type;
        }
    }
}
