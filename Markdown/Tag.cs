using System.Collections.Generic;


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

        private static readonly Dictionary<TokenType, string> stringRepresentation =
            new Dictionary<TokenType, string>
        {
            { TokenType.EmTag, "<em>"},
            { TokenType.StrongTag, "<strong>"}
        };

        public static readonly Dictionary<TokenType, string> MardownRepresentation =
            new Dictionary<TokenType, string>()
        {
            { TokenType.EmTag, "_"},
            { TokenType.StrongTag, "__"}
        };

        public int LengthOfMardownRepresentation => MardownRepresentation[TokenType].Length;

        public string TextRepresentation =>
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
