//
// Organizer/Modules/Tasks/DlgCmds/RenameTaskDlgCmd.cs
//

using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using Organizer.Modules.Tasks.Cmds;

namespace Organizer.Modules.Tasks.DlgCmds
{
    public class RenameTaskDlgCmd : DlgCmd
    {
        protected Tasks tasks;

        public RenameTaskDlgCmd(Io io, UndoAndRedo undoAndRedo, Tasks tasks)
            : base(io)
        {
            this.tasks = tasks;
        }

        public override Cmd Create()
        {
            Editor editor = new Editor();
            IntEditor intEditor = new IntEditor();
            int? srcPriority;
            string name;

            srcPriority = intEditor.RangePrompt(io, 1, tasks.Count, PromptForTarget);
            if (srcPriority == null)
            {
                return null;
            }
            name = editor.StringPrompt(io, PromptForName);
            if (name == null)
            {
                return null;
            }
            return new RenameTask(tasks, srcPriority.Value - 1, name);
        }

        public string PromptForName {
            get { return "Enter new name\n$ "; }
        }

        public string PromptForTarget {
            get { return "\nEnter priority of item to rename\n# "; }
        }
    }
}
