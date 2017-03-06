//
// Organizer/Modules/Tasks/Tasks.cs
//

using System.Collections;
using MSG.Types.Dir;

namespace Organizer.Modules.Tasks
{
    /// <summary>
    /// Tasks can be initialized with any Dir type.
    /// </summary>
    public class Tasks : IEnumerable
    {
        protected Dir<Task> tasks;

        public Tasks(Dir<Task> tasks)
        {
            this.tasks = tasks;
        }

        public virtual void Add(string name, Task task)
        {
            tasks.Add(name, task);
        }

        public int Count {
            get { return tasks.Count; }
        }

        public virtual void CreateSubdir(int index)
        {
            tasks.CreateSubdir(index);
        }

        public virtual void DeleteSubdir(int index)
        {
            tasks.DeleteSubdir(index);
        }

        public void DownDir(int index)
        {
            tasks.DownDir(index);
        }

        public string GetCurPath()
        {
            return tasks.GetCurPath();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return tasks.GetEnumerator();
        }

        public string GetNameAt(int index)
        {
            return tasks.GetNameAt(index);
        }

        public Task GetItemAt(int index)
        {
            return tasks.GetItemAt(index);
        }

        public bool HasParent()
        {
            return tasks.HasParent();
        }

        public bool HasSubdirAt(int index)
        {
            return tasks.HasSubdirAt(index);
        }

        public virtual void Insert(int index, string name, Task task)
        {
            tasks.Insert(index, name, task);
        }

        public virtual bool ItemExists(Task task)
        {
            return tasks.ItemExists(task);
        }

        public virtual bool ItemExistsAt(int index)
        {
            return tasks.ItemExistsAt(index);
        }

        public virtual void Move(int srcIndex, int destIndex)
        {
            tasks.Move(srcIndex, destIndex);
        }

        public virtual void Remove(Task task)
        {
            tasks.Remove(task);
        }

        public void RemoveAt(int index)
        {
            tasks.RemoveAt(index);
        }

        public void SetNameAt(int index, string name)
        {
            tasks.SetNameAt(index, name);
        }

        public void UpDir()
        {
            tasks.UpDir();
        }
    }
}
