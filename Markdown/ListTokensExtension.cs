using System.Collections.Generic;
using System.Linq;

namespace Markdown
{
    static class ListTokensExtension
    {

        public static IEnumerable<Tag> GetTags(this List<Token> tokens)
        {
            var openTags = tokens.Select(token => new Tag(token.Start, token.Type, TagType.Opened));
            var closedTags = tokens.Select(token => new Tag(token.End, token.Type, TagType.Closed));

            var wasEmTag = false;
            var wasNestedStrongTag = false;

            return openTags
                .Concat(closedTags)
                .OrderBy(tag => tag.Index)
                .Where((tag) =>
                {
                    if (tag.TokenType == TokenType.EmTag)
                    {
                        wasEmTag = tag.TagType == TagType.Opened;
                        return true;
                    }
                    if (tag.TokenType == TokenType.StrongTag && (wasEmTag || wasNestedStrongTag))
                    {
                        wasNestedStrongTag = tag.TagType == TagType.Opened;
                        return false;
                    }
                    return true;
                });
        }
    }
}
