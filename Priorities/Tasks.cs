using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Priorities
{
    class Task
    {
        string name;
        int parent;
        int priority;
        public Task(string name, int parent, int priority)
        {
            this.name = name;
            this.parent = parent;
            this.priority = priority;
        }
    }

    class Tasks
    {
        List<Task> tasks;
        public Tasks()
        {
            this.tasks = new List<Task>();
        }
        public virtual void Add(string name, int parent = 0, int priority = 1)
        {
            tasks.Add(new Task(name, parent, priority));
        }
    }
}
