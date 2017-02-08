//
// PrioritiesTest/DialogCommands/AddTaskDialogTests.cs
//

using NUnit.Framework;
using System;
using MSGTest.IO;
using MSG.Patterns;
using Priorities.DialogCommands;

namespace PrioritiesTest.DialogCommands
{
    [TestFixture]
    public class AddTaskDialogTests
    {
        AddTaskDialog addTaskDialog;
        TestPrint print;
        TestRead read;
        TestTasks tasks;
        UndoManager undoManager;
        string newTask = "Add me";

        [SetUp]
        public void Initialize()
        {
            print = new TestPrint();
            read = new TestRead(print);
            undoManager = new UndoManager();
            tasks = new TestTasks();
            addTaskDialog = new AddTaskDialog(print, read, undoManager, tasks);
            // The AddTaskDialog command prompts the user for the name of the task to add
            read.PushString(newTask + "\r");
            addTaskDialog.Do();
            // Set up the task.TaskExists() method to claim that the task has been added
            tasks.taskExists_nextReturn = true;
        }

        [Test]
        public void TestPrompt()
        {
            Assert.AreEqual(addTaskDialog.LastPrompt, print.Output.Substring(0, addTaskDialog.LastPrompt.Length));
        }

        [Test]
        public void TestDoCallsTasksAdd()
        {
            // The tasks.Add() method should be called once
            Assert.AreEqual(1, tasks.addCnt);
            // newTask should passed in the name parameter to the tasks.Add() method
            Assert.AreEqual(newTask, tasks.add_task.Name);
            // 0 (default) should be the position parameter
            Assert.AreEqual(-1, tasks.add_position);
        }

        [Test]
        public void TestUndoCallsTasksRemove()
        {
            undoManager.Undo();
            // Assert the task was removed
            Assert.AreEqual(1, tasks.removeCnt);
        }

        [Test]
        public void TestRedoCallsTasksAdd()
        {
            undoManager.Undo();
            // Set up the task.TaskExists() method to claim that the adding of the task has been undone
            tasks.taskExists_nextReturn = false;
            undoManager.Redo();
            // Assert the task was added twice
            Assert.AreEqual(2, tasks.addCnt);
        }
    }
    [TestFixture]
    public class NoAddTaskDialogTests
    {
        AddTaskDialog addTaskDialog;
        TestPrint print;
        TestRead read;
        TestTasks tasks;
        UndoManager undoManager;

        [SetUp]
        public void Initialize()
        {
            print = new TestPrint();
            read = new TestRead(print);
            tasks = new TestTasks();
            undoManager = new UndoManager();
            addTaskDialog = new AddTaskDialog(print, read, undoManager, tasks);
            // Set up the task.TaskExists() method to claim that the task doesn't exist
            tasks.taskExists_nextReturn = false;
        }

        [Test]
        public void TestUndoSomethingNotDoneThrowsUp()
        {
            Assert.IsInstanceOf<Command.NothingToUndo>(undoManager.Undo());
        }
    }
}
