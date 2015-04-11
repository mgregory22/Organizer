using MSG.IO;
using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    class ListTasks : TaskCommand
    {
        public ListTasks(Print print, Read read, Tasks tasks)
            : base(print, read, tasks)
        {
        }
    }
}
