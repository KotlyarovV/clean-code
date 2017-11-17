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
        public readonly TokenType tokenType;
        public readonly int Index;
        public readonly TagType tagType;

        private static readonly Dictionary<TokenType, string> stringRepresentation = 
            new Dictionary<TokenType, string>
        {
            { TokenType.EmTag, "<em>"},
            { TokenType.StrongTag, "<strong>"}
        };

        private static readonly Dictionary<TokenType, string> mardownRepresentation = 
            new Dictionary<TokenType, string>()
        {
            { TokenType.EmTag, "_"},
            { TokenType.StrongTag, "__"}
        };
        
        public int LengthOfMardownRepresentation => mardownRepresentation[tokenType].Length;

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
