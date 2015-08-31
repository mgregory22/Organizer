//
// PrioritiesTest/Commands/ListTasksTests.cs
//

using MSGTest.IO;
using NUnit.Framework;
using Priorities;
using Priorities.Commands;
using System.Collections.Generic;

namespace PrioritiesTest.Commands
{
    [TestFixture]
    public class ListTasksTests
    {
        ListTasks listTasks;
        TestPrint print;
        TestRead read;
        TestTasks tasks;

        List<Task> testTasks = new List<Task> {
            new Task("Test task 1", 0, 1),
            new Task("Test task 2", 0, 1),
            new Task("Test task 3", 0, 1)
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
