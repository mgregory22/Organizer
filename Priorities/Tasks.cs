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
        ///   Task name.
        /// </param>
        /// <param name="priority">
        ///   Task priority.
        /// </param>
        public void Add(string task, int priority = 1)
        {
            if (priority > Count + 1) priority = Count + 1;
            if (priority < 1) priority = 1;
            _tasks.Insert(IndexFromPriority(priority), task);
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
        /// <param name="priority">
        ///   Priority of the task to delete.
        /// </param>
        public void Delete(int priority)
        {
            _tasks.RemoveAt(IndexFromPriority(priority));
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
                if (!each(_tasks[IndexFromPriority(priority)], priority)) return false;
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
            return _tasks.ElementAt(IndexFromPriority(priority));
        }
        /// <summary>
        ///   Loads the list of to-do tasks from tasks.txt.
        /// </summary>
        public void FileLoad(string filename, MSGLib.Console con)
        {
            if (File.Exists(filename))
            {
                StreamReader sr;
                try
                {
                    sr = new StreamReader(filename);
                }
                catch (Exception e)
                {
                    con.PrintColored(String.Format("File {0} could not be opened for reading: {1}", filename, e.Message), ConsoleColor.Red, true);
                    System.Environment.Exit(1);
                    return; // just to appease VS
                }
                Clear();
                string task;
                while ((task = sr.ReadLine()) != null)
                {
                    Add(task);
                }
                sr.Close();
            }
        }
        /// <summary>
        ///   Saves the list of to-do tasks to the file named in _filename.
        /// </summary>
        public void FileSave(string filename, MSGLib.Console con)
        {
            StreamWriter sw;
            try
            {
                sw = new StreamWriter(filename);
            }
            catch (Exception e)
            {
                con.PrintColored(String.Format("File {0} could not be opened for writing: {1}", filename, e.Message), ConsoleColor.Red, true);
                System.Environment.Exit(1);
                return; // just to appease VS
            }
            ForEach((string task, int priority) =>
            {
                sw.WriteLine(task);
                return true;
            });
            sw.Close();
        }
        /// <summary>
        ///   Converts a priority value to a list index.
        /// </summary>
        /// <param name="priority">
        ///   Priority value to convert.
        /// </param>
        /// <returns>
        ///   Index into the internal task list.
        /// </returns>
        private int IndexFromPriority(int priority)
        {
            return priority - 1;
        }
        /// <summary>
        ///   Changes the priority of a task.
        /// </summary>
        /// <param name="priority">
        ///   The priority of the task to change.
        /// </param>
        /// <param name="priorityNew">
        ///   New priority of the task.
        /// </param>
        public void PriorityTaskChange(int priorityTask, int priorityNew)
        {
            int indexTask = IndexFromPriority(priorityTask);
            string srcTask = _tasks.ElementAt(indexTask);
            _tasks.RemoveAt(indexTask);
            int indexNew = IndexFromPriority(priorityNew);
            _tasks.Insert(indexNew, srcTask);
        }
        /// <summary>
        ///   Renames the task with the given priority.
        /// </summary>
        /// <param name="priority">
        ///   Priority of the task to rename.
        /// </param>
        /// <param name="name">
        ///   New name of the task.
        /// </param>
        public void Rename(int priority, string name)
        {
            int indexTask = IndexFromPriority(priority);
            _tasks.RemoveAt(indexTask);
            _tasks.Insert(indexTask, name);
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
