//
// Priorities/Modules/Tasks/DlgCmds/RedoDlgCmd.cs
//

using MSG.IO;
using MSG.Patterns;
using Priorities.Modules.Tasks.Cmds;

namespace Priorities.Modules.Tasks.DlgCmds
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
