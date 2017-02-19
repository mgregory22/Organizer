//
// PrioritiesTest/DialogCommands/InsertTaskDialogTests.cs
//

using MSG.Patterns;
using MSGTest.IO;
using NUnit.Framework;
using Priorities.Features.Tasks.DialogCommands;

namespace PrioritiesTest.DialogCommands
{
    [TestFixture]
    public class InsertTaskDialogTests
    {
        InsertTaskDialog insertTaskDialog;
        TestPrint print;
        TestRead read;
        TestTasks tasks;
        UndoManager undoManager;
        string insertedTask = "I";

        [SetUp]
        public void Initialize()
        {
            print = new TestPrint();
            read = new TestRead(print);
            undoManager = new UndoManager();
            tasks = new TestTasks();
            insertTaskDialog = new InsertTaskDialog(print, read, undoManager, tasks);
            // The InsertTaskDialog command prompts the user for the name of the task to add
            read.PushString(insertedTask + "\r");
            insertTaskDialog.Do();
        }

        [Test]
        public void TestPrompt()
        {
            Assert.AreEqual(insertTaskDialog.Prompt, print.Output.Substring(0, insertTaskDialog.Prompt.Length));
        }

        [Test]
        public void TestDoCallsTasksInsert()
        {
            // The tasks.Insert() method should be called once
            Assert.AreEqual(1, tasks.insertCnt);
            // newTask should passed in the name parameter to the tasks.Add() method
            Assert.AreEqual(insertedTask, tasks.insert_task.Name);
            // 0 (default) should be the index parameter
            Assert.AreEqual(0, tasks.insert_index);
        }

        [Test]
        public void TestUndoCallsTasksRemove()
        {
            undoManager.Undo();
            // Assert the task was removed
            Assert.AreEqual(1, tasks.removeCnt);
        }

        [Test]
        public void TestRedoCallsTasksInsert()
        {
            undoManager.Undo();
            undoManager.Redo();
            // Assert the task was added twice
            Assert.AreEqual(2, tasks.insertCnt);
        }
    }
    [TestFixture]
    public class NoInsertTaskDialogTests
    {
        InsertTaskDialog insertTaskDialog;
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
            insertTaskDialog = new InsertTaskDialog(print, read, undoManager, tasks);
        }

        [Test]
        public void TestUndoSomethingNotDoneThrowsUp()
        {
            Assert.IsInstanceOf<Command.NothingToUndo>(undoManager.Undo());
        }
    }
}
