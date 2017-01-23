//
// Priorities/DialogCommands/AddTaskDialog.cs
//

using System;
using MSG.IO;
using MSG.Patterns;
using Priorities.TaskCommands;
using Priorities.Types;

namespace Priorities.DialogCommands
{
    /// <summary>
    ///   AddDialog executes the Add Task dialog.
    ///   Then, if successful, executes the Add Task command.
    /// </summary>
    public class AddTaskDialog : DialogCommand
    {
        protected Tasks tasks;

        /// <summary>
        ///   Initializes AddDialog.
        /// </summary>
        /// <param name="print"></param>
        /// <param name="read"></param>
        /// <param name="addTask"></param>
        public AddTaskDialog(Print print, Read read, UndoManager undoManager, Tasks tasks)
            : base(print, read, undoManager)
        {
            this.tasks = tasks;
        }

        public override Command Create()
        {
            string name = editor.StringPrompt();
            if (String.IsNullOrEmpty(name))
            {
                print.StringNL("Add cancelled");
                return null;
            }
            return new AddTask(tasks, name);
        }
    }
}
