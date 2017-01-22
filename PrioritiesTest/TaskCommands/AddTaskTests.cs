//
// PrioritiesTest/TaskCommands/AddTaskTests.cs
//

using MSGTest.IO;
using NUnit.Framework;
using Priorities.TaskCommands;
using System;

namespace PrioritiesTest.TaskCommands
{
    [TestFixture]
    public class AddTaskTests
    {
        AddTask addTask;
        TestPrint print;
        TestRead read;
        TestTasks tasks;
        string newTask = "This is a task to be added";

        [SetUp]
        public void Initialize()
        {
            print = new TestPrint();
            read = new TestRead(print);
            tasks = new TestTasks();
            addTask = new AddTask(print, read, tasks);
            // The AddTask command prompts the user for the name of the task to add
            read.PushString(newTask + "\r");
            addTask.Do();
            // Set up the task.TaskExists() method to claim that the task has been added
            tasks.taskExists_nextReturn = true;
        }

        [Test]
        public void TestPrompt()
        {
            Assert.AreEqual(addTask.LastPrompt, print.Output.Substring(0, addTask.LastPrompt.Length));
        }

        [Test]
        public void TestDoCallsTasksAdd()
        {
            // The tasks.Add() method should be called once
            Assert.AreEqual(1, tasks.addCnt);
            // newTask should passed in the name parameter to the tasks.Add() method
            Assert.AreEqual(newTask, tasks.add_name);
            // 0 (default) should be the priority parameter
            Assert.AreEqual(0, tasks.add_priority);
        }

        [Test]
        public void TestUndoCallsTasksRemove()
        {
            addTask.Undo();
            // Assert the task was removed
            Assert.AreEqual(1, tasks.removeCnt);
        }

        [Test]
        public void TestRedoSomethingNotUndoneThrowsUp()
        {
            // Try to redo without undoing first
            Assert.Catch<InvalidOperationException>(() => addTask.Redo());
        }

        [Test]
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
    [TestFixture]
    public class NoAddTaskTests
    {
        AddTask addTask;
        TestPrint print;
        TestRead read;
        TestTasks tasks;

        [SetUp]
        public void Initialize()
        {
            print = new TestPrint();
            read = new TestRead(print);
            tasks = new TestTasks();
            addTask = new AddTask(print, read, tasks);
            // Set up the task.TaskExists() method to claim that the task doesn't exist
            tasks.taskExists_nextReturn = false;
        }

        [Test]
        public void TestUndoSomethingNotDoneThrowsUp()
        {
            Assert.Catch<InvalidOperationException>(() => addTask.Undo());
        }

        [Test]
        public void TestRedoSomethingNotDoneThrowsUp()
        {
            Assert.Catch<InvalidOperationException>(() => addTask.Redo());
        }
    }
}
