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
            }); return items;
        }
        /// <summary>
        ///   Adds a to-do task.
        /// </summary>
        void TaskAdd(Tasks tasks, MSGLib.Console con)
        {
            // Prompt for task to do
            string task = con.PromptForString("Enter task to do > ", ConsoleColor.Green, ValidateTask);
            // Abort if the user enters a blank task
            if (task == String.Empty)
            {
                return;
            }
            // Prompt for priority (if applicable)
            int priority = 1;
            if (tasks.Count > 0)
            {
                priority = con.PromptForInteger("Enter task priority [1.." + (tasks.Count + 1) + "] > ", ConsoleColor.Green, (int i) => ValidatePriorityNew(tasks, i));
            }
            tasks.Add(task, priority);
            con.PrintColored(String.Format("Task \"{0}\" added.", task), ConsoleColor.White, true);
        }
        /// <summary>
        ///   Deletes a to-do task.
        /// </summary>
        /// <param name="con"></param>
        void TaskDelete(Tasks tasks, MSGLib.Console con)
        {
            // Prompt for priority (if any tasks exist)
            int priority = 1;
            if (tasks.Count == 0)
            {
                con.PrintColored("There are no tasks to delete.", ConsoleColor.White, true);
                return;
            }
            priority = con.PromptForInteger("Enter task priority [1.." + (tasks.Count + 1) + "] > ", ConsoleColor.Green, (int i) => ValidatePriorityNew(tasks, i));
            string task = tasks.Get(priority);
            tasks.Delete(priority);
            con.PrintColored(String.Format("Task \"{0}\" deleted.", task), ConsoleColor.White, true);
        }
        /// <summary>
        ///   Changes the priority of a task.
        /// </summary>
        /// <param name="con">
        ///   The console on which to print prompts and error messages.
        /// </param>
        void TaskMove(Tasks tasks, MSGLib.Console con)
        {
            TasksList(tasks, con);
            int priorityTaskSrc = con.PromptForInteger("Enter priority of item to move > ", ConsoleColor.Green, (int i) => ValidatePriorityExisting(tasks, i));
            if (priorityTaskSrc == 0) return;
            int priorityNew = con.PromptForInteger("Enter new priority of item > ", ConsoleColor.Green, (int i) => ValidatePriorityExisting(tasks, i));
            if (priorityNew == 0) return;
            tasks.PriorityTaskChange(priorityTaskSrc, priorityNew);
            con.PrintColored(String.Format("Task \"{0}\" priority changed.", tasks.Get(priorityTaskSrc)), ConsoleColor.White, true);
        }
        /// <summary>
        ///   Prints the to-do tasks.
        /// </summary>
        /// <param name="con"></param>
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
                rows.Add(new MSGLib.Console.Table.Rows.Row(cols, priority--, task));
                return true;
            });
            cols.PaddingAdd(2);
            MSGLib.Console.Table table = new MSGLib.Console.Table();
            table.Print(rows, cols, con);
        }
        /// <summary>
        ///   Creates the menu items.
        /// </summary>
        public TasksMenu(Tasks tasks, MSGLib.Console con)
        {
            var items = ItemsMenuCreate(tasks, con);
            var menu = new MSGLib.Console.Menu();
            while ((con = menu.MenuDo(items, con)) != null) ;
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
        ///   the number of to-do tasks.
        /// </summary>
        /// <param name="priority">
        ///   Priority number to validate.
        /// </param>
        /// <returns>
        ///   An error prompt if the validation fails, otherwise an empty string.
        /// </returns>
        string ValidatePriorityExisting(Tasks tasks, int priority)
        {
            return ValidateIntegerRange(priority, 1, tasks.Count);
        }
        /// <summary>
        ///   Validates the priority entered by the user.  Must be between 1 and 
        ///   1 more than the number of to-do tasks.
        /// </summary>
        /// <param name="priority">
        ///   Priority number to validate.
        /// </param>
        /// <returns>
        ///   An error prompt if the validation fails, otherwise an empty string.
        /// </returns>
        string ValidatePriorityNew(Tasks tasks, int priority)
        {
            return ValidateIntegerRange(priority, 1, tasks.Count + 1);
        }
        /// <summary>
        ///   Validates the to-do task entered by the user.
        /// </summary>
        /// <param name="taskToDo">
        ///   User input to validate.
        /// </param>
        /// <returns>
        ///   Empty string.
        /// </returns>
        string ValidateTask(string taskToDo)
        {
            return "";
        }
    }
}
