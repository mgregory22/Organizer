//
// PrioritiesTest/TaskCommands/ListTasksTests.cs
//

using MSGTest.IO;
using NUnit.Framework;
using System.Collections.Generic;
using Priorities.TaskCommands;
using Priorities.Types;

namespace PrioritiesTest.TaskCommands
{
    [TestFixture]
    public class ListTasksTests
    {
        ListTasks listTasks;
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
            listTasks = new ListTasks(print, tasks);
            tasks.enumerator_collection = testTasks;
        }

        [Test]
        public void TestListTasksEnumeratesAndPrintsTasks()
        {
            listTasks.Do();
            Assert.AreEqual(
                "[0] " + testTasks[0].Name + "\n"
                + "[1] " + testTasks[1].Name + "\n"
                + "[2] " + testTasks[2].Name + "\n"
                , print.Output
            );
        }
    }
}
