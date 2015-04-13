using Priorities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PrioritiesTest
{
    /// <summary>
    ///   Mock Tasks object for testing the task commands.
    /// </summary>
    class TestTasks : Tasks, IEnumerable
    {
        public int addCnt;
        public string add_name;
        public int add_parent;
        public int add_priority;
        public override void Add(string name, int parent = 0, int priority = 1)
        {
            addCnt++;
            // Save the last parameters Add() was called with, so they can be checked
            add_name = name;
            add_parent = parent;
            add_priority = priority;
        }
        public int count;
        public override int Count
        {
            get { return count; }
        }
        public List<Task> enumerator_collection;
        IEnumerator IEnumerable.GetEnumerator()
        {
            return enumerator_collection.GetEnumerator();
        }
        public int removeCnt;
        public string remove_name;
        public override void Remove(string name)
        {
            removeCnt++;
            // Save the last parameters Remove() was called with, so they can be checked
            remove_name = name;
        }
        public int taskExistsCnt;
        public string taskExists_name;
        public bool taskExists_nextReturn;
        public override bool TaskExists(string name)
        {
            taskExists_name = name;
            return taskExists_nextReturn;
        }
    }
}
