using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Priorities.TaskList
{
    class Task
    {
        int priority;
        string name;
    }

    class TaskList
    {
        int listNumber;
        int parentTask;
        List<Task> tasks;
    }
}
