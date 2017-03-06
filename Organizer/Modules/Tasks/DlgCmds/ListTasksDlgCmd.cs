//
// Organizer/Modules/Tasks/DlgCmds/ListTasksDlgCmd.cs
//

using System;
using MSG.IO;
using MSG.Patterns;
using MSG.Types.Dir;

namespace Organizer.Modules.Tasks.DlgCmds
{
    public class ListTasksDlgCmd : DlgCmd
    {
        Tasks tasks;

        public ListTasksDlgCmd(Io io, Tasks tasks)
            : base(io)
        {
            this.tasks = tasks;
        }

        public override Cmd Create()
        {
            int priority = 1;
            foreach (Enumerated<Task> task in tasks)
                io.print.StringNL(String.Format("[{0}] {1}", priority++, task.Name));
            return null;
        }

    }
}
