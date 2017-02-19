//
// Priorities/DialogCommands/QuitProgramDialog.cs
//

using MSG.IO;
using MSG.Patterns;

namespace Priorities.DialogCommands
{
    public class QuitProgramDialog : DialogCommand
    {
        public QuitProgramDialog(Print print, Read read) : base(print, read)
        {
        }

        public override Command Create()
        {
            return new MSG.Patterns.QuitProgram();
        }
    }
}
