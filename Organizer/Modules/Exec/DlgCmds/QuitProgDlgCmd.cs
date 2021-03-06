﻿//
// Organizer/Modules/Base/DlgCmds/QuitProgramDialog.cs
//

using MSG.IO;
using MSG.Patterns;
using MSG.Patterns.Cmds;

namespace Organizer.Modules.Base.DlgCmds
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
