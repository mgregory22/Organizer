//
// PrioritiesTest/DriverTests.cs
//

using MSG.Console;
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

        [SetUp]
        public void SetUp()
        {
            print = new TestPrint();
            read = new TestRead(print);
        }

        [Test]
        public void TestHelpDisplays()
        {
            read.PushString("?q");
            CharPrompt prompt = new CharPrompt(print, read);
            Driver.Run(prompt);

            // The menu should display its title every time, and
            // if the help key is pressed, it should display bracketed
            // descriptions of menu items which start with bracketed
            // keystrokes.
            string start = string.Format("\nMain Menu\n{0}?\n[", prompt.Prompt);
            Assert.AreEqual(start, print.Output.Substring(0, start.Length));

            // The help display should end with a help menu item
            // followed by a new Main Menu prompt.
            string end = string.Format("[?] Help\n\nMain Menu\n{0}q\n", prompt.Prompt);
            Assert.AreEqual(end, print.Output.Substring(print.Output.Length - end.Length));
        }
    }
}
