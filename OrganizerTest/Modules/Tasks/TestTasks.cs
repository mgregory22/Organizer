//
// OrganizerTest/TestTasks.cs
//

using MSG.Types.Dir;
using System.Collections;
using System.Collections.Generic;
using Organizer.Modules.Tasks;

namespace OrganizerTest
{
    /// <summary>
    /// Mock Tasks object for testing the task commands.
    /// </summary>
    class TestTasks : Tasks, IEnumerable
    {
        public int addCnt;
        public string add_name;
        public Task add_task;
        public int insertCnt;
        public string insert_name;
        public Task insert_task;
        public int insert_index;

        public TestTasks() : base(new MemDir<Task>())
        {
        }

        public override void Add(string name, Task task)
        {
            addCnt++;
            // Save the last parameters Insert() was called with, so they can be checked
            add_name = name;
            add_task = task;
        }

        public override void Insert(int index, string name, Task task)
        {
            insertCnt++;
            // Save the last parameters Insert() was called with, so they can be checked
            insert_name = name;
            insert_task = task;
            insert_index = index;
        }

        public List<Enumerated<Task>> enumerator_collection;
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
