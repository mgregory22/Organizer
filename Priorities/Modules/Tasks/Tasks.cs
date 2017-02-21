//
// Priorities/Modules/Tasks/Tasks.cs
//

using System;
using System.Collections;
using MSG.Types.Dir;

namespace Priorities.Modules.Tasks
{
    public class Tasks : IEnumerable
    {
        protected Dir<Task> tasksRoot;
        protected Dir<Task> currentTasks;

        public Tasks()
        {
            this.tasksRoot = new Dir<Task>();
            this.currentTasks = tasksRoot;
        }

        /// <summary>
        /// Get task from current tasks by index
        /// </summary>
        /// <param name="index">0-based item number</param>
        /// <returns>The task</returns>
        public virtual Task this[int index] {
            get {
                return currentTasks[index];
            }
            set {
                currentTasks[index] = value;
            }
        }

        public virtual void Add(Task task)
        {
            currentTasks.Add(task);
        }

        public int Count {
            get { return currentTasks.Count; }
        }

        public void DownDir(int index)
        {
            if (!ItemExistsAt(index)) {
                throw new InvalidOperationException(string.Format("Item does not exist at {0}", index));
            }
            if (!HasSubdirAt(index)) {
                SetSubdir(index, new Dir<Task>());
            }
            currentTasks = GetSubdir(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return currentTasks.GetEnumerator();
        }

        public virtual Dir<Task> GetSubdir(int index)
        {
            return currentTasks.GetSubdir(index);
        }

        public bool HasSubdirAt(int index)
        {
            return currentTasks.HasSubdirAt(index);
        }

        public virtual void Insert(int index, Task task)
        {
            currentTasks.Insert(index, task);
        }

        public virtual bool ItemExists(Task task)
        {
            return currentTasks.ItemExists(task);
        }

        public virtual bool ItemExistsAt(int index)
        {
            return currentTasks.ItemExistsAt(index);
        }

        public virtual void Move(int srcIndex, int destIndex)
        {
            currentTasks.Move(srcIndex, destIndex);
        }

        public Dir<Task> Parent {
            get {
                return currentTasks.Parent;
            }
            set {
                currentTasks.Parent = value;
            }
        }

        public virtual void Remove(Task task)
        {
            currentTasks.Remove(task);
        }

        public void RemoveAt(int index)
        {
            currentTasks.RemoveAt(index);
        }

        public virtual void SetSubdir(int index, Dir<Task> subdir)
        {
            currentTasks.SetSubdir(index, subdir);
        }

        public void UpDir()
        {
            if (currentTasks != tasksRoot) {
                this.currentTasks = currentTasks.Parent;
            }
        }
    }
}
