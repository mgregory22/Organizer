//
// PrioritiesTest/DialogCommands/ListTasksDialogTests.cs
//

using MSGTest.IO;
using NUnit.Framework;
using System.Collections.Generic;
using Priorities.DialogCommands;
using Priorities.Types;

namespace PrioritiesTest.DialogCommands
{
    [TestFixture]
    public class ListTasksDialogTests
    {
        ListTasksDialog listTasksDialog;
        TestPrint print;
        TestRead read;
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
            read = new TestRead(print);
            tasks = new TestTasks();
            listTasksDialog = new ListTasksDialog(print, tasks);
            tasks.enumerator_collection = testTasks;
        }

        [Test]
        public void TestListTasksEnumeratesAndPrintsTasks()
        {
            listTasksDialog.Do();
            Assert.AreEqual(
                "[1] " + testTasks[0].Name + "\n"
                + "[2] " + testTasks[1].Name + "\n"
                + "[3] " + testTasks[2].Name + "\n"
                , print.Output
            );
        }
    }
}
