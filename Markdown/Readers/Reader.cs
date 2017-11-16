
namespace Markdown.Readers
{
    public abstract class Reader
    {
        public abstract void ReadChar(int index, string str);
        public abstract bool IsActive { get; set; }

        protected bool Screened(int index, string str) => index > 0 && str[index - 1] == '\\';

        protected bool UnderScoreBeforeSymbol(int index, string str) => index > 0 && str[index - 1] == '_';

        protected bool UnderScoreAfterSymbol(int index, string str) => index < str.Length && str[index + 1] == '_';

        protected bool WhiteSpaceAfterSymbol(int index, string str) => index < str.Length && str[index + 1] == ' ';

        protected bool EndOfString(int index, string str) => index == str.Length - 1;
    }
}
