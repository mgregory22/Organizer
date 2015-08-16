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
            read.SetNextString(testString);
            string gotString = testPrompt.DoPrompt();
            Assert.AreEqual(testString, gotString);
        }
    }
}
