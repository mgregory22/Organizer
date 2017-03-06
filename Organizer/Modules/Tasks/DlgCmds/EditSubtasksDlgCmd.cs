//
// Organizer/Modules/Tasks/DlgCmds/EditSubtasksDlgCmd.cs
//

using MSG.Console;
using MSG.IO;
using MSG.Patterns;

namespace Organizer.Modules.Tasks.DlgCmds
{
    public class EditSubtasksDlgCmd : DlgCmd
    {
        protected Tasks tasks;

        public EditSubtasksDlgCmd(Io io, Tasks tasks)
            : base(io)
        {
            this.tasks = tasks;
        }

        public override Cmd Create()
        {
            IntEditor intEditor = new IntEditor();
            int? targetPriority = intEditor.RangePrompt(io, 1, tasks.Count, PriorityPrompt);
            if (targetPriority == null)
            {
                return null;
            }
            int index = targetPriority.Value - 1;
            tasks.DownDir(index);
            return null;
        }

        public string PriorityPrompt {
            get { return "\nEnter item number to edit subtasks for\n# "; }
        }
    }
}
