
namespace Markdown.Readers
{
    public abstract class Reader
    {
        public abstract void ReadChar(int index, string str);

        public abstract bool IsActive { get; set; }

        protected static bool Screened(int index, string str) => index > 0 && str[index - 1] == '\\';

        protected static bool UnderScoreBeforeSymbol(int index, string str) => index > 0 && str[index - 1] == '_';

        protected static bool UnderScoreAfterSymbol(int index, string str) => index < str.Length && str[index + 1] == '_';

        protected static bool WhiteSpaceAfterSymbol(int index, string str) => index < str.Length && str[index + 1] == ' ';

        protected static bool WhiteSpaceBeforeSymbol(int index, string str) => index >= 1 && str[index - 1] == ' ';

        protected static bool EndOfString(int index, string str) => index == str.Length - 1;
    }
}
