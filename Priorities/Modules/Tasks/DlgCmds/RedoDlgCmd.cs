//
// Priorities/Modules/Tasks/DlgCmds/RedoDlgCmd.cs
//

using MSG.IO;
using MSG.Patterns;
using Priorities.Modules.Tasks.Cmds;

namespace Priorities.Modules.Tasks.DlgCmds
{
    public class RedoDlgCmd : DlgUnCmd
    {
        protected Tasks tasks;

        public RedoDlgCmd(Io io, UndoManager undoManager, Tasks tasks)
            : base(io, undoManager)
        {
            this.tasks = tasks;
        }

        public override UnCmd Create()
        {
            return new RedoTask(undoManager, tasks);
        }
    }
}
