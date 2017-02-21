//
// Priorities/Modules/Tasks/DlgCmds/DeleteTaskDialog.cs
//

using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using Priorities.Modules.Tasks.Cmds;

namespace Priorities.Modules.Tasks.DlgCmds
{
    public class DeleteTaskDlgCmd : DlgUnCmd
    {
        protected Tasks tasks;

        public DeleteTaskDlgCmd(Io io, UndoManager undoManager, Tasks tasks)
            : base(io, undoManager)
        {
            this.tasks = tasks;
        }

        public override UnCmd Create()
        {
            IntEditor editor = new IntEditor();
            int? priority = null;

            // If there's only one item, then just delete that item
            if (tasks.Count == 1)
                priority = 0;
            else
            {
                while (priority == null)
                {
                    // Ask user for priority of task to delete
                    priority = editor.RangePrompt(io, 1, tasks.Count, Prompt);
                    // If escape was pressed, abort
                    if (priority == null)
                    {
                        return null;
                    }
                }
            }
            return new DeleteTask(tasks, priority.Value - 1);
        }

        public string Prompt {
            get { return "\nEnter priority of item to delete\n# "; }
        }
    }
}
