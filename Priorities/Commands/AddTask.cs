using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    public class AddTask : TaskCommand
    {
        protected string taskName;
        public AddTask(Print print, Read read, Tasks tasks)
            : base(print, read, tasks)
        {

        }
        public override int Do()
        {
            StringPrompt prompt = new StringPrompt(print, "Enter task name\n> ", read);
            taskName = prompt.DoPrompt();
            Redo();
            return 0;
        }
        public override int Redo()
        {
            this.tasks.Add(taskName);
            return 0;
        }
    }
}
