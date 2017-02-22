//
// Priorities/DlgCmds/QuitProgramDialog.cs
//

using MSG.IO;
using MSG.Patterns;
using MSG.Patterns.Cmds;

namespace Priorities.DlgCmds
{
    public class QuitProgDlgCmd : DlgCmd
    {
        public QuitProgDlgCmd(Io io) : base(io)
        {
        }

        public override Cmd Create()
        {
            return new QuitProg();
        }
    }
}
