//
// Priorities/Features/Tasks/DialogCommands/InsertTaskDialog.cs
//

using System;
using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using Priorities.Features.Tasks.Commands;

namespace Priorities.Features.Tasks.DialogCommands
{
    /// <summary>
    /// InsertTaskDialog executes the Insert Task dialog.
    /// Then, if successful, executes the Insert Task command.
    /// </summary>
    public class InsertTaskDialog : DialogCommand
    {
        protected Tasks tasks;

        /// <summary>
        /// Initializes AddDialog.
        /// </summary>
        /// <param name="print"></param>
        /// <param name="read"></param>
        /// <param name="addTask"></param>
        public InsertTaskDialog(Print print, Read read, UndoManager undoManager, Tasks tasks)
            : base(print, read, undoManager)
        {
            this.tasks = tasks;
        }

        public override Command Create()
        {
            Editor editor = new Editor(print, read);
            string name = editor.StringPrompt(Prompt);
            if (String.IsNullOrEmpty(name))
            {
                print.StringNL("Insert cancelled");
                return null;
            }
            return new InsertTask(tasks, name, 0);
        }

        public string Prompt {
            get { return "\nEnter task name/description\n$ "; }
        }
    }
}
