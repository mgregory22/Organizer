using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSG.IO;
using Priorities;
using System;

namespace PrioritiesTest
{
    [TestClass]
    public class DriverTests
    {
        Print print;
        Read read;

        [TestInitialize]
        public void Initialize()
        {
            print = new Print();
            read = new Read();
        }
        [TestMethod]
        public void TestProgramRunsSuccessfullyWithNoArguments()
        {
            Driver driver = new Driver(print, read);
        }
        [TestMethod]
        public void TestProgramMenuDisplays()
        {
            Assert.Fail();            
        }
    }
}
