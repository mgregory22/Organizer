//
// Priorities/DialogCommands/UpMenuDialog.cs
//

using MSG.IO;
using MSG.Patterns;

namespace Priorities.DialogCommands
{
    public class UpMenuDialog : DialogCommand
    {
        public UpMenuDialog(Print print, Read read) : base(print, read)
        {
        }

        public override Command Create()
        {
            return new MSG.Patterns.UpMenu();
        }
    }
}
