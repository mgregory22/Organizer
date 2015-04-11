using Priorities;
using System;

namespace PrioritiesTest
{
    /// <summary>
    ///   Mock Tasks object for testing.
    /// </summary>
    class TestTasks : Tasks
    {
        public string name;
        public int parent;
        public int priority;
        public override void Add(string name, int parent = 0, int priority = 1)
        {
            this.name = name;
            this.parent = parent;
            this.priority = priority;
        }
    }
}
