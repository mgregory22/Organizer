using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSG.Console;
using System;

namespace PrioritiesTest
{
    class TestPrint : Print
    {
        string output = "";
        public override void Char(char c, bool nl = false)
        {
            Output += c;
            if (nl) Output += '\n';
        }
        public string Output
        {
            get { string r = output; output = ""; return r; }
            set { output = value; }
        }
        public override void String(string s, bool nl = false)
        {
            Output += s;
            if (nl) Output += '\n';
        }
    }

    class TestRead : Read
    {
        char nextKey;
        public override char Key()
        {
            return NextKey;
        }
        public char NextKey
        {
            get { return nextKey; }
            set { nextKey = value; }
        }
        public TestRead()
        {
            nextKey = 'a';
        }
    }

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
        public void TestKeyPromptStoresPrompt()
        {
            Assert.AreEqual(testPromptMsg, testPrompt.PromptMsg);
        }

        [TestMethod]
        public void TestKeyPromptValidatesKey()
        {
            testRead.NextKey = 'a';
            testPrompt.DoPrompt();
            Assert.AreEqual("> a\n", testPrint.Output);
            testRead.NextKey = 'b';
            testPrompt.DoPrompt();
            Assert.AreEqual("> b\n", testPrint.Output);
            testRead.NextKey = 'c';
            testPrompt.DoPrompt();
            Assert.AreNotEqual("> c\n", testPrint.Output);
        }
    }
}
