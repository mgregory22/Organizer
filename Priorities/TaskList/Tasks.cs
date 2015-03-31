using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Priorities.Tasks
{
    class Task
    {
        string name;
        int parent;
        int priority;
    }

    class List
    {
        List<Task> tasks;
    }
}
