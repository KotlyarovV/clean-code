using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;
using Fclp.Internals.Extensions;

namespace Markdown.Benchmark
{

    public class MarkDownBenchmark
    {
        private static string GetGreatString(string str, int count)
        {
            var stringBuilder = new StringBuilder(str);
            Enumerable.Range(0, count).ForEach((a) => stringBuilder.Append(str));
            return stringBuilder.ToString();
        }

        private string markDownString;
        private string markDownStringRepeated100Times;
        private string markDownStringRepeated200Times;

        private const int RepetitionsNumber = 100;
        public const int MagnificationFactor = 2;
        private Md md;

        [GlobalSetup]
        public void GlobalSetUp()
        {
            markDownString = "_a _b_ b_ digits_aa_a __aaaa__b__bbb__ _a __b__ b_";
            markDownStringRepeated100Times = GetGreatString(markDownString, RepetitionsNumber);
            markDownStringRepeated200Times = GetGreatString(markDownString, RepetitionsNumber * MagnificationFactor);
            md = new Md();
        }

        [Benchmark(Description = "TestMarkDownStringBig")]
        public string TestMarkDownStringBig()
        {
            return md.RenderToHtml(markDownStringRepeated100Times);
        }

        [Benchmark(Description = "TestMarkDownStringBigger")]
        public string TestMarkDownStringBigger()
        {
            return md.RenderToHtml(markDownStringRepeated200Times);
        }
    }
}
