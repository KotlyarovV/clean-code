using NUnit.Framework;

namespace Markdown
{
	public class Md
	{
		public string RenderToHtml(string markdown)
		{
            var lexer = new Lexer(markdown);
            lexer.FormedTokens();
		    return lexer.GetFinalString();
		}
	}
}