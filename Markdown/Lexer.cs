using System.Collections.Generic;
using System.Linq;
using System.Text;
using Markdown.Readers;

namespace Markdown
{
    class Lexer
    {

        private readonly List<IReader> readers = new List<IReader>();
        private readonly string text;

        public readonly EmReader EmReader;
        public readonly StrongReader StrongReader;
        private readonly List<Token> tokens = new List<Token>();

        public Lexer(string text)
        {
            EmReader = new EmReader(tokens, this);
            StrongReader = new StrongReader(tokens, this);

            readers.Add(EmReader);
            readers.Add(StrongReader);

            this.text = text;
        }


        public void GetTokens()
        {
            for (int i = 0; i < text.Length; i++)
            {
                readers.ForEach(reader => reader.ReadChar(i, text));
            }
        }

        private IOrderedEnumerable<Tag> GetTags()
        {
            var openTags = tokens.Select(token => new Tag(token.Start, token.Type, TagType.Opened));
            var closedTags = tokens.Select(token => new Tag(token.End, token.Type, TagType.Closed));
            return openTags.Concat(closedTags).OrderBy(tag => tag.Index);
        }

        public string GetFinalString()
        {
            if (tokens.Count == 0)
                return text;
            else
            {
                var tags = GetTags();

                var newString = new StringBuilder();
                var tagsEnumerator =  tags.GetEnumerator();
                tagsEnumerator.MoveNext();

                for (var i = 0; i < text.Length; i++)
                {
                    if (i == tagsEnumerator.Current.Index)
                        while (i == tagsEnumerator.Current.Index)
                        {
                            newString.Append(tagsEnumerator.Current.TextRepresentation);
                            i = i - 1 + tagsEnumerator.Current.LengthOfMardownRepresentation;
                            if (!tagsEnumerator.MoveNext()) break;
                            
                        }
                    else newString.Append(text[i]);       
                }

                return newString.ToString();
            }
        }
    }
}
