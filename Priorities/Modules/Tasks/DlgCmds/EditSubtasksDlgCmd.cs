//
// Priorities/Modules/Tasks/DlgCmds/EditSubtasksDlgCmd.cs
//

using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using Priorities.Modules.Tasks.Cmds;

namespace Priorities.Modules.Tasks.DlgCmds
{
    public class EditSubtasksDlgCmd : DlgUnCmd
    {
        protected Tasks tasks;

        public EditSubtasksDlgCmd(Io io, UndoManager undoManager, Tasks tasks)
            : base(io, undoManager)
        {
            this.tasks = tasks;
        }

        public override UnCmd Create()
        {
            IntEditor intEditor = new IntEditor();
            int? targetPriority = intEditor.RangePrompt(io, 1, tasks.Count, PromptForTarget);
            if (targetPriority == null)
            {
                return null;
            }
            int index = targetPriority.Value - 1;
            Editor editor = new Editor();
            string name = editor.StringPrompt(io, PromptForName);
            if (name == null)
            {
                return null;
            }
            return new EditSubtasks(tasks, index, name);
        }

        public string PromptForName {
            get { return "Enter new task name/description\n$ "; }
        }

        public string PromptForTarget {
            get { return "\nEnter priority of item to create subtask for\n# "; }
        }
    }
}
