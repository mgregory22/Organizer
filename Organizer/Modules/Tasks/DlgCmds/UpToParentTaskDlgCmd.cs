//
// Organizer/Modules/Tasks/DlgCmds/EditSubtasksDlgCmd.cs
//

using MSG.IO;
using MSG.Patterns;

namespace Organizer.Modules.Tasks.DlgCmds
{
    public class UpToParentTaskDlgCmd : DlgCmd
    {
        protected Tasks tasks;

        public UpToParentTaskDlgCmd(Io io, Tasks tasks)
            : base(io)
        {
            this.tasks = tasks;
        }

        public override Cmd Create()
        {
            tasks.UpDir();
            return null;
        }
    }
}
