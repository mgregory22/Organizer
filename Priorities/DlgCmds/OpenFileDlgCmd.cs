//
// Priorities/DlgCmds/OpenFileDlgCmd.cs
//

using MSG.IO;
using MSG.Patterns;

namespace Priorities.DlgCmds
{
    public class OpenFileDlgCmd : DlgCmd
    {
        public OpenFileDlgCmd(Io io) : base(io)
        {
        }

        public override Cmd Create()
        {
            return null;
        }
    }
}
