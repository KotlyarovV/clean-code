using System.Collections.Generic;
using System.Linq;

namespace Markdown
{
    // почему решил сделать методом расширения, а не приватным в классе?
    static class ListTokensExtension
    {
        public static IEnumerable<Tag> GetTags(this List<Token> tokens)
        {
            var openTags = tokens.Select(token => new Tag(token.Start, token.Type, TagType.Opened));
            var closedTags = tokens.Select(token => new Tag(token.End, token.Type, TagType.Closed));

            // variable is only assigned but never used
            var wasEmTag = false;
            var wasNestedStrongTag = false;

            return openTags
                .Concat(closedTags)
                .OrderBy(tag => tag.Index);
        }
    }
}
