using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSGTest.IO;
using System;

namespace PrioritiesTest
{
    [TestClass]
    public class AddTaskTests
    {
        TestPrint print;
        TestRead read;

        [TestInitialize]
        public void Initialize()
        {
            print = new TestPrint();
            read = new TestRead();
        }
        [TestMethod]
        public void TestDoAddsTask()
        {
        }
    }
}
