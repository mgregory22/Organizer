using Priorities;
using System;

namespace PrioritiesTest
{
    /// <summary>
    ///   Mock Tasks object for testing the task commands.
    /// </summary>
    class TestTasks : Tasks
    {
        public string name;
        public int parent;
        public int priority;
        public int removeCnt;
        public override void Add(string name, int parent = 0, int priority = 1)
        {
            // This object is seeming kind of weird to me, since the task commands
            // are molded around the Tasks class, so I feel like I'm going to have
            // to reimplement the whole Tasks class.

            // if this name has already been stored, it's a duplicate, thus error
            if (name == this.name)
                throw new InvalidOperationException();
            this.name = name;
            this.parent = parent;
            this.priority = priority;
        }
        public override void Remove(string name)
        {
            removeCnt++;
            if (name != this.name)
                throw new InvalidOperationException();
        }
        public override bool TaskExists(string name)
        {
            return name == this.name;
        }
    }
}
