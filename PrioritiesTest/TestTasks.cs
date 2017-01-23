//
// PrioritiesTest/TestTasks.cs
//

using System.Collections;
using System.Collections.Generic;
using Priorities.Types;

namespace PrioritiesTest
{
    /// <summary>
    ///   Mock Tasks object for testing the task commands.
    /// </summary>
    class TestTasks : Tasks, IEnumerable
    {
        public int addCnt;
        public Task add_task;
        public int add_position;

        public override void Add(Task task, int position = -1)
        {
            addCnt++;
            // Save the last parameters Add() was called with, so they can be checked
            add_task = task;
            add_position = position;
        }

        /*
        public int count;
        public override int Count
        {
            get { return count; }
        }
         */

        public List<Task> enumerator_collection;
        IEnumerator IEnumerable.GetEnumerator()
        {
            return enumerator_collection.GetEnumerator();
        }

        public int removeCnt;
        public Task remove_task;
        public string remove_name;

        public override void Remove(Task task)
        {
            removeCnt++;
            remove_task = task;
        }

        public override void Remove(string name)
        {
            removeCnt++;
            // Save the last parameters Remove() was called with, so they can be checked
            remove_name = name;
        }

        /*
        public int taskExistsCnt;
         */
        public string taskExists_name;
        public bool taskExists_nextReturn;

        public override bool TaskExists(string name)
        {
            taskExists_name = name;
            return taskExists_nextReturn;
        }
    }
}
