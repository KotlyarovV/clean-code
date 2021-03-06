﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using Markdown.Readers;

namespace Markdown
{
    public class Md
    {
        private readonly List<Reader> readers = new List<Reader>();
        
        public Md()
        {
            var strongReader = new StrongReader();
            var emReader = new EmReader();

            readers.Add(emReader);
            readers.Add(strongReader);
        }


        private List<Token> GetTokens(string text)
        {
            var tokens = new List<Token>();
            for (var i = 0; i < text.Length; i++)
            {
                readers.ForEach(reader =>
                {
                    var token = reader.ReadChar(i, text);
                    if (token != null)
                        tokens.Add(token);
                });
            }
            return tokens;
        }

        private static IEnumerable<Tag> GetTags(List<Token> tokens)
        {
            var openTags = tokens.Select(token => new Tag(token.Start, token.Type, TagType.Opened));
            var closedTags = tokens.Select(token => new Tag(token.End, token.Type, TagType.Closed));

            return openTags
                .Concat(closedTags)
                .OrderBy(tag => tag.Index);
        }

        private static IEnumerable<Tag> DeleteNested(IEnumerable<Tag> tags, TokenType externalTag, TokenType nestedTag)
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

        public string RenderToHtml(string markdown)
        {
            var tokens = GetTokens(markdown);
            var tags = GetTags(tokens);
            var tagsWithoutNested = DeleteNested(tags, TokenType.EmTag, TokenType.StrongTag);

            var newString = new StringBuilder();
            var tagsEnumerator = tagsWithoutNested.GetEnumerator();
            var hasNext = tagsEnumerator.MoveNext();

            for (var i = 0; i < markdown.Length; i++)
            {
                if (i < markdown.Length - 1 && Reader.IsEscapedSymbol(i + 1, markdown)) continue;

                if (hasNext && tagsEnumerator.Current != null && i == tagsEnumerator.Current.Index)
                {
                    newString.Append(tagsEnumerator.Current.TextRepresentation);
                    i = i - 1 + tagsEnumerator.Current.LengthOfMardownRepresentation;
                    hasNext = tagsEnumerator.MoveNext();
                }
                else newString.Append(markdown[i]);
            }

            tokens.Clear();
            tagsEnumerator.Dispose();

            return newString.ToString();
        }
    }
}