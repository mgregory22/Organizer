using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Priorities
{
    class Tasks
    {
        /// <summary>
        ///   The list of to-do tasks.  The position of a task in the list determines its priority.
        /// </summary>
        private List<string> _tasks;
        /// <summary>
        ///   Adds a task with the given priority.  If priority is omitted or 0, then the new task is
        ///   given the lowest priority.
        /// </summary>
        /// <param name="task">
        ///   Task description.
        /// </param>
        /// <param name="priority">
        ///   Task priority.
        /// </param>
        public void Add(string task, int priority = 1)
        {
            if (priority > Count + 1) priority = Count + 1;
            if (priority < 1) priority = 1;
            _tasks.Insert(priority - 1, task);
        }
        /// <summary>
        ///   Removes all tasks.
        /// </summary>
        public void Clear()
        {
            _tasks.Clear();
        }
        /// <summary>
        ///   The number of tasks.
        /// </summary>
        public int Count
        {
            get
            {
                return _tasks.Count;
            }
        }
        /// <summary>
        ///   Deletes the task with the given priority.
        /// </summary>
        /// <param name="priority"></param>
        public void Delete(int priority)
        {
            _tasks.RemoveAt(priority - 1);
        }
        /// <summary>
        ///   The type of method that <see cref="ForEach"/> calls on each task.
        /// </summary>
        /// <param name="task">
        ///   Task description.
        /// </param>
        /// <param name="priority">
        ///   Higher priority items come first.
        /// </param>
        /// <returns>
        ///   Returning false will abort the iteration.
        /// </returns>
        public delegate bool EachFunc(string task, int priority);
        /// <summary>
        ///   Calls given <see cref="EachFunc"/> method on each task in priority order (highest first).
        /// </summary>
        /// <param name="each"></param>
        /// <returns></returns>
        public bool ForEach(EachFunc each)
        {
            for (int priority = Count; priority > 0; priority--)
            {
                if (!each(_tasks[priority - 1], priority)) return false;
            }
            return true;
        }
        /// <summary>
        ///   Returns the task with the given priority.
        /// </summary>
        /// <param name="priority">
        ///   Priority of the task to get.
        /// </param>
        /// <returns>
        ///   The task description.
        /// </returns>
        public string Get(int priority)
        {
            return _tasks.ElementAt(priority - 1);
        }
        /// <summary>
        ///   Loads the list of to-do tasks from tasks.txt.
        /// </summary>
        void FileLoad(string filename)
        {
            var sr = new StreamReader(filename);
            if (sr == null) return;
            Clear();
            string task;
            while ((task = sr.ReadLine()) != null)
            {
                Add(task);
            }
            sr.Close();
        }
        /// <summary>
        ///   Saves the list of to-do tasks to the file named in _filename.
        /// </summary>
        void FileSave(string filename)
        {
            var sw = new StreamWriter(filename);
            ForEach((string task, int priority) =>
            {
                sw.WriteLine(task);
                return true;
            });
            sw.Close();
        }
        /// <summary>
        ///   Changes the priority of a task.
        /// </summary>
        /// <param name="priorityTaskSrc">
        ///   The priority of the task to change.
        /// </param>
        /// <param name="priorityNew">
        ///   New priority of the task.
        /// </param>
        public void PriorityTaskChange(int srcTaskPriority, int newPriority)
        {
            int srcTaskIndex = srcTaskPriority - 1;
            string srcTask = _tasks.ElementAt(srcTaskIndex);
            _tasks.RemoveAt(srcTaskIndex);
            _tasks.Insert(newPriority - 1, srcTask);
        }
        /// <summary>
        ///   Create the list of strings to hold the tasks.
        /// </summary>
        public Tasks()
        {
            this._tasks = new List<string>();
        }
    }
}
