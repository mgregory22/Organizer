//
// Organizer/Modules/File/DlgCmds/NewDlgCmd.cs
//

using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using Organizer.Modules.File.Cmds;

namespace Organizer.Modules.File.DlgCmds
{
    public class NewFileDlgCmd : DlgCmd
    {
        protected string fileName;

        public NewFileDlgCmd(Io io) : base(io)
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
                io.print.StringNL("New file operation cancelled");
                return null;
            }
            return new NewFile(fileName);
        }
    }
}
