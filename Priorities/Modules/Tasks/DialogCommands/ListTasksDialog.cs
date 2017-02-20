//
// Priorities/Modules/Tasks/DialogCommands/ListTasksDialog.cs
//

using System;
using MSG.IO;
using MSG.Patterns;
using Priorities;

namespace Priorities.Modules.Tasks.DialogCommands
{
    public class ListTasksDialog : DialogCommand
    {
        Tasks tasks;

        public ListTasksDialog(Print print, Tasks tasks)
            : base(print, null, null)
        {
            this.tasks = tasks;
        }

        public override Command Create()
        {
            int priority = 1;
            foreach (Task task in tasks)
                print.StringNL(String.Format("[{0}] {1}", priority++, task.Name));
            return null;
        }

    }
}
