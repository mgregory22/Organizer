//
// PrioritiesTest/TasksTests.cs
//

using NUnit.Framework;
using Priorities.Types;

namespace PrioritiesTest
{
    /// <summary>
    ///   Tests of the Tasks class.
    /// </summary>
    [TestFixture]
    public class TasksTests
    {
        Tasks tasks;
        string testTask = "Test Task";

        [SetUp]
        public void Initialize()
        {
            tasks = new Tasks();
        }

        [Test]
        public void TestTaskCanBeAdded()
        {
            int cnt = tasks.Count;
            tasks.Add(testTask);
            Assert.AreEqual(cnt + 1, tasks.Count);
            Assert.AreEqual(true, tasks.TaskExists(testTask));
        }
    }
}
