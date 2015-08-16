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
        Driver driver;
        TestPrint print;
        TestRead read;

        [SetUp]
        public void SetUp()
        {
            print = new TestPrint();
            read = new TestRead(print);
            driver = new Driver(print, read);
        }

        [Test]
        public void TestHelpDisplays()
        {
            read.SetNextKeys(new[] { '?', 'q' });
            driver.Run();
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
                    , Driver.promptMsg)
                , print.Output
            );
        }
    }
}
