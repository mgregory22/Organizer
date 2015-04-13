using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    public class AddTask : TaskCommand
    {
        StringPrompt prompt;
        protected string taskName;
        public AddTask(Print print, Read read, Tasks tasks)
            : base(tasks)
        {
            prompt = new StringPrompt(print, "Enter task name\n> ", read);
        }
        public override void Do()
        {
            taskName = prompt.DoPrompt();
            Redo();
        }
        public override void Redo()
        {
            if (taskName == null)
                throw new InvalidOperationException("Adding a task must be done before it can be redone");
            if (tasks.TaskExists(taskName))
                throw new InvalidOperationException("Adding a task cannot be redone before it is undone");
            tasks.Add(taskName);
        }
        public override void Undo()
        {
            if (taskName == null)
                throw new InvalidOperationException("Adding a task must be done before it can be undone");
            if (!tasks.TaskExists(taskName))
                throw new InvalidOperationException("Adding a task cannot be undone twice");
            tasks.Remove(taskName);
        }
    }
}
