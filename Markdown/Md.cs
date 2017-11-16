using NUnit.Framework;

namespace Markdown
{
	public class Md
	{
		public string RenderToHtml(string markdown)
		{
            var lexer = new Lexer(markdown);
            lexer.GetTokens();
		    return lexer.GetFinalString();
		}
	}

	[TestFixture]
	public class Md_ShouldRender
	{
	    private Md md;

	    [SetUp]
	    public void SetUp()
	    {
	        md = new Md();
	    }

	    [Test]
	    public void OneUnderscore_TagEmAdded()
	    {
	        Assert.AreEqual("<em>a</em>", md.RenderToHtml("_a_"));
	    }

	    [Test]
	    public void OneUnderscoreManyLetters_TagEmAdded()
	    {
	        Assert.AreEqual("<em>aaaa</em>", md.RenderToHtml("_aaaa_"));
	    }

	    [Test]
	    public void TwoUnderscoreManyLetters_TwoTagsEmAdded()
	    {
	        Assert.AreEqual("<em>aaaa</em>b<em>bbb</em>", md.RenderToHtml("_aaaa_b_bbb_"));
	    }

	    [Test]
	    public void NestedUnderscore_NestedTagEmAdded()
	    {
	        Assert.AreEqual("<em>a <em>b</em> b</em>", md.RenderToHtml("_a _b_ b_"));
	    }

	    [Test]
	    public void DoubleUnderscore_TagStrongAdded()
	    {
	        Assert.AreEqual("<strong>a</strong>", md.RenderToHtml("__a__"));
	    }

	    [Test]
	    public void DoubleUnderscore_ManyLetters_TagStrongAdded()
	    {
	        Assert.AreEqual("<strong>aaaa</strong>", md.RenderToHtml("__aaaa__"));
	    }

	    [Test]
	    public void DoubleUnderscoreManyLettersTwoSection_TwoTagsStrongAdded()
	    {
	        Assert.AreEqual("<strong>aaaa</strong>b<strong>bbb</strong>", md.RenderToHtml("__aaaa__b__bbb__"));
	    }

	    [Test]
	    public void NestedDoubleUnderscores_NestedTagsStrongsAdded()
	    {
	        Assert.AreEqual("<strong>a <strong>b</strong> b</strong>", md.RenderToHtml("__a __b__ b__"));
	    }

	    [Test]
	    public void NestedDoubleAndOneUnderscores_NestedTagsStrongsAndEmAdded()
	    {
	        Assert.AreEqual("<strong>a <em>b</em> b</strong>", md.RenderToHtml("__a _b_ b__"));
	    }

	    [Test]
	    public void NestedOneAndDoubleUnderscores_NestedStrongsTagsNotAdded()
	    {
	        Assert.AreEqual("<em>a __b__ b</em>", md.RenderToHtml("_a __b__ b_"));
	    }

	    [Test]
	    public void ScreenedUnderscores_UnderscoresIsScreened()
	    {
	        Assert.AreEqual("_a_", md.RenderToHtml(@"\_a\_"));
	    }
    }
}