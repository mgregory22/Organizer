//
// Priorities/DialogCommands/AdminToolsDialog.cs
//

using MSG.IO;
using MSG.Patterns;

namespace Priorities.DialogCommands
{
    public class AdminToolsDialog : DialogCommand
    {
        public AdminToolsDialog(Print print, Read read)
            :base(print, read, null)
        {
        }

        public override Command Create()
        {
            return null;
        }
    }
}
