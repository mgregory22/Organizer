﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSGTest.IO;
using Priorities;
using Priorities.Commands;
using System;

namespace PrioritiesTest
{
    [TestClass]
    public class DriverTests
    {
        Driver driver;
        string runMessage;
        TestPrint print;
        TestRead read;

        [TestInitialize]
        public void Initialize()
        {
            print = new TestPrint();
            read = new TestRead();
            driver = new Driver(print, read);
            read.NextKey = 'q';
            runMessage = driver.Run();
        }
        [TestMethod]
        public void TestDriverRunsSuccessfully()
        {
            Assert.AreEqual("", runMessage);
        }
        [TestMethod]
        public void TestProgramMenuDisplays()
        {
            Assert.AreEqual("Main Menu\n---------\n"
                    + "[a] Add Task\n"
                    + "[d] Delete Task\n"
                    + "[l] List Tasks\n"
                    + "[m] Move Task/Change Priority\n"
                    + "[o] Options Menu\n"
                    + "[q] Quit Program\n"
                    + "[r] Rename Task\n"
                    + "[?] Help\n"
                    + "> \n"
                , print.Output
            );
        }
    }
}
