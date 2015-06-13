using MSG.Console;
using MSGTest.IO;
using NUnit.Framework;
using System;

namespace MSGTest.Console
{

    [TestFixture]
    public class CharPromptTests
    {
        CharPrompt prompt;
        string promptMsg = "> ";
        TestPrint print;
        TestRead read;

        [SetUp]
        public void SetUp()
        {
            print = new TestPrint();
            read = new TestRead(print);
            prompt = new CharPrompt(print, promptMsg, read);
            prompt.ValidList = new char[] { 'a', 'b' };
        }

        [Test]
        public void TestCharPromptValidatesValidChar()
        {
            char validKey = 'a';
            read.NextKey = validKey;
            char gotKey = prompt.DoPrompt();
            Assert.AreEqual(promptMsg + "\n", print.Output);
            Assert.AreEqual(validKey, gotKey);
            print.ClearOutput();
        }

        [Test]
        public void TestCharPromptInvalidatesInvalidChar()
        {
            char invalidKey = 'X';
            char validKey = 'a';
            // A valid key needs to be sent to terminate the prompt loop
            read.NextKeys = new char[] { invalidKey, validKey };
            char gotKey = prompt.DoPrompt();
            Assert.AreEqual("> \nInvalid selection. Try again.\n> \n", print.Output);
            // Might as well test this again
            Assert.AreEqual(validKey, gotKey);
        }
    }
}
