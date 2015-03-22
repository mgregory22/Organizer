using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSG.Console;
using System;

namespace MSGTest.Console
{
    class TestPrint : Print
    {
        string output;
        public override void Char(char c, bool nl = false)
        {
            output += c;
            if (nl) output += '\n';
        }
        public override void Newline(int n = 1)
        {
            for (int i = 0; i < n; i++) output += '\n';
        }
        public string Output
        {
            get { string r = output; output = ""; return r; }
            set { output = value; }
        }
        public override void String(string s, bool nl = false)
        {
            output += s;
            if (nl) output += '\n';
        }
    }
    class TestRead : Read
    {
        public override char Key()
        {
            return ' ';
        }
    }
    [TestClass]
    public class PromptTests
    {
        Prompt testPrompt;
        string testPromptMsg = Priorities.Program.promptMsg;
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
