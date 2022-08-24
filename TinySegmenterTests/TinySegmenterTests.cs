using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TinySegmenter.Tests
{
    [TestClass()]
    public class TinySegmenterTests
    {
        [TestMethod()]
        public void SegmentTest()
        {
            var tokens = TinySegmenter.Segment("私の名前は中野です");
            Assert.AreEqual("私 | の | 名前 | は | 中野 | です", string.Join(" | ", tokens));
        }
    }
}