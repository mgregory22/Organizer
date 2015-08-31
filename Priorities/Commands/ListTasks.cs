//
// Priorities/Commands/ListTasks.cs
//

using MSG.IO;
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

        override public void Do()
        {
            int priority = 0;
            foreach (Task task in tasks)
            {
                print.StringNL(String.Format("[{0}] {1}", priority++, task.Name));
            }
        }
    }
}
