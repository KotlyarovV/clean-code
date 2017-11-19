using System.Collections;
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

        [TestCaseSource(typeof(DataClass), nameof(DataClass.SingleUnderscore))]
        [TestCaseSource(typeof(DataClass), nameof(DataClass.DoubleUnderscore))]
        [TestCaseSource(typeof(DataClass), nameof(DataClass.ScreenedUnderscores))]

        [TestCaseSource(typeof(DataClass), nameof(DataClass.NestedDoubleAndSingleUnderscores))]
        [TestCaseSource(typeof(DataClass), nameof(DataClass.DigitsWithUnderscores))]
        [TestCaseSource(typeof(DataClass), nameof(DataClass.DigitsWithUnderscoresAndWhitespaces))]
        [TestCaseSource(typeof(DataClass), nameof(DataClass.DigitsWithUnderscoresAndTabs))]
        public string MdStringData_ShouldConvertToHtml(string expetedString)
        {
            return md.RenderToHtml(expetedString);
        }
    }

    public class DataClass
    {    
        public static IEnumerable SingleUnderscore()
        {
            yield return new TestCaseData("_a _b_ b_")
                .Returns("<em>a <em>b</em> b</em>")
                .SetName("two_single_underscores_nested");

            yield return new TestCaseData("_aaaa_b_bbb_")
                .Returns("<em>aaaa</em>b<em>bbb</em>")
                .SetName("two_single_underscores_not_nested_");

            yield return new TestCaseData("_aaaa_")
                .Returns("<em>aaaa</em>")
                .SetName("one_single_underscore_many_letters");

            yield return new TestCaseData("_a_")
                .Returns("<em>a</em>")
                .SetName("one_single_underscore");

            yield return new TestCaseData("digits_aa_a")
                .Returns("digits<em>aa</em>a")
                .SetName("underscoresin_middle_of_string");
        }

        public static IEnumerable DoubleUnderscore()
        {
            yield return new TestCaseData("__a__")
                .Returns("<strong>a</strong>")
                .SetName("one_double_underscores");

            yield return new TestCaseData("__aaaa__")
                .Returns("<strong>aaaa</strong>")
                .SetName("two_double_underscores_many_letters");

            yield return new TestCaseData("__aaaa__b__bbb__")
                .Returns("<strong>aaaa</strong>b<strong>bbb</strong>")
                .SetName("two_double_underscores_not_nested_");

            yield return new TestCaseData("__a __b__ b__")
                .Returns("<strong>a <strong>b</strong> b</strong>")
                .SetName("two_double_underscores_nested");
        }

        public static IEnumerable NestedDoubleAndSingleUnderscores()
        {
            yield return new TestCaseData("__a _b_ b__")
                .Returns("<strong>a <em>b</em> b</strong>")
                .SetName("single_nested_in_double_underscore");

            yield return new TestCaseData("_a __b__ b_")
                .Returns("<em>a __b__ b</em>")
                .SetName("double_nested_in_single_underscores");
        }

        public static IEnumerable ScreenedUnderscores()
        {
            yield return new TestCaseData(@"\__a\__")
                .Returns("__a__")
                .SetName("escaped_symbols");
        }

        public static IEnumerable DigitsWithUnderscores()
        {
            yield return new TestCaseData("digits__12__3")
                .Returns("digits__12__3")
                .SetName("double_underscores_with_digits_not_changed");

            yield return new TestCaseData("digits_12_3")
                .Returns("digits_12_3")
                .SetName("underscores_with_digits_not_changed");
        }

        public static IEnumerable DigitsWithUnderscoresAndWhitespaces()
        {
            yield return new TestCaseData("digits __12__3__")
                .Returns("digits <strong>12__3</strong>")
                .SetName("double_underscores_with_whitespaces_add_tags");

            yield return new TestCaseData("digits _12_3_")
                .Returns("digits <em>12_3</em>")
                .SetName("underscores_and_whitespaces_with_digits_add_tags");

            yield return new TestCaseData("__12__3__ digits")
                .Returns("<strong>12__3</strong> digits")
                .SetName("double_underscores_at_begin_with_whitespaces_add_tags");

            yield return new TestCaseData("_12_3_ digits")
                .Returns("<em>12_3</em> digits")
                .SetName("underscores_and_whitespaces_at_begin_with_digits_add_tags");
        }

        public static IEnumerable DigitsWithUnderscoresAndTabs()
        {
            yield return new TestCaseData("digits\t__12__3__")
                .Returns("digits\t<strong>12__3</strong>")
                .SetName("double_underscores_with_tab_add_tags");

            yield return new TestCaseData("digits\t_12_3_")
                .Returns("digits\t<em>12_3</em>")
                .SetName("underscores_and_tab_with_digits_add_tags");

            yield return new TestCaseData("__12__3__\tdigits")
                .Returns("<strong>12__3</strong>\tdigits")
                .SetName("double_underscores_at_begin_with_tab_add_tags");

            yield return new TestCaseData("_12_3_\tdigits")
                .Returns("<em>12_3</em>\tdigits")
                .SetName("underscores_and_tab_at_begin_with_digits_add_tags");
        }
    }    
}
