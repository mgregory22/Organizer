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

        public virtual Task this[int position]
        {
            // Get task by position
            get
            {
                return tasks[position];
            }
            // Replace task with given priority
            set
            {
                tasks[position] = value;
            }
        }

        /// <summary>
        ///     Adds a new task to the list.  The priority determines where it is
        ///     to be inserted.  1 is first, 2 is second, etc.  0 is last.
        /// </summary>
        /// <param name="task">
        ///   Task to add.
        /// </param>
        /// <param name="position">
        ///   1-based position in the list to add.  0 = the end.
        /// </param>
        public virtual void Add(Task task, int position = -1)
        {
            if (TaskExists(task))
                throw new InvalidOperationException("Cannot add same task twice");

            // Position -1 = add to end of list
            if (position == -1)
                tasks.Add(task);
            else
                tasks.Insert(position, task);
        }

        public int Count
        {
            get { return tasks.Count; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return tasks.GetEnumerator();
        }

        public virtual void Move(int srcPosition, int destPosition)
        {
            Task task = tasks[srcPosition];
            tasks.RemoveAt(srcPosition);
            tasks.Insert(destPosition, task);
        }

        public virtual void Remove(Task task)
        {
            if (task == null)
                throw new NullReferenceException("<task> cannot be null in call to Remove(Task)");
            if (tasks.Remove(tasks.Find(t => t == task)) == false)
                throw new ApplicationException("Task \"" + task.Name + "\" could not be removed");
        }

        public virtual void Remove(string name)
        {
            if (name == null)
                throw new ArgumentNullException("<name> cannot be null in call to Remove(string)");
            if (name == "")
                throw new ArgumentException("<name> cannot be empty string in call to Remove(string)");
            if (!TaskExists(name))
                throw new InvalidOperationException("Task \"" + name + "\" does not exist");
            if (tasks.Remove(tasks.Find(task => task.Name == name)) == false)
                throw new ApplicationException("Task \"" + name + "\" could not be removed");
        }

         public virtual void Remove(int position)
        {
            if (!TaskExists(position))
                throw new InvalidOperationException("Cannot remove nonexistent task");
            tasks.RemoveAt(position);
        }

        public virtual bool TaskExists(Task task)
        {
            return tasks.IndexOf(task) > -1;
        }

        public virtual bool TaskExists(string name)
        {
            return tasks.Find(task => task.Name == name) != null;
        }

        public virtual bool TaskExists(int position)
        {
            return position >= 0 && position < tasks.Count;
        }
    }
}
