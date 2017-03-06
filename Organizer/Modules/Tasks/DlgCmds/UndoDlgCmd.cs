//
// Organizer/Modules/Tasks/DlgCmds/UndoDlgCmd.cs
//

using MSG.IO;
using MSG.Patterns;
using Organizer.Modules.Tasks.Cmds;

namespace Organizer.Modules.Tasks.DlgCmds
{
    public class UndoDlgCmd : DlgCmd
    {
        protected Tasks tasks;

        public UndoDlgCmd(Io io, UndoAndRedo undoAndRedo, Tasks tasks)
            : base(io, undoAndRedo)
        {
            this.tasks = tasks;
        }

        public override Cmd Create()
        {
            return new UndoTask(undoAndRedo, tasks);
        }
    }
}
