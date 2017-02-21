//
// PrioritiesTest/DlgCmds/ListTasksDialogTests.cs
//

using System.Collections.Generic;
using MSG.IO;
using MSGTest.IO;
using NUnit.Framework;
using Priorities.Modules.Tasks;
using Priorities.Modules.Tasks.DlgCmds;

namespace PrioritiesTest.DlgCmds
{
    [TestFixture]
    public class ListTasksDialogTests
    {
        ListTasksDlgCmd listTasksDlgCmd;
        TestPrint print;
        TestRead read;
        Io io;
        TestTasks tasks;

        List<Task> testTasks = new List<Task> {
            new Task("Test task 1"),
            new Task("Test task 2"),
            new Task("Test task 3")
        };

        [SetUp]
        public void Initialize()
        {
            print = new TestPrint();
            read = new TestRead();
            io = new Io(print, read);
            tasks = new TestTasks();
            listTasksDlgCmd = new ListTasksDlgCmd(io, tasks);
            tasks.enumerator_collection = testTasks;
        }

        [Test]
        public void TestListTasksEnumeratesAndPrintsTasks()
        {
            listTasksDlgCmd.Do(io);
            Assert.AreEqual(
                "[1] " + testTasks[0].Name + "\n"
                + "[2] " + testTasks[1].Name + "\n"
                + "[3] " + testTasks[2].Name + "\n"
                , print.Output
            );
        }
    }
}
