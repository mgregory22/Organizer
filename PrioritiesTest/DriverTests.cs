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
            Driver.Run(print, read, prompt);
            Assert.AreEqual(
                string.Format("{0}?\n"
                        + "Main Menu\n---------\n"
                        + "[a] Add Task\n"
                        + "[d] Delete Task\n"
                        + "[l] List Tasks\n"
                        + "[m] Move Task/Change Priority\n"
                        + "[o] Options Menu\n"
                        + "[q] Quit Program\n"
                        + "[r] Rename Task\n"
                        + "[?] Help\n\n"
                        + "{0}q\n\n"
                    , prompt.LastPrompt)
                , print.Output
            );
        }
    }
}
