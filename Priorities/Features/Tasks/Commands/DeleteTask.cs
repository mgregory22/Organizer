//
// Priorities/Features/Tasks/Commands/DeleteTask.cs
//

using System;

namespace Priorities.Features.Tasks.Commands
{
    public class DeleteTask : TaskCommand
    {
        /// <summary>
        /// The index in the list where the task was deleted
        /// </summary>
        protected int index;

        /// <summary>
        /// The task that was removed from the list
        /// </summary>
        protected Task deletedTask;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tasks">
        /// The list of tasks to manipulate
        /// </param>
        /// <param name="index">
        /// The list index of the task to delete
        /// </param>
        public DeleteTask(Tasks tasks, int index)
            : base(tasks)
        {
            if (!tasks.IndexExists(index))
                throw new ArgumentOutOfRangeException("index", "index must be nonnegative and less than tasks.Count");
            this.index = index;
        }

        public override Result Do()
        {
            deletedTask = tasks[index];
            tasks.Remove(index);
            return new Ok();
        }

        public override Result Undo()
        {
            tasks.Insert(deletedTask, index);
            return new Ok();
        }
    }
}
