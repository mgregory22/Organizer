//
// Priorities/Modules/Tasks/DlgCmds/UndoDlgCmd.cs
//

using MSG.IO;
using MSG.Patterns;
using Priorities.Modules.Tasks.Cmds;

namespace Priorities.Modules.Tasks.DlgCmds
{
    public class UndoDlgCmd : DlgCmd
    {
        protected Tasks tasks;
        protected UndoManager undoManager;

        public UndoDlgCmd(Io io, UndoManager undoManager, Tasks tasks)
            : base(io)
        {
            this.tasks = tasks;
            this.undoManager = undoManager;
        }

        public override Cmd Create()
        {
            return new UndoTask(undoManager, tasks);
        }
    }
}
