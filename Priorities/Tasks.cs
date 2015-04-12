using System;
using System.Collections.Generic;

namespace Priorities
{
    public class Task
    {
        string name;
        int parent;
        int priority;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public Task(string name, int parent, int priority)
        {
            this.name = name;
            this.parent = parent;
            this.priority = priority;
        }
    }

    public class Tasks
    {
        public const int ErrorCannotAddDuplicate = 1;
        public const int ErrorCannotRemoveNonexistent = 2;
        List<Task> tasks;
        public Tasks()
        {
            this.tasks = new List<Task>();
        }
        public virtual int Add(string name, int parent = 0, int priority = 1)
        {
            if (TaskExists(name))
                return ErrorCannotAddDuplicate;
            tasks.Add(new Task(name, parent, priority));
            return 0;
        }
        public int Count
        {
            get { return tasks.Count; }
        }
        public virtual int Remove(string name)
        {
            if (!TaskExists(name))
                return ErrorCannotRemoveNonexistent;
            return tasks.Remove(tasks.Find(task => task.Name == name)) ? 0 : 1;
        }
        public bool TaskExists(string searchString)
        {
            return tasks.Find(task => task.Name == searchString) != null;
        }
    }
}
