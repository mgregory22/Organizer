//
// Organizer/Modules/Tasks/TaskMenuPrompt.cs
//

using MSG.Console;
using MSG.IO;

namespace Organizer.Modules.Tasks
{
    public class TaskMenuPrompt : CharPrompt
    {
        Tasks tasks;

        public TaskMenuPrompt(Tasks tasks)
            : base()
        {
            this.tasks = tasks;
        }

        /// <summary>
        /// The text that prompts the user for input.
        /// </summary>
        public override string Prompt {
            get { return tasks.GetCurPath() + prompt; }
        }
    }
}
