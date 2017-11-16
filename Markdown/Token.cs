using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown
{
    public enum TokenType
    {
        NonSpesialText,
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
            this.End = end;
            this.Start = start;
            this.Type = type;
        }
    }
}
