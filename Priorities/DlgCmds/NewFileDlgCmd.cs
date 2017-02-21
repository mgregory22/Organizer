//
// Priorities/DlgCmds/NewFileDialog.cs
//

using MSG.IO;
using MSG.Patterns;

namespace Priorities.DlgCmds
{
    public class NewFileDlgCmd : DlgCmd
    {
        public NewFileDlgCmd(Io io) : base(io)
        {
        }

        public override Cmd Create()
        {
            return null;
        }
    }
}
