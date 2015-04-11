using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSGTest.IO;
using Priorities;
using System;

namespace PrioritiesTest.Commands
{
    [TestClass]
    public class AddTaskTests
    {
        TestPrint print;
        TestRead read;
        TestTasks tasks;

        [TestInitialize]
        public void Initialize()
        {
            print = new TestPrint();
            read = new TestRead();
            tasks = new TestTasks();
        }
        [TestMethod]
        public void TestDoAddsTask()
        {
            string testString = "This is a test";
            read.NextString = testString;
            Priorities.Commands.AddTask addTask = new Priorities.Commands.AddTask(print, read, tasks);
            addTask.Do();
            Assert.AreEqual(print.Output, "Enter task name\n> " + testString + "\n");
            Assert.AreEqual(testString, tasks.name);
        }
        [TestMethod]
        public void TestUndoRemovesTask()
        {
            // Just realized that my Command class needs to separate the act of doing a
            // menu command from the act of performing the pure computational command,
            // otherwise if I want to undo the last task added and then redo it, the
            // AddTask command will re-prompt the user for the task information.
            Assert.Fail();
        }
    }
}
