using MSG.IO;
using MSG.Types.String;
using NUnit.Framework;
using System;
using System.Diagnostics;

namespace MSGTest.IO
{
    // I thought I'd try doing setup stuff exclusively in SetUp() methods
    // (which leads to more test classes) and see how that goes.

    [TestFixture]
    public class InsertOneCharTests
    {
        TestPrint print;
        TestRead read;
        string input;

        [SetUp]
        public void SetUp()
        {
            print = new TestPrint();
            print.BufferWidth = 10;
            read = new TestRead(null);
            read.NextKeys = new char[] { 'a', '\r' };
            input = Editor.GetInput(print, read);
        }

        [Test]
        public void TestCharWasInsertedIntoBuffer()
        {
            Assert.AreEqual("a", input);
        }

        [Test]
        public void TestCharWasEchoedOnScreen()
        {
            Assert.AreEqual("a<0^0\n"
                       , print.Output);
        }
    }

    [TestFixture]
    public class InsertWordTests
    {
        TestPrint print;
        TestRead read;
        string input;

        [SetUp]
        public void SetUp()
        {
            print = new TestPrint();
            print.BufferWidth = 10;
            read = new TestRead(null);
            read.NextKeys = new char[] { 'W', 'o', 'r', 'd', '\r' };
            input = Editor.GetInput(print, read);
        }

        [Test]
        public void TestCharsWereInsertedIntoBuffer()
        {
            Assert.AreEqual("Word", input);
        }

        [Test]
        public void TestCharsWereEchoedOnScreen()
        {
            string expected = "Word<0^0\n";
            Assert.AreEqual(expected, print.Output);
        }
    }

    [TestFixture]
    public class InsertWordAndBackspaceTests
    {
        TestPrint print;
        TestRead read;
        string input;

        [SetUp]
        public void SetUp()
        {
            print = new TestPrint();
            print.BufferWidth = 10;
            read = new TestRead(null);
            read.NextKeys = new char[] { 'W', 'o', 'r', 'd', '\b', '\r' };
            input = Editor.GetInput(print, read);
        }

        [Test]
        public void TestBackspace()
        {
            string expected = "Word<3^0 <3^0<0^0\n";
            Assert.AreEqual(expected, print.Output);
        }
    }
}