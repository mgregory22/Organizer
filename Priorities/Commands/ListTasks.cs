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

        public override void Do()
        {
            int priority = 0;
            foreach (Task task in tasks)
            {
                print.StringNL(String.Format("[{0}] {1}", priority++, task.Name));
            }
        }
    }
}
