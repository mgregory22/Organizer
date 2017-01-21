//
// Priorities/Types/Tasks.cs
//

using System;
using System.Collections;
using System.Collections.Generic;

namespace Priorities.Types
{
    public class Tasks : IEnumerable
    {
        List<Task> tasks;

        public Tasks()
        {
            this.tasks = new List<Task>();
        }

        public Task this[int priority]
        {
            // Get task by priority
            get
            {
                return tasks[priority - 1];
            }
            // Replace task with given priority
            set
            {
                tasks[priority - 1] = value;
            }
        }

        /// <summary>
        ///     Adds a new task to the list.  The priority determines where it is
        ///     to be inserted.  1 is first, 2 is second, etc.  0 is last.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        /// <param name="priority"></param>
        virtual public void Add(string name, int parent = 0, int priority = 0)
        {
            if (name == null)
                throw new ArgumentNullException("Cannot add task without a name");
            if (name == "")
                throw new ArgumentException("Cannot add task without a name");
            if (TaskExists(name))
                throw new InvalidOperationException("Cannot add duplicate task");

            Task newTask = new Task(name, parent);
            // priority = 0 means last priority
            if (priority == 0)
            {
                tasks.Add(newTask);
            }
            else
            {
                tasks.Insert(priority - 1, newTask);
            }
        }

        virtual public int Count
        {
            get { return tasks.Count; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return tasks.GetEnumerator();
        }

        virtual public void Remove(string name)
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

        virtual public void Remove(int priority)
        {
            if (!TaskExists(priority))
                throw new InvalidOperationException("Cannot remove nonexistent task");
            tasks.RemoveAt(priority - 1);
        }

        virtual public bool TaskExists(string name)
        {
            return tasks.Find(task => task.Name == name) != null;
        }

        virtual public bool TaskExists(int priority)
        {
            return priority >= 1 && priority <= tasks.Count;
        }
    }
}
