using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSGTest.IO;
using Priorities;
using System;

namespace PrioritiesTest
{
    [TestClass]
    public class AddTaskTests
    {
        TestPrint print;
        TestRead read;

        class TestTasks : Tasks
        {
            public string name;
            public int parent;
            public int priority;
            public override void Add(string name, int parent = 0, int priority = 1)
            {
                this.name = name;
                this.parent = parent;
                this.priority = priority;
            }
        }
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
            Priorities.Commands.AddTask addTask = new Priorities.Commands.AddTask(tasks);
            addTask.Do(print, read);
            Assert.AreEqual(print.Output, "Enter task name\n> " + testString + "\n");
            Assert.AreEqual(testString, tasks.name);
        }
    }
}
