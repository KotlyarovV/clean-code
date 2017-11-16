using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown
{
    enum TagType
    {
        Opened, 
        Closed
    }

    class Tag
    {
        public readonly TokenType TokenType;
        public readonly int Index;
        public readonly TagType TagType;

        private static Dictionary<TokenType, string> stringRepresentation = new Dictionary<TokenType, string>
        {
            { TokenType.EmTag, "<em>"},
            { TokenType.StrongTag, "<strong>"}
        };

        private static Dictionary<TokenType, int> lengthOfMardownRepresentation = new Dictionary<TokenType, int>()
        {
            { TokenType.EmTag, 1},
            { TokenType.StrongTag, 2}
        };

        public int LengthOfMardownRepresentation => lengthOfMardownRepresentation[TokenType];

        public string TextRepresentation() =>
            (TagType == TagType.Opened)
                ? stringRepresentation[TokenType]
                : stringRepresentation[TokenType][0] + "/" + stringRepresentation[TokenType].Substring(1);

        public Tag(int index, TokenType tokenType, TagType tagType)
        {
            Index = index;
            TokenType = tokenType;
            TagType = tagType;
        }

    }
}
