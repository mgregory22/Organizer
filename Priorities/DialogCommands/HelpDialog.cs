//
// Priorities/DialogCommands/HelpDialog.cs
//

using MSG.IO;
using MSG.Patterns;

namespace Priorities.DialogCommands
{
    /// <summary>
    ///   The Help object just prints the ToString() output of
    ///   whatever object you send it.
    /// </summary>
    public class HelpDialog : DialogCommand
    {
        object target;

        public HelpDialog(Print print, object target)
            : base(print, null, null)
        {
            this.target = target;
        }

        public override Command Create()
        {
            print.String(target.ToString());
            return null;
        }
    }
}
