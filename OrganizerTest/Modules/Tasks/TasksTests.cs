//
// OrganizerTest/TasksTests.cs
//

using MSG.Types.Dir;
using NUnit.Framework;
using Organizer.Modules.Tasks;

namespace OrganizerTest
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
            MemDir<Task> tasksRoot = new MemDir<Task>();
            tasks = new Tasks(tasksRoot);
        }

        [Test]
        public void TestTaskCanBeAdded()
        {
            int cnt = tasks.Count;
            Task testTask = new Task();
            tasks.Add(taskName, testTask);
            Assert.AreEqual(cnt + 1, tasks.Count);
            Assert.AreEqual(true, tasks.ItemExists(testTask));
        }
    }
}
