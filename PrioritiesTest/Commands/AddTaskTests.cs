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
        }
        [TestMethod]
        public void TestDoAddsTask()
        {
            read.NextString = testString;
            addTask = new AddTask(print, read, tasks);
            int r = addTask.Do();
            Assert.AreEqual(0, r);
            Assert.AreEqual(print.Output, "Enter task name\n> " + testString + "\n");
            Assert.AreEqual(testString, tasks.name);
        }
        [TestMethod]
        public void TestAddingDuplicateTaskReturnsError()
        {
            // add a task
            read.NextString = testString;
            addTask = new AddTask(print, read, tasks);
            addTask.Do();
            // try to add same task
            read.NextString = testString;
            addTask = new AddTask(print, read, tasks);
            int r = addTask.Do();
            Assert.AreEqual(TestTasks.ErrorCannotAddDuplicate, r);
        }
        [TestMethod]
        public void TestUndoRemovesTask()
        {
            // add a task
            read.NextString = testString;
            addTask = new AddTask(print, read, tasks);
            addTask.Do();
            // undo
            int r = addTask.Undo();
            // assert the task was removed
            Assert.AreEqual(0, r);
            Assert.AreEqual(1, tasks.removeCnt);
        }
        [TestMethod]
        public void TestUndoSomethingNotDoneReturnsError()
        {
            int r = addTask.Undo();
            Assert.AreEqual(AddTask.ErrorCannotUndoSomethingNotDone, r);
        }
        [TestMethod]
        public void TestRedoSomethingNotDoneReturnsError()
        {
            int r = addTask.Redo();
            Assert.AreEqual(AddTask.ErrorCannotRedoSomethingNotDone, r);
        }
        [TestMethod]
        public void TestRedoSomethingNotUndoneReturnsError()
        {
            // add a task
            read.NextString = testString;
            addTask = new AddTask(print, read, tasks);
            addTask.Do();
            // try to redo without undoing first
            int r = addTask.Redo();
            Assert.AreEqual(AddTask.ErrorCannotRedoSomethingNotUndone, r);
        }
    }
}
