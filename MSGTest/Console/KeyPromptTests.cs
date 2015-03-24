using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSG.Console;
using System;
using System.Collections.Generic;

namespace MSGTest.Console
{
    class KeyPromptTestRead : Read
    {
        char? nextKey;
        List<char> nextKeys;
        public override char Key()
        {
            if (nextKey != null)
            {
                char c = nextKey.GetValueOrDefault();
                nextKey = null;
                return c;
            }
            else if (nextKeys != null && nextKeys.Count > 0)
            {
                char c = nextKeys[0];
                nextKeys.RemoveAt(0);
                return c;
            }
            return ' ';
        }
        public char NextKey
        {
            get { return nextKey.GetValueOrDefault(); }
            set { nextKey = value; }
        }
        public char[] NextKeys
        {
            get { return nextKeys.ToArray(); }
            set { nextKeys = new List<char>(value); }
        }
    }

    [TestClass]
    public class KeyPromptTests
    {
        KeyPrompt testPrompt;
        string testPromptMsg = Priorities.Program.promptMsg;
        TestPrint testPrint;
        KeyPromptTestRead testRead;
        [TestInitialize]
        public void Initialize()
        {
            testPrint = new TestPrint();
            testRead = new KeyPromptTestRead();
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
