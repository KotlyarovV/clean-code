using Markdown.Readers;
using NUnit.Framework;


namespace Markdown.Tests
{
    [TestFixture()]
    class StrongReaderShould
    {
        private StrongReader strongReader;

        [SetUp]
        public void SetUp()
        {
            strongReader = new StrongReader();
        }

        [TestCase("__a__", 0, TestName = "start_of_string_is_start_of_token")]
        [TestCase("dfd__a__", 3, TestName = "underscore_in_the_middle_of_string")]
        [TestCase("dfd __a__", 4, TestName = "whitrespace_before_underscore")]
        [TestCase("dfd __a __a__f__", 8, TestName = "check_second_start_in_nested_tags")]
        public void SymbolInIndexIsStartSymbol_ShouldBeTrue(string str, int index)
        {
            for (var i = 0; i < index; i++)
                strongReader.ReadChar(i, str);
            Assert.IsTrue(strongReader.IsStartState(index, str));
        }

        [TestCase("__ a__", 0, TestName = "double_underscore_with_whitespace")]
        [TestCase("1__a__", 1, TestName = "double_underscore_with_digits")]
        public void SymbolInIndexIsNotStartSymbol_ShouldBeFalse(string str, int index)
        {
            for (var i = 0; i < index; i++)
                strongReader.ReadChar(i, str);
            Assert.IsFalse(strongReader.IsStartState(index, str));
        }

        [TestCase("__a__", 3, TestName = "end_of_string_is_end_of_token")]
        [TestCase("dfd__a__dfdf", 6, TestName = "underscores_in_the_middle_of_string")]
        [TestCase("dfd __a__", 7, TestName = "whitrespace_before_underscore")]
        [TestCase("dfd __a __a__f__", 11, TestName = "check_first_end_in_nested_tags")]
        public void SymbolInIndexIsFinalSymbol_ShouldBeTrue(string str, int index)
        {
            for (var i = 0; i < index; i++)
                strongReader.ReadChar(i, str);
            Assert.IsTrue(strongReader.IsFinalState(index, str));
        }

        [TestCase("__a __", 3, TestName = "underscore_after_whitespace")]
        [TestCase("__dfdf1__1", 6, TestName = "underscores_with_digit")]
        public void SymbolInIndexIsNotFinalSymbol_ShouldBeFalse(string str, int index)
        {
            for (var i = 0; i < index; i++)
                strongReader.ReadChar(i, str);
            Assert.IsFalse(strongReader.IsStartState(index, str));
        }
    }
}
