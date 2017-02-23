//
// PrioritiesTest/DriverTests.cs
//

using MSG.Console;
using MSG.IO;
using MSGTest.IO;
using NUnit.Framework;
using Priorities;

namespace PrioritiesTest
{
    [TestFixture]
    public class DriverTests
    {
        TestPrint print;
        TestRead read;
        Io io;

        [SetUp]
        public void SetUp()
        {
            print = new TestPrint();
            read = new TestRead();
            io = new Io(print, read);
        }

        [Test]
        public void TestHowToGetHelpDisplays()
        {
            read.PushString("q");
            CharPrompt prompt = new CharPrompt();

            Driver.Run(io, prompt);

            // The menu should display once how to get help
            string start = string.Format("\nMain Menu (? for help)\n{0}q\n", prompt.Prompt);
            Assert.AreEqual(start, print.Output.Substring(0, start.Length));
        }

        [Test]
        public void TestHowToGetHelpDisplaysOnceOnly()
        {
            read.PushString("?q");
            CharPrompt prompt = new CharPrompt();

            Driver.Run(io, prompt);

            // The second Main Menu prompt should not have a help message
            string end = string.Format("\n\nMain Menu\n{0}q\n", prompt.Prompt);
            Assert.AreEqual(end, print.Output.Substring(print.Output.Length - end.Length));
        }
    }
}
