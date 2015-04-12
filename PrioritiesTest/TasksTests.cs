using Microsoft.VisualStudio.TestTools.UnitTesting;
using Priorities;
using System;

namespace PrioritiesTest
{
    /// <summary>
    ///   Tests of the Tasks class.
    /// </summary>
    [TestClass]
    public class TasksTests
    {
        Tasks tasks;
        string testTask = "Test Task";
        [TestInitialize]
        public void Initialize()
        {
            tasks = new Tasks();
        }
        [TestMethod]
        public void TestTaskCanBeAdded()
        {
            int cnt = tasks.Count;
            tasks.Add(testTask);
            Assert.AreEqual(cnt + 1, tasks.Count);
            Assert.AreEqual(true, tasks.TaskExists(testTask));
        }
    }
}
