using MSG.IO;
using MSG.Types.String;
using NUnit.Framework;
using System;
using System.Diagnostics;

namespace MSGTest.IO
{
    [TestFixture]
    public class EditorTests
    {
        MSG.IO.Editor editor;
        TestPrint print;
        TestRead read;
        string input;

        [SetUp]
        public void SetUp()
        {
            print = new TestPrint();
            print.BufferWidth = 10;
            read = new TestRead(null);
            editor = new MSG.IO.Editor(print, read);
        }

        [Test]
        public void TestCharWasInsertedIntoBuffer()
        {
            read.SetNextKeys("a\r");
            input = editor.GetInput();
            Assert.AreEqual("a", input);
        }

        [Test]
        public void TestCharWasEchoedOnScreen()
        {
            read.SetNextKeys("a\r");
            input = editor.GetInput();
            Assert.AreEqual("a<0^0\n"
                       , print.Output);
        }

        [Test]
        public void TestCharsWereInsertedIntoBuffer()
        {
            read.SetNextKeys("Word\r");
            input = editor.GetInput();
            Assert.AreEqual("Word", input);
        }

        [Test]
        public void TestCharsWereEchoedOnScreen()
        {
            read.SetNextKeys("Word\r");
            input = editor.GetInput();
            string expected = "Word<0^0\n";
            Assert.AreEqual(expected, print.Output);
        }

        [Test]
        public void TestBackspace()
        {
            read.SetNextKeys("Word\b\r");
            input = editor.GetInput();
            string expected = "Word<3^0 <3^0<0^0\n";
            Assert.AreEqual(expected, print.Output);
        }
    }
}