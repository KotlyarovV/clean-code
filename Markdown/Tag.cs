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
        private readonly TokenType tokenType;
        public readonly int Index;
        private readonly TagType tagType;

        private static readonly Dictionary<TokenType, string> stringRepresentation = 
            new Dictionary<TokenType, string>
        {
            { TokenType.EmTag, "<em>"},
            { TokenType.StrongTag, "<strong>"}
        };
 
        private static readonly Dictionary<TokenType, int> lengthOfMardownRepresentation = 
            new Dictionary<TokenType, int>()
        {
            { TokenType.EmTag, 1}, // можно не хардкодить значения, а вычислять
            { TokenType.StrongTag, 2}
        };

        public int LengthOfMardownRepresentation => lengthOfMardownRepresentation[tokenType];

        public string TextRepresentation =>
            (tagType == TagType.Opened)
                ? stringRepresentation[tokenType]
                : stringRepresentation[tokenType][0] + "/" + stringRepresentation[tokenType].Substring(1);

        public Tag(int index, TokenType tokenType, TagType tagType)
        {
            Index = index;
            this.tokenType = tokenType;
            this.tagType = tagType;
        }

    }
}
