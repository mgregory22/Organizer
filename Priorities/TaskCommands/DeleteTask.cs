//
// Priorities/TaskCommands/DeleteTask.cs
//

using System;
using MSG.Console;
using MSG.IO;
using Priorities.Types;

namespace Priorities.TaskCommands
{
    class DeleteTask : TaskCommand
    {
        /// <summary>
        ///   The position in the list where the task was deleted
        /// </summary>
        protected int position;

        /// <summary>
        ///   The task that was removed from the list
        /// </summary>
        protected Task deletedTask;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tasks">
        ///   The list of tasks to manipulate
        /// </param>
        /// <param name="position">
        ///   The list position of the task to delete
        /// </param>
        public DeleteTask(Tasks tasks, int position)
            : base(tasks)
        {
            if (!tasks.TaskExists(position))
                throw new ArgumentOutOfRangeException("position", "position must be nonnegative and less than tasks.Count");
            this.position = position;
        }

        public override void Do()
        {
            deletedTask = tasks[position];
            tasks.Remove(position);
        }

        public override void Undo()
        {
            tasks.Add(deletedTask, position);
        }
    }
}
