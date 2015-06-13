using NUnit.Framework;
using MSG.Console;
using MSG.IO;
using MSGTest.IO;
using System;

namespace MSGTest.Console
{
    [TestFixture]
    public class PromptTests
    {
        Prompt testPrompt;
        string testPromptMsg = "> ";
        TestPrint print;
        TestRead read;
        [SetUp]
        public void Initialize()
        {
            print = new TestPrint();
            read = new TestRead(print);
            testPrompt = new CharPrompt(print, testPromptMsg, read);
        }
        [Test]
        public void TestKeyPromptStoresPrint()
        {
            Assert.AreEqual(print, testPrompt.Print);
        }
        [Test]
        public void TestKeyPromptStoresPrompt()
        {
            Assert.AreEqual(testPromptMsg, testPrompt.PromptMsg);
        }
        [Test]
        public void TestKeyPromptStoresRead()
        {
            Assert.AreEqual(read, testPrompt.Read);
        }
    }
}
