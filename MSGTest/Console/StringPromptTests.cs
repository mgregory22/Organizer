using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSG.Console;
using MSGTest.IO;
using System;
using System.Collections.Generic;

namespace MSGTest.Console
{
    [TestClass]
    public class StringPromptTests
    {
        StringPrompt testPrompt;
        string testPromptMsg = "> ";
        TestPrint testPrint;
        TestRead testRead;
        string testString = "This is wonderful";
        [TestInitialize]
        public void Initialize()
        {
            testPrint = new TestPrint();
            testRead = new TestRead();
            testPrompt = new StringPrompt(testPrint, testPromptMsg, testRead);
        }
        [TestMethod]
        public void TestGetsString()
        {
            testRead.NextString = testString;
            string gotString = testPrompt.DoPrompt();
            Assert.AreEqual(testString, gotString);
        }
    }
}
