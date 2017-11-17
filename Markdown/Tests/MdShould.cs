using NUnit.Framework;


namespace Markdown.Tests
{
    [TestFixture]
    public class Md_ShouldRender
    {
        private Md md;

        [SetUp]
        public void SetUp()
        {
            md = new Md();
        }

        [TestCase("<em>a <em>b</em> b</em>", "_a _b_ b_", TestName = "two_single_underscores_nested")]
        [TestCase("<em>aaaa</em>b<em>bbb</em>", "_aaaa_b_bbb_", TestName = "two_single_underscores_not_nested_")]
        [TestCase("<em>aaaa</em>", "_aaaa_", TestName = "one_single_underscore_many_letters")]
        [TestCase("<em>a</em>", "_a_", TestName = "one_single_underscore")]
        [TestCase("digits<em>aa</em>a", "digits_aa_a", TestName = "underscoresin_middle_of_string")]
        public void SingleUnderscore_ShouldAddTagEm(string expetedStrng, string mdString)
        {
            Assert.AreEqual(expetedStrng, md.RenderToHtml(mdString));
        }

        [TestCase("<strong>a</strong>", "__a__", TestName = "one_double_underscores")]
        [TestCase("<strong>aaaa</strong>", "__aaaa__", TestName = "two_double_underscores_many_letters")]
        [TestCase("<strong>aaaa</strong>b<strong>bbb</strong>", "__aaaa__b__bbb__", TestName = "two_double_underscores_not_nested_")]
        [TestCase("<strong>a <strong>b</strong> b</strong>", "__a __b__ b__", TestName = "two_double_underscores_nested_")]
        public void DoubleUnderscore_TagStrongAdded(string expetedStrng, string mdString)
        {
            Assert.AreEqual(expetedStrng, md.RenderToHtml(mdString));
        }
        
        [TestCase("<strong>a <em>b</em> b</strong>", "__a _b_ b__", TestName = "single_nested_in_double_underscore")]
        [TestCase("<em>a __b__ b</em>", "_a __b__ b_", TestName = "double_nested_in_single_underscores")]
        public void NestedDoubleAndSingleUnderscores_AddedNestedTags(string expetedStrng, string mdString)
        {
            Assert.AreEqual(expetedStrng, md.RenderToHtml(mdString));
        }

        [TestCase("__a__", @"\__a\__", TestName = "escaped_symbols")]
        public void ScreenedUnderscores_UnderscoresIsScreened(string expetedStrng, string mdString)
        {
            Assert.AreEqual(expetedStrng, md.RenderToHtml(mdString));
        }

        [TestCase("digits__12__3", "digits__12__3", TestName = "double_underscores_with_digits_not_changed")]
        [TestCase("digits_12_3", "digits_12_3", TestName = "underscores_with_digits_not_changed")]
        public void DigitsWithUnderscores_InderscoresIsNotChanged(string expetedStrng, string mdString)
        {
            Assert.AreEqual(expetedStrng, md.RenderToHtml(mdString));
        }

        [TestCase("digits <strong>12__3</strong>", "digits __12__3__", TestName = "double_underscores_with_whitespaces_add_tags")]
        [TestCase("digits <em>12_3</em>", "digits _12_3_", TestName = "underscores_and_whitespaces_with_digits_add_tags")]
        [TestCase("<strong>12__3</strong> digits", "__12__3__ digits", TestName = "double_underscores_at_begin_with_whitespaces_add_tags")]
        [TestCase("<em>12_3</em> digits", "_12_3_ digits", TestName = "underscores_and_whitespaces_at_begin_with_digits_add_tags")]
        public void DigitsWithUnderscoresAndWhitespaces_ShouldBeTrue(string expetedStrng, string mdString)
        {
            Assert.AreEqual(expetedStrng, md.RenderToHtml(mdString));
        }
    }
}
