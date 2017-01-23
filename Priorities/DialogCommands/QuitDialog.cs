//
// Priorities/DialogCommands/QuitDialog.cs
//

using System;
using MSG.IO;
using MSG.Patterns;

namespace Priorities.DialogCommands
{
    public class QuitDialog : DialogCommand
    {
        public QuitDialog(Print print, Read read)
            : base(print, read, null)
        {
        }

        public override Command Create()
        {
            // throwing an exception for a usual occurrence is admittedly pretty lame
            throw new OperationCanceledException("User has quit");
        }
    }
}
