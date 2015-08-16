using MSGTest.IO;
using NUnit.Framework;
using Priorities;
using Priorities.Commands;
using System;

namespace PrioritiesTest
{
    [TestFixture]
    public class DriverTests
    {
        TestPrint print;
        TestRead read;
        string promptMsg = "! ";

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
            Driver.Run(print, "! ", read);
            Assert.AreEqual(
                string.Format("{0}\n"
                        + "Main Menu\n---------\n"
                        + "[a] Add Task\n"
                        + "[d] Delete Task\n"
                        + "[l] List Tasks\n"
                        + "[m] Move Task/Change Priority\n"
                        + "[o] Options Menu\n"
                        + "[q] Quit Program\n"
                        + "[r] Rename Task\n"
                        + "[?] Help\n\n"
                        + "{0}\n\n"
                    , promptMsg)
                , print.Output
            );
        }
    }
}
