using MSG.IO;
using MSG.Types.String;
using NUnit.Framework;
using System;

namespace MSGTest.IO.EditorTests
{
    [TestFixture]
    public class LineDisplaySubstringsTests
    {
        [Test]
        public void TestCreateEmpty()
        {
            Editor.LineDisplaySubstring substring = new Editor.LineDisplaySubstring("Test");
            Assert.AreEqual(0, substring.StartIndex);
            Assert.AreEqual(0, substring.Length);
        }

        [Test]
        public void TestCreateWithStart()
        {
            Editor.LineDisplaySubstring substring = new Editor.LineDisplaySubstring("Test", 1);
            Assert.AreEqual(1, substring.StartIndex);
            Assert.AreEqual(0, substring.Length);
        }

        [Test]
        public void TestCreateWithBoth()
        {
            Editor.LineDisplaySubstring substring = new Editor.LineDisplaySubstring("Test", 1, 2);
            Assert.AreEqual(1, substring.StartIndex);
            Assert.AreEqual(2, substring.Length);
        }

        [Test]
        public void TestCreateWithBothTakingUpEntireLine()
        {
            Editor.LineDisplaySubstring substring = new Editor.LineDisplaySubstring("Test", 0, 4);
            Assert.AreEqual(0, substring.StartIndex);
            Assert.AreEqual(4, substring.Length);
        }

        [Test]
        public void TestCreateWithNegativeStartThrows()
        {
            var ex = Assert.Catch<ArgumentOutOfRangeException>(() => new Editor.LineDisplaySubstring("Test", -1));
            StringAssert.Contains("Line start must be nonnegative", ex.Message);
        }

        [Test]
        public void TestCreateWithStartOutsideTextThrows()
        {
            var ex = Assert.Catch<ArgumentOutOfRangeException>(() => new Editor.LineDisplaySubstring("Test", 10));
            StringAssert.Contains("Line start must be inside text", ex.Message);
            ex = Assert.Catch<ArgumentOutOfRangeException>(() => new Editor.LineDisplaySubstring("Test", 10, 1));
            StringAssert.Contains("Line start must be inside text", ex.Message);
        }

        [Test]
        public void TestCreateWithNegativeLengthThrows()
        {
            var ex = Assert.Catch<ArgumentOutOfRangeException>(() => new Editor.LineDisplaySubstring("Test", 0, -1));
            StringAssert.Contains("Line length must be nonnegative", ex.Message);
        }

        [Test]
        public void TestCreateWithLengthExtendingOutsideTextThrows()
        {
            var ex = Assert.Catch<ArgumentOutOfRangeException>(() => new Editor.LineDisplaySubstring("Test", 0, 5));
            StringAssert.Contains("Line length must not extend outside text", ex.Message);
        }
    }

}
