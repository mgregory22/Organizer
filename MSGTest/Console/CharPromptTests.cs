//
// MSGTest/Console/CharPromptTests.cs
//

using MSG.Console;
using MSGTest.IO;
using NUnit.Framework;

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
            prompt = new CharPrompt(print, read, promptMsg);
            prompt.ValidList = new char[] { 'a', 'b' };
        }

        [Test]
        public void TestCharPromptInvalidatesInvalidChar()
        {
            char invalidKey = 'X';
            char validKey = 'a';
            // A valid key needs to be sent to terminate the prompt loop
            read.PushChar(invalidKey);
            read.PushChar(validKey);
            char gotKey = prompt.PromptAndInput();
            Assert.AreEqual("> X\n" + CharPrompt.helpMsg + "\n> a\n", print.Output);
            // Might as well test this again
            Assert.AreEqual(validKey, gotKey);
        }

        [Test]
        public void TestCharPromptValidatesValidChar()
        {
            char validKey = 'a';
            read.PushChar(validKey);
            char gotKey = prompt.PromptAndInput();
            Assert.AreEqual(promptMsg + "a\n", print.Output);
            Assert.AreEqual(validKey, gotKey);
            print.ClearOutput();
        }
    }
}
