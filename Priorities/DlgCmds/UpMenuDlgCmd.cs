//
// Priorities/DlgCmds/UpMenuDialog.cs
//

using MSG.IO;
using MSG.Patterns;

namespace Priorities.DlgCmds
{
    public class UpMenuDlgCmd : DlgCmd
    {
        public UpMenuDlgCmd(Io io) : base(io)
        {
        }

        public override Cmd Create()
        {
            return new MSG.Patterns.UpMenu();
        }
    }
}
