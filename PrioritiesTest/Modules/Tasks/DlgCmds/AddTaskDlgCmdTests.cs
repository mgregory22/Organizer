//
// PrioritiesTest/Modules/Tasks/DlgCmds/AddTaskDlgCmdTests.cs
//

using MSG.Patterns;
using MSG.IO;
using MSGTest.IO;
using NUnit.Framework;
using Priorities.Modules.Tasks.DlgCmds;

namespace PrioritiesTest.Modules.Tasks.DlgCmds
{
    [TestFixture]
    public class AddTaskDlgCmdTests
    {
        AddTaskDlgCmd addTaskDlgCmd;
        TestPrint print;
        TestRead read;
        Io io;
        TestTasks tasks;
        UndoAndRedo undoAndRedo;
        string addedTask = "A";

        [SetUp]
        public void Initialize()
        {
            print = new TestPrint();
            read = new TestRead();
            io = new Io(print, read);
            undoAndRedo = new UndoAndRedo();
            tasks = new TestTasks();
            addTaskDlgCmd = new AddTaskDlgCmd(io, undoAndRedo, tasks);
            // The AddTaskDlgCmd command prompts the user for the name of the task to add
            read.PushString(addedTask + "\r");
            addTaskDlgCmd.Do(io);
        }

        [Test]
        public void TestPrompt()
        {
            Assert.AreEqual(addTaskDlgCmd.NamePrompt, print.Output.Substring(0, addTaskDlgCmd.NamePrompt.Length));
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
            undoAndRedo.Undo();
            // Assert the task was removed
            Assert.AreEqual(1, tasks.removeCnt);
        }

        [Test]
        public void TestRedoCallsTasksAdd()
        {
            undoAndRedo.Undo();
            undoAndRedo.Redo();
            // Assert the task was added twice
            Assert.AreEqual(2, tasks.addCnt);
        }
    }

    [TestFixture]
    public class NoAddTaskDlgCmdTests
    {
        AddTaskDlgCmd addTaskDlgCmd;
        TestTasks tasks;
        Io io;
        UndoAndRedo undoAndRedo;

        [SetUp]
        public void Initialize()
        {
            io = new Io(new TestPrint(), new TestRead());
            tasks = new TestTasks();
            undoAndRedo = new UndoAndRedo();
            addTaskDlgCmd = new AddTaskDlgCmd(io, undoAndRedo, tasks);
        }

        [Test]
        public void TestUndoSomethingNotDoneThrows()
        {
            Assert.IsInstanceOf<Cmd.CantUndo>(undoAndRedo.Undo());
        }
    }
}
