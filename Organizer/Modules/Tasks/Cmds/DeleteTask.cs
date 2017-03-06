//
// Organizer/Modules/Tasks/Cmds/DeleteTask.cs
//

using System;

namespace Organizer.Modules.Tasks.Cmds
{
    public class DeleteTask : TaskCmd
    {
        /// <summary>
        /// The index in the list where the task was deleted
        /// </summary>
        protected int index;

        /// <summary>
        /// The task that was removed from the list
        /// </summary>
        protected Task deletedTask;
        protected string deletedName;

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
            if (!tasks.ItemExistsAt(index)) {
                throw new ArgumentOutOfRangeException("index", "index must be nonnegative and less than tasks.Count");
            }
            this.index = index;
        }

        public override Result Do()
        {
            deletedTask = tasks.GetItemAt(index);
            deletedName = tasks.GetNameAt(index);
            tasks.RemoveAt(index);
            return new Ok();
        }

        public override Result Undo()
        {
            tasks.Insert(index, deletedName, deletedTask);
            return new Ok();
        }
    }
}
