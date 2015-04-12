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
        List<Task> tasks;
        public Tasks()
        {
            this.tasks = new List<Task>();
        }
        public virtual void Add(string name, int parent = 0, int priority = 1)
        {
            if (name == null)
                throw new ArgumentNullException("Cannot add task without a name");
            if (name == "")
                throw new ArgumentException("Cannot add task without a name");
            if (TaskExists(name))
                throw new InvalidOperationException("Cannot add duplicate task");
            tasks.Add(new Task(name, parent, priority));
        }
        public int Count
        {
            get { return tasks.Count; }
        }
        public virtual void Remove(string name)
        {
            if (name == null)
                throw new ArgumentNullException("Cannot remove task without its name");
            if (name == "")
                throw new ArgumentException("Cannot remove task without its name");
            if (!TaskExists(name))
                throw new InvalidOperationException("Cannot remove nonexistent task");
            if (tasks.Remove(tasks.Find(task => task.Name == name)) == false)
                throw new ApplicationException("Task \"" + name + "\" could not be removed");
        }
        public virtual bool TaskExists(string name)
        {
            return tasks.Find(task => task.Name == name) != null;
        }
    }
}
