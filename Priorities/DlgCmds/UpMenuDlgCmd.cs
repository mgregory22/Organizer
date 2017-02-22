//
// Priorities/DlgCmds/UpMenuDlgCmd.cs
//

using MSG.IO;
using MSG.Patterns;
using MSG.Patterns.Cmds;

namespace Priorities.DlgCmds
{
    public class UpMenuDlgCmd : DlgCmd
    {
        public UpMenuDlgCmd(Io io) : base(io)
        {
        }

        public override Cmd Create()
        {
            return new UpMenu();
        }
    }
}
