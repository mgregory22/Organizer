//
// PrioritiesTest/TasksTests.cs
//

using NUnit.Framework;
using Priorities.Modules.Tasks;

namespace PrioritiesTest
{
    /// <summary>
    /// Tests of the Tasks class.
    /// </summary>
    [TestFixture]
    public class TasksTests
    {
        Tasks tasks;
        string taskName = "Test Task";

        [SetUp]
        public void Initialize()
        {
            tasks = new Tasks();
        }

        [Test]
        public void TestTaskCanBeAdded()
        {
            int cnt = tasks.Count;
            Task testTask = new Task(taskName);
            tasks.Add(testTask);
            Assert.AreEqual(cnt + 1, tasks.Count);
            Assert.AreEqual(true, tasks.ItemExists(testTask));
        }
    }
}
