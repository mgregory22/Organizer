using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSGTest.IO;
using Priorities.Commands;
using System;

namespace PrioritiesTest.Commands
{
    [TestClass]
    public class AddTaskTests
    {
        AddTask addTask;
        TestPrint print;
        TestRead read;
        TestTasks tasks;
        string testString = "This is a test";

        [TestInitialize]
        public void Initialize()
        {
            print = new TestPrint();
            read = new TestRead();
            tasks = new TestTasks();
            addTask = new AddTask(print, read, tasks);
        }
        [TestMethod]
        public void TestDoCallsTasksAdd()
        {
            read.NextString = testString;
            addTask.Do();
            Assert.AreEqual("Enter task name\n> ", print.Output);
            Assert.AreEqual(testString, tasks.name);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestAddingDuplicateTaskThrowsUp()
        {
            // add a task
            read.NextString = testString;
            addTask.Do();
            // try to add same task
            read.NextString = testString;
            addTask.Do();
        }
        [TestMethod]
        public void TestUndoCallsTasksRemove()
        {
            // add a task
            read.NextString = testString;
            addTask.Do();
            // undo
            addTask.Undo();
            // assert the task was removed
            Assert.AreEqual(1, tasks.removeCnt);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestUndoSomethingNotDoneThrowsUp()
        {
            addTask.Undo();
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRedoSomethingNotDoneThrowsUp()
        {
            addTask.Redo();
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRedoSomethingNotUndoneThrowsUp()
        {
            // add a task
            read.NextString = testString;
            addTask.Do();
            // try to redo without undoing first
            addTask.Redo();
        }
    }
}
