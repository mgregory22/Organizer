//
// Organizer/Modules/Base/DlgCmds/UpMenuDlgCmd.cs
//

using MSG.IO;
using MSG.Patterns;
using MSG.Patterns.Cmds;

namespace Organizer.Modules.Base.DlgCmds
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
