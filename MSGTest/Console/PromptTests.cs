using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSG.Console;
using MSG.IO;
using MSGTest.IO;
using System;

namespace MSGTest.Console
{
    [TestClass]
    public class PromptTests
    {
        Prompt testPrompt;
        string testPromptMsg = "> ";
        TestPrint testPrint;
        TestRead testRead;
        [TestInitialize]
        public void Initialize()
        {
            testPrint = new TestPrint();
            testRead = new TestRead();
            testPrompt = new KeyPrompt(testPrint, testPromptMsg, testRead);
        }
        [TestMethod]
        public void TestKeyPromptStoresPrint()
        {
            Assert.AreEqual(testPrint, testPrompt.Print);
        }
        [TestMethod]
        public void TestKeyPromptStoresPrompt()
        {
            Assert.AreEqual(testPromptMsg, testPrompt.PromptMsg);
        }
        [TestMethod]
        public void TestKeyPromptStoresRead()
        {
            Assert.AreEqual(testRead, testPrompt.Read);
        }
    }
}
