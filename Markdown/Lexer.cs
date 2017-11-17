using System.Collections.Generic;
using System.Linq;
using System.Text;
using Markdown.Readers;

namespace Markdown
{
    class Lexer
    {
        private readonly List<Reader> readers = new List<Reader>();
        private readonly string text;
        private readonly List<Token> tokens = new List<Token>();

        public Lexer(string text)
        {
            var strongReader = new StrongReader(tokens);
            var emReader = new EmReader(tokens, strongReader);

            readers.Add(emReader);
            readers.Add(strongReader);

            this.text = text;
        }

        private bool IsEscapedSymbol(int i, string str) => 
            i < str.Length - 1 && str[i] == '\\' && str[i + 1] == '_';

        public List<Token> FormedTokens()
        {
            for (var i = 0; i < text.Length; i++)
            {
                readers.ForEach(reader => reader.ReadChar(i, text));
            }
            return tokens;
        }

        private IEnumerable<Tag> GetTags()
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
                    if (tag.tokenType == TokenType.EmTag)
                    {
                        wasEmTag = tag.tagType == TagType.Opened;
                        return true;
                    }
                    if (tag.tokenType == TokenType.StrongTag &&  wasEmTag)
                    {
                        wasNestedStrongTag = tag.tagType == TagType.Opened;
                        return false;
                    }
                    if (tag.tokenType == TokenType.StrongTag && wasNestedStrongTag)
                    {
                        wasNestedStrongTag = false;
                        return false;
                    }
                    return true;
                });
        }

        public string GetFinalString()
        {
            var tags = GetTags();

            var newString = new StringBuilder();
            var tagsEnumerator =  tags.GetEnumerator();
            var hasNext = tagsEnumerator.MoveNext();
    
            for (var i = 0; i < text.Length; i++)
            {
                if (IsEscapedSymbol(i, text)) continue;

                if (hasNext && i == tagsEnumerator.Current.Index)
                {
                    newString.Append(tagsEnumerator.Current.TextRepresentation);
                    i = i - 1 + tagsEnumerator.Current.LengthOfMardownRepresentation;
                    hasNext = tagsEnumerator.MoveNext();
                }
                else newString.Append(text[i]);       
            }

            return newString.ToString();
        }
    }
}
