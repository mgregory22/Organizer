//
// PrioritiesTest/DialogCommands/AddTaskDialogTests.cs
//

using MSG.Patterns;
using MSGTest.IO;
using NUnit.Framework;
using Priorities.Features.Tasks.DialogCommands;

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
        string addedTask = "A";

        [SetUp]
        public void Initialize()
        {
            print = new TestPrint();
            read = new TestRead(print);
            undoManager = new UndoManager();
            tasks = new TestTasks();
            addTaskDialog = new AddTaskDialog(print, read, undoManager, tasks);
            // The AddTaskDialog command prompts the user for the name of the task to add
            read.PushString(addedTask + "\r");
            addTaskDialog.Do();
        }

        [Test]
        public void TestPrompt()
        {
            Assert.AreEqual(addTaskDialog.NamePrompt, print.Output.Substring(0, addTaskDialog.NamePrompt.Length));
        }

        [Test]
        public void TestDoCallsTasksAdd()
        {
            // The tasks.Add() method should be called once
            Assert.AreEqual(1, tasks.addCnt);
            // newTask should passed in the name parameter to the tasks.Add() method
            Assert.AreEqual(addedTask, tasks.add_task.Name);
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
        }

        [Test]
        public void TestUndoSomethingNotDoneThrowsUp()
        {
            Assert.IsInstanceOf<Command.NothingToUndo>(undoManager.Undo());
        }
    }
}
