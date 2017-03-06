//
// Organizer/Modules/Tasks/DlgCmds/DeleteTaskDlgCmd.cs
//

using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using Organizer.Modules.Tasks.Cmds;

namespace Organizer.Modules.Tasks.DlgCmds
{
    public class DeleteTaskDlgCmd : DlgCmd
    {
        protected Tasks tasks;

        public DeleteTaskDlgCmd(Io io, UndoAndRedo undoAndRedo, Tasks tasks)
            : base(io, undoAndRedo)
        {
            this.tasks = tasks;
        }

        public override Cmd Create()
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
