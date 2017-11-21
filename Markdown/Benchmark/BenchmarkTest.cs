using System;
using BenchmarkDotNet.Running;
using NUnit.Framework;


namespace Markdown.Benchmark
{
        [TestFixture]
        class BenchmarkTest
        {
            [Test]
            [Category("BenchmarkTest")]
            public void SpeedTest()
            {
                var result = BenchmarkRunner.Run<MarkDownBenchmark>();
                var time1 = result.Reports[0].ResultStatistics.Median;
                var time2 = result.Reports[1].ResultStatistics.Median;
                Assert.True(time1 * Math.Pow(MarkDownBenchmark.MagnificationFactor, 2) > time2);
            }
        }
}
