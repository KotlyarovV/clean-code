using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown
{
    static class IEnumerableTagsExtension
    {
        public static IEnumerable<Tag> DeleteNested(this IEnumerable<Tag> tags, TokenType externalTag, TokenType nestedTag)
        {
            var wasExternalTag = false;
            var wasNestedTag = false;

            return tags.Where((tag) =>
            {
                if (tag.TokenType == externalTag)
                {
                    wasExternalTag = tag.TagType == TagType.Opened;
                    return true;
                }
                if (tag.TokenType == nestedTag && (wasExternalTag || wasNestedTag))
                {
                    wasNestedTag = tag.TagType == TagType.Opened;
                    return false;
                }
                return true;
            });
        }
    }
}
