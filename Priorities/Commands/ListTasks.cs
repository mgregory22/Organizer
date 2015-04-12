using MSG.IO;
using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    public class ListTasks : TaskCommand
    {
        Print print;
        public ListTasks(Print print, Tasks tasks)
            : base(tasks)
        {
            this.print = print;
        }
    }
}
