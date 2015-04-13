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
        string newTask = "This is a task to be added";

        [TestInitialize]
        public void Initialize()
        {
            print = new TestPrint();
            read = new TestRead();
            tasks = new TestTasks();
            addTask = new AddTask(print, read, tasks);
            // The AddTask command prompts the user for the name of the task to add
            read.NextString = newTask;
            addTask.Do();
            // Set up the task.TaskExists() method to claim that the task has been added
            tasks.taskExists_nextReturn = true;
        }
        [TestMethod]
        public void TestPrompt()
        {
            Assert.AreEqual("Enter task name\n> ", print.Output);
        }
        [TestMethod]
        public void TestDoCallsTasksAdd()
        {
            // The tasks.Add() method should be called once
            Assert.AreEqual(1, tasks.addCnt);
            // newTask should passed in the name parameter to the tasks.Add() method
            Assert.AreEqual(newTask, tasks.add_name);
            // 0 (default) should be the parent parameter
            Assert.AreEqual(0, tasks.add_parent);
            // 1 (default) should be the priority parameter
            Assert.AreEqual(1, tasks.add_priority);
        }
        [TestMethod]
        public void TestUndoCallsTasksRemove()
        {
            addTask.Undo();
            // Assert the task was removed
            Assert.AreEqual(1, tasks.removeCnt);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRedoSomethingNotUndoneThrowsUp()
        {
            // Try to redo without undoing first
            addTask.Redo();
        }
        [TestMethod]
        public void TestRedoCallsTasksAdd()
        {
            addTask.Undo();
            // Set up the task.TaskExists() method to claim that the adding of the task has been undone
            tasks.taskExists_nextReturn = false;
            addTask.Redo();
            // Assert the task was added twice
            Assert.AreEqual(2, tasks.addCnt);
        }
    }
    [TestClass]
    public class NoAddTaskTests
    {
        AddTask addTask;
        TestPrint print;
        TestRead read;
        TestTasks tasks;

        [TestInitialize]
        public void Initialize()
        {
            print = new TestPrint();
            read = new TestRead();
            tasks = new TestTasks();
            addTask = new AddTask(print, read, tasks);
            // Set up the task.TaskExists() method to claim that the task doesn't exist
            tasks.taskExists_nextReturn = false;
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
    }
}
