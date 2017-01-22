//
// Priorities/TaskCommands/DeleteTask.cs
//

using MSG.Console;
using MSG.IO;
using System;
using Priorities.Types;

namespace Priorities.TaskCommands
{
    class DeleteTask : TaskCommand
    {
        protected int? priority;
        protected Task deletedTask;
        Print print;
        Read read;

        public DeleteTask(Print print, Read read, Tasks tasks)
            : base(tasks)
        {
            this.print = print;
            this.read = read;
        }

        public override void Do()
        {
            IntEditor editor = new IntEditor(print, read);
            priority = editor.IntPrompt();
            if (priority == 0)
            {
                print.StringNL("Add cancelled");
                return;
            }
            Redo();
        }

        public override void Redo()
        {
            if (priority == null)
                throw new InvalidOperationException("Deleting a task must be done before it can be redone");
            if (tasks.TaskExists(priority.Value))
                throw new InvalidOperationException("Deleting a task cannot be redone before it is undone");
            deletedTask = tasks[priority.Value];
            tasks.Remove(priority.Value);
        }

        public override void Undo()
        {
            if (deletedTask == null)
                throw new InvalidOperationException("Deleting a task must be done before it can be undone");
            tasks.Add(deletedTask.Name, priority.Value);
        }
    }
}
