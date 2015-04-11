using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSG.Console;
using MSGTest.IO;
using System;
using System.Collections.Generic;

namespace MSGTest.Console
{

    [TestClass]
    public class KeyPromptTests
    {
        KeyPrompt testPrompt;
        string testPromptMsg = "> ";
        TestPrint testPrint;
        TestRead testRead;
        [TestInitialize]
        public void Initialize()
        {
            testPrint = new TestPrint();
            testRead = new TestRead();
            testPrompt = new KeyPrompt(testPrint, testPromptMsg, testRead);
            testPrompt.ValidList = new char[] { 'a', 'b' };
        }
        [TestMethod]
        public void TestKeyPromptValidatesKey()
        {
            char testKey = 'a';
            testRead.NextKey = testKey;
            char gotKey = testPrompt.DoPrompt();
            Assert.AreEqual(testPromptMsg + "\n", testPrint.Output);
            Assert.AreEqual(testKey, gotKey);

            testRead.NextKeys = new char[] { 'A', 'a' };
            gotKey = testPrompt.DoPrompt();
            Assert.AreEqual(testPromptMsg + "\n"
                    + "Invalid selection. Try again.\n"
                    + testPromptMsg + "\n"
                , testPrint.Output
            );
            Assert.AreEqual(testKey, gotKey);
        }
    }
}
