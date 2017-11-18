﻿using System.Collections.Generic;
using System.Text;
using Markdown.Readers;

namespace Markdown
{
	public class Md
	{
	    private readonly List<Reader> readers = new List<Reader>();
	    private readonly List<Token> tokens = new List<Token>();

	    public Md()
	    {
	        var strongReader = new StrongReader(tokens);
	        var emReader = new EmReader(tokens, strongReader);

	        readers.Add(emReader);
	        readers.Add(strongReader);
	    }

	    private bool IsEscapedSymbol(int i, string str) =>
	        i < str.Length - 1 && str[i] == '\\' && str[i + 1] == '_';

		// непонятное название
	    private List<Token> FormedTokens(string text)
	    {
	        for (var i = 0; i < text.Length; i++)
	        {
	            readers.ForEach(reader => reader.ReadChar(i, text));
	        }
	        return tokens;
	    }

	    public string RenderToHtml(string markdown)
	    {
		    // насколько хорошо это будет расширяться при добавлении нового синтаксиса в маркдаун?
	        var tags = FormedTokens(markdown).GetTags().DeleteNested(TokenType.EmTag, TokenType.StrongTag);

	        var newString = new StringBuilder();
	        var tagsEnumerator = tags.GetEnumerator();
	        var hasNext = tagsEnumerator.MoveNext();

	        for (var i = 0; i < markdown.Length; i++)
	        {
	            if (IsEscapedSymbol(i, markdown)) continue;

		        // possible null reference exception
	            if (hasNext && i == tagsEnumerator.Current.Index)
	            {
	                newString.Append(tagsEnumerator.Current.TextRepresentation);
	                i = i - 1 + tagsEnumerator.Current.LengthOfMardownRepresentation;
	                hasNext = tagsEnumerator.MoveNext();
	            }
	            else newString.Append(markdown[i]);
	        }

	        tokens.Clear(); // зачем?
            tagsEnumerator.Dispose();

            return newString.ToString();
	    }
	}
}