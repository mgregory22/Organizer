//
// Priorities/DialogCommands/OptionsDialog.cs
//

using MSG.IO;
using MSG.Patterns;

namespace Priorities.DialogCommands
{
    public class OptionsDialog : DialogCommand
    {
        public OptionsDialog(Print print, Read read)
            :base(print, read, null)
        {
        }

        public override Command Create()
        {
            return null;
        }
    }
}
