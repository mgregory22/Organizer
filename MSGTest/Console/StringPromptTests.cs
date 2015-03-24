using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSG.Console;
using System;
using System.Collections.Generic;

namespace MSGTest.Console
{
    class StringPromptTestRead : Read
    {
        string nextString;
        List<string> nextStrings;
        public override string String()
        {
            if (nextString != null)
            {
                string s = nextString;
                nextString = null;
                return s;
            }
            else if (nextStrings != null && nextStrings.Count > 0)
            {
                string s = nextStrings[0];
                nextStrings.RemoveAt(0);
                return s;
            }
            return "";
        }
        public string NextString
        {
            get { return nextString; }
            set { nextString = value; }
        }
        public string[] NextStrings
        {
            get { return nextStrings.ToArray(); }
            set { nextStrings = new List<string>(value); }
        }
    }

    [TestClass]
    public class StringPromptTests
    {
        StringPrompt testPrompt;
        string testPromptMsg = Priorities.Program.promptMsg;
        TestPrint testPrint;
        StringPromptTestRead testRead;
        string testString = "This is wonderful";
        [TestInitialize]
        public void Initialize()
        {
            testPrint = new TestPrint();
            testRead = new StringPromptTestRead();
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
