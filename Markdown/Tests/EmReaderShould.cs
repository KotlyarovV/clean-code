using Markdown.Readers;
using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    class EmReaderShould
    {
        private EmReader emReader;

        [SetUp]
        public void SetUp()
        {
            var strongReader = new StrongReader();
            emReader = new EmReader(strongReader);
        }

        [TestCase("_a_", 0, TestName = "start_of_string_is_start_of_token")]
        [TestCase("dfd_a_", 3, TestName = "underscore_in_the_middle_of_string")]
        [TestCase("dfd _a_", 4, TestName = "whitrespace_before_underscore")]
        [TestCase("dfd _a _a_f_", 7, TestName = "check_second_start_in_nested_tags")]
        public void SymbolInIndexIsStartSymbol_ShouldBeTrue(string str, int index)
        {
            for (var i = 0; i < index; i++)
                emReader.ReadChar(i, str);
            Assert.IsTrue(emReader.IsStartState(index, str));
        }

        [TestCase("_ a_", 0, TestName = "underscore_with_whitespace")]
        [TestCase("1_a_", 1, TestName = "underscore_with_digits")]
        public void SymbolInIndexIsNotStartSymbol_ShouldBeFalse(string str, int index)
        {
            for (var i = 0; i < index; i++)
                emReader.ReadChar(i, str);
            Assert.IsFalse(emReader.IsStartState(index, str));
        }

        [TestCase("_a_", 2, TestName = "end_of_string_is_end_of_token")]
        [TestCase("dfd_a_dfdf", 5, TestName = "underscores_in_the_middle_of_string")]
        [TestCase("dfd _a_", 6, TestName = "whitrespace_before_underscore")]
        [TestCase("dfd _a _a_f_", 11, TestName = "check_first_end_in_nested_tags")]
        public void SymbolInIndexIsFinalSymbol_ShouldBeTrue(string str, int index)
        {
            for (var i = 0; i < index; i++)
                emReader.ReadChar(i, str);
            Assert.IsTrue(emReader.IsFinalState(index, str));
        }

        [TestCase("_a _", 3, TestName = "underscore_after_whitespace")]
        [TestCase("_dfdf1_1", 6, TestName = "underscores_with_digit")]
        public void SymbolInIndexIsNotFinalSymbol_ShouldBeFalse(string str, int index)
        {
            for (var i = 0; i < index; i++)
                emReader.ReadChar(i, str);
            Assert.IsFalse(emReader.IsFinalState(index, str));
        }
    }
}
