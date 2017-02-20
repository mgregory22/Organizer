//
// PrioritiesTest/TestTasks.cs
//

using System.Collections;
using System.Collections.Generic;
using Priorities.Features.Tasks;

namespace PrioritiesTest
{
    /// <summary>
    /// Mock Tasks object for testing the task commands.
    /// </summary>
    class TestTasks : Tasks, IEnumerable
    {
        public int addCnt;
        public Task add_task;
        public int insertCnt;
        public Task insert_task;
        public int insert_index;

        public override void Add(Task task)
        {
            addCnt++;
            // Save the last parameters Insert() was called with, so they can be checked
            add_task = task;
        }

        public override void Insert(int index, Task task)
        {
            insertCnt++;
            // Save the last parameters Insert() was called with, so they can be checked
            insert_task = task;
            insert_index = index;
        }

        public List<Task> enumerator_collection;
        IEnumerator IEnumerable.GetEnumerator()
        {
            return enumerator_collection.GetEnumerator();
        }

        public int removeCnt;
        public Task remove_task;

        public override void Remove(Task task)
        {
            removeCnt++;
            remove_task = task;
        }

    }
}
