using NUnit.Framework;
using MSG.Console;
using MSGTest.IO;
using System;
using System.Collections.Generic;

namespace MSGTest.Console
{
    [TestFixture]
    public class StringPromptTests
    {
        StringPrompt testPrompt;
        string testPromptMsg = "> ";
        TestPrint print;
        TestRead read;
        string testString = "This is wonderful";

        [SetUp]
        public void Initialize()
        {
            print = new TestPrint();
            read = new TestRead(print);
            testPrompt = new StringPrompt(print, testPromptMsg, read);
        }

        [Test]
        public void TestGetsString()
        {
            read.PushString(testString);
            string gotString = testPrompt.Loop();
            Assert.AreEqual(testString, gotString);
        }
    }
}
