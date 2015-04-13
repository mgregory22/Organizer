using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSGTest.IO;
using Priorities;
using Priorities.Commands;
using System;
using System.Collections.Generic;

namespace PrioritiesTest.Commands
{
    [TestClass]
    public class ListTasksTests
    {
        ListTasks listTasks;
        TestPrint print;
        TestRead read;
        TestTasks tasks;
        List<Task> testTasks = new List<Task> {
            new Task("Test task 1", 0, 1),
            new Task("Test task 2", 0, 1),
            new Task("Test task 3", 0, 1)
        };
        [TestInitialize]
        public void Initialize()
        {
            print = new TestPrint();
            read = new TestRead();
            tasks = new TestTasks();
            listTasks = new ListTasks(print, tasks);
            tasks.enumerator_collection = testTasks;
        }
        [TestMethod]
        public void TestListTasksEnumeratesAndPrintsTasks()
        {
            listTasks.Do();
            Assert.AreEqual(
                testTasks[0].Name + "\n" + testTasks[1].Name + "\n" + testTasks[2].Name + "\n"
                , print.Output
            );
        }
    }
}
