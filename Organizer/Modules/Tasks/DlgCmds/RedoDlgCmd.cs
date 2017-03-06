//
// Organizer/Modules/Tasks/DlgCmds/RedoDlgCmd.cs
//

using MSG.IO;
using MSG.Patterns;
using Organizer.Modules.Tasks.Cmds;

namespace Organizer.Modules.Tasks.DlgCmds
{
    public class RedoDlgCmd : DlgCmd
    {
        protected Tasks tasks;

        public RedoDlgCmd(Io io, UndoAndRedo undoAndRedo, Tasks tasks)
            : base(io, undoAndRedo)
        {
            this.tasks = tasks;
        }

        public override Cmd Create()
        {
            return new RedoTask(undoAndRedo, tasks);
        }
    }
}
