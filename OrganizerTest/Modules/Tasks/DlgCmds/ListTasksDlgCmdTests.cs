//
// OrganizerTest/Modules/Tasks/DlgCmds/ListTasksDlgCmdTests.cs
//

using System.Collections.Generic;
using MSG.IO;
using MSG.Types.Dir;
using MSGTest.IO;
using NUnit.Framework;
using Organizer.Modules.Tasks;
using Organizer.Modules.Tasks.DlgCmds;

namespace OrganizerTest.Modules.Tasks.DlgCmds
{
    [TestFixture]
    public class ListTasksDlgCmdTests
    {
        ListTasksDlgCmd listTasksDlgCmd;
        TestPrint print;
        TestRead read;
        Io io;
        TestTasks tasks;
        List<Enumerated<Task>> testTasks;

        [Test]
        public void TestListTasksEnumeratesAndPrintsTasks()
        {
            print = new TestPrint();
            read = new TestRead();
            io = new Io(print, read);
            tasks = new TestTasks();
            listTasksDlgCmd = new ListTasksDlgCmd(io, tasks);
            testTasks = new List<Enumerated<Task>>();
            testTasks.Add(new Enumerated<Task>() { Name = "A", Item = new Task() });
            testTasks.Add(new Enumerated<Task>() { Name = "B", Item = new Task() });
            testTasks.Add(new Enumerated<Task>() { Name = "C", Item = new Task() });
            tasks.enumerator_collection = testTasks;

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
