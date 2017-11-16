using System.Collections.Generic;
using System.Linq;
using System.Text;
using Markdown.Readers;

namespace Markdown
{
    // жаль тестов нет на этот класс
    class Lexer
    {

        private readonly List<Reader> readers = new List<Reader>();
        private readonly string text;

        public readonly EmReader EmReader;
        public readonly StrongReader StrongReader;
        private readonly List<Token> tokens = new List<Token>();

        public Lexer(string text)
        {
            // Lexer использует EmReader, а EmReader использует Lexer
            EmReader = new EmReader(tokens, this);
            StrongReader = new StrongReader(tokens, this);

            readers.Add(EmReader);
            readers.Add(StrongReader);

            this.text = text;
        }

        // может IsEscapeSymbol? 
        public bool ScreeningSymbol(int i, string str) => i < str.Length - 1 && str[i] == '\\' && str[i + 1] == '_';

        // Метод вроде Get, а вроде и void :(
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
            var tags = GetTags();

            var newString = new StringBuilder();
            var tagsEnumerator =  tags.GetEnumerator(); // Enumerator need to be Disposed
            tagsEnumerator.MoveNext();

            for (var i = 0; i < text.Length; i++)
            {
                if (ScreeningSymbol(i, text)) 
                    continue;

                // этот код нужно упростить
                if (tagsEnumerator.Current != null  && i == tagsEnumerator.Current.Index)
                while (i == tagsEnumerator.Current.Index)
                {
                    newString.Append(tagsEnumerator.Current.TextRepresentation);
                    i = i - 1 + tagsEnumerator.Current.LengthOfMardownRepresentation;
                    
                    if (!tagsEnumerator.MoveNext()) 
                        break;        
                }
                else 
                    newString.Append(text[i]);       
            }

            return newString.ToString();
        }
    }
}
