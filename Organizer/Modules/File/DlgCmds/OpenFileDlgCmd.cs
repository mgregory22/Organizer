//
// Organizer/Modules/File/DlgCmds/OpenFileDlgCmd.cs
//

using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using Organizer.Modules.File.Cmds;

namespace Organizer.Modules.File.DlgCmds
{
    public class OpenFileDlgCmd : DlgCmd
    {
        protected string fileName;

        public OpenFileDlgCmd(Io io) : base(io)
        {
        }

        public string FileNamePrompt {
            get { return "\nEnter file name\n$ "; }
        }

        public override Cmd Create()
        {
            Editor fileNameEditor = new Editor();
            string fileName = fileNameEditor.StringPrompt(io, FileNamePrompt);
            if (string.IsNullOrEmpty(fileName)) {
                io.print.StringNL("Open file operation cancelled");
                return null;
            }
            return new OpenFile(fileName);
        }
    }
}
