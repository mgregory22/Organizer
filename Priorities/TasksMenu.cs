using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Priorities
{
    class TasksMenu
    {
        /// <summary>
        ///   Creates the menu items.
        /// </summary>
        /// <param name="tasks">
        ///   The task list.
        /// </param>
        /// <param name="con">
        ///   The console on which to print prompts and error messages.
        /// </param>
        /// <returns>
        ///   An <see cref="MSGLib.Console.Menu.Items"/> items object.
        /// </returns>
        MSGLib.Console.Menu.Items ItemsMenuCreate(Tasks tasks, MSGLib.Console con)
        {
            var items = new MSGLib.Console.Menu.Items();
            items.ItemAdd(new MSGLib.Console.Menu.Item
            {
                key = ConsoleKey.A,
                title = "Add Task",
                menuProc = () => TaskAdd(tasks, con)
            });
            items.ItemAdd(new MSGLib.Console.Menu.Item
            {
                key = ConsoleKey.D,
                title = "Delete Task",
                menuProc = () => TaskDelete(tasks, con)
            });
            items.ItemAdd(new MSGLib.Console.Menu.Item
            {
                key = ConsoleKey.L,
                title = "List Tasks",
                menuProc = () => TasksList(tasks, con)
            });
            items.ItemAdd(new MSGLib.Console.Menu.Item
            {
                key = ConsoleKey.M,
                title = "Move Task/Change Priority",
                menuProc = () => TaskMove(tasks, con)
            });
            items.ItemAdd(new MSGLib.Console.Menu.Item
            {
                key = ConsoleKey.R,
                title = "Rename Task",
                menuProc = () => TaskRename(tasks, con)
            });
            return items;
        }
        /// <summary>
        ///   Run menu print/read/execute loop.
        /// </summary>
        /// <param name="tasks">
        ///   The task list.
        /// </param>
        /// <param name="con">
        ///   The console on which to print prompts and error messages.
        /// </param>
        public void PreLoop(Tasks tasks, MSGLib.Console con)
        {
            var items = ItemsMenuCreate(tasks, con);
            var menu = new MSGLib.Console.Menu();
            do
            {
                if (menu.Selection != ConsoleKey.L) TasksList(tasks, con);
                con = menu.PreLoop(items, con);
            } while (menu.Selection != ConsoleKey.Q);
        }
        /// <summary>
        ///   Prompts the user for priority.
        /// </summary>
        /// <param name="prompt">
        ///   Prompt message.
        /// </param>
        /// <param name="tasks">
        ///   The task list.
        /// </param>
        /// <param name="con">
        /// The console on which to print prompts and error messages.
        /// </param>
        /// <param name="adding">
        ///   If true, then an item is being added, so accept one more than the
        ///   number of items so it can be added one past the last item.
        /// </param>
        /// <returns></returns>
        public int PromptForPriority(string prompt, Tasks tasks, MSGLib.Console con, bool adding)
        {
            prompt = String.Format("{0} [1..{1}, 0 to cancel] > ", prompt, tasks.Count + (adding ? 1 : 0));
            int priority = 1;
            if (tasks.Count > 0)
            {
                priority = con.PromptForInteger(prompt, ConsoleColor.Green, (int i) => ValidatePriority(tasks, i, adding));
            }
            return priority;
        }
        /// <summary>
        ///   Adds a task.
        /// </summary>
        /// <param name="tasks">
        ///   The task list.
        /// </param>
        /// <param name="con">
        ///   The console on which to print prompts and error messages.
        /// </param>
        void TaskAdd(Tasks tasks, MSGLib.Console con)
        {
            // Prompt for task name
            string task = con.PromptForString("Enter task name > ", ConsoleColor.Green, ValidateTaskName);
            // Abort if the user enters a blank task
            if (task == String.Empty)
            {
                return;
            }
            // Prompt for priority (if applicable)
            int priority = PromptForPriority("Enter task priority", tasks, con, true);
            tasks.Add(task, priority);
            con.PrintColored(String.Format("Task \"{0}\" added.", task), ConsoleColor.White, true);
        }
        /// <summary>
        ///   Deletes a task.
        /// </summary>
        /// <param name="tasks">
        ///   The task list.
        /// </param>
        /// <param name="con">
        ///   The console on which to print prompts and error messages.
        /// </param>
        void TaskDelete(Tasks tasks, MSGLib.Console con)
        {
            // Prompt for priority (if any tasks exist)
            int priority = 1;
            if (tasks.Count == 0)
            {
                con.PrintColored("There are no tasks to delete.", ConsoleColor.White, true);
                return;
            }
            priority = PromptForPriority("Enter priority of task to delete", tasks, con, false);
            if (priority == 0) return;
            string task = tasks.Get(priority);
            tasks.Delete(priority);
            con.PrintColored(String.Format("Task \"{0}\" deleted.", task), ConsoleColor.White, true);
        }
        /// <summary>
        ///   Changes the priority of a task.
        /// </summary>
        /// <param name="tasks">
        ///   The task list.
        /// </param>
        /// <param name="con">
        ///   The console on which to print prompts and error messages.
        /// </param>
        void TaskMove(Tasks tasks, MSGLib.Console con)
        {
            TasksList(tasks, con);
            int priorityTaskSrc = PromptForPriority("Enter priority of item to move", tasks, con, false);
            if (priorityTaskSrc == 0) return;
            int priorityNew = PromptForPriority("Enter new priority of item", tasks, con, false);
            if (priorityNew == 0) return;
            tasks.PriorityTaskChange(priorityTaskSrc, priorityNew);
            con.PrintColored(String.Format("Task \"{0}\" priority changed.", tasks.Get(priorityTaskSrc)), ConsoleColor.White, true);
        }
        /// <summary>
        ///   Renames a task.
        /// </summary>
        /// <param name="tasks">
        ///   The task list.
        /// </param>
        /// <param name="con">
        ///   The console on which to print prompts and error messages.
        /// </param>
        void TaskRename(Tasks tasks, MSGLib.Console con)
        {
            TasksList(tasks, con);
            int priorityTaskSrc = PromptForPriority("Enter priority of item to rename", tasks, con, false);
            // Cancel if the user enters a blank or zero for the priority
            if (priorityTaskSrc == 0) return;
            // Prompt for task name
            string nameTask = con.PromptForString("Enter task name > ", ConsoleColor.Green, ValidateTaskName);
            // Cancel if the user enters a blank task
            if (nameTask == String.Empty) return;
            // Perform the renamation
            tasks.Rename(priorityTaskSrc, nameTask);
        }
        /// <summary>
        ///   Prints the tasks in priority order.
        /// </summary>
        /// <param name="tasks">
        ///   The task list.
        /// </param>
        /// <param name="con">
        ///   The console on which to print prompts and error messages.
        /// </param>
        void TasksList(Tasks tasks, MSGLib.Console con)
        {
            List<MSGLib.Console.Colors> colColorses = new List<MSGLib.Console.Colors>();
            colColorses.Add(new MSGLib.Console.Colors
            {
                fg = ConsoleColor.Yellow
            });
            colColorses.Add(new MSGLib.Console.Colors
            {
                fg = ConsoleColor.Cyan
            });
            MSGLib.Console.Table.Cols cols = new MSGLib.Console.Table.Cols(colColorses);
            MSGLib.Console.Table.Rows rows = new MSGLib.Console.Table.Rows();
            tasks.ForEach((string task, int priority) => {
                rows.Add(new MSGLib.Console.Table.Rows.Row(cols, "  " + (priority--).ToString(), task));
                return true;
            });
            cols.PaddingAdd(2);
            MSGLib.Console.Table table = new MSGLib.Console.Table();
            table.Print(rows, cols, con);
        }
        /// <summary>
        ///   Creates the menu items.
        /// </summary>
        public TasksMenu()
        {
        }
        /// <summary>
        ///   Validates the range of an integer value.
        /// </summary>
        /// <param name="value">
        ///   The integer to test.
        /// </param>
        /// <param name="minValue">
        ///   The minimum valid value.
        /// </param>
        /// <param name="maxValue">
        ///   The maximum valid value.
        /// </param>
        /// <returns>
        ///   Error prompt string if the value falls outside the given range, otherwise an empty string.
        /// </returns>
        string ValidateIntegerRange(int value, int minValue, int maxValue)
        {
            if (value < minValue || value > maxValue)
            {
                return String.Format("Enter a number between {0} and {1} > ", minValue, maxValue);
            }
            return "";
        }
        /// <summary>
        ///   Validates the priority entered by the user.  Must be between 1 and 
        ///   the number of tasks, unless adding is true, then it can be as
        ///   much as one more than the number of tasks.
        /// </summary>
        /// <param name="tasks">
        ///   The task list.
        /// </param>
        /// <param name="priority">
        ///   Priority number to validate.
        /// </param>
        /// <param name="adding">
        ///   If true, then an item is being added, so accept one more than the
        ///   number of items so it can be added one past the last item.
        /// </param>
        /// <returns>
        ///   An error prompt if the validation fails, otherwise an empty string.
        /// </returns>
        string ValidatePriority(Tasks tasks, int priority, bool adding)
        {
            return ValidateIntegerRange(priority, 0, tasks.Count + (adding ? 1 : 0));
        }
        /// <summary>
        ///   Validates the task entered by the user.
        /// </summary>
        /// <param name="task">
        ///   User input to validate.
        /// </param>
        /// <returns>
        ///   Empty string (all strings are valid)
        /// </returns>
        string ValidateTaskName(string task)
        {
            return String.Empty;
        }
    }
}
