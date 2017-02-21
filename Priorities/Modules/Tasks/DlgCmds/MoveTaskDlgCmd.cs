//
// Priorities/Modules/Tasks/DlgCmds/MoveTaskDlgCmd.cs
//

using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using Priorities.Modules.Tasks.Cmds;

namespace Priorities.Modules.Tasks.DlgCmds
{
    public class MoveTaskDlgCmd : DlgUnCmd
    {
        Tasks tasks;

        public MoveTaskDlgCmd(Io io, UndoManager undoManager, Tasks tasks)
            : base(io, undoManager)
        {
            this.tasks = tasks;
        }

        public override UnCmd Create()
        {
            IntEditor intEditor = new IntEditor();
            int? srcPriority;
            int? destPriority;

            srcPriority = intEditor.RangePrompt(io, 1, tasks.Count, PromptForSrc);
            if (srcPriority == null)
            {
                return null;
            }
            destPriority = intEditor.RangePrompt(io, 1, tasks.Count, PromptForDest);
            if (destPriority == null)
            {
                return null;
            }
            return new MoveTask(tasks, srcPriority.Value - 1, destPriority.Value - 1);
        }

        public string PromptForDest {
            get { return "Enter new priority\n# "; }
        }

        public string PromptForSrc {
            get { return "\nEnter priority of item to change\n# "; }
        }
    }
}
;
