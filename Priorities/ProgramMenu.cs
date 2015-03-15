using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colored = MSGLib.Colored;
using ColoredConsole = MSGLib.Colored.Console;
using Menu = MSGLib.Colored.Console.Menu;
using MenuItem = MSGLib.Colored.Console.Menu.Item;
using MenuItems = MSGLib.Colored.Console.Menu.Item.List;
using Table = MSGLib.Colored.Console.Table;
using TableCols = MSGLib.Colored.Console.Table.Col.List;
using TableRow = MSGLib.Colored.Console.Table.Col.List.Row;
using TableRows = MSGLib.Colored.Console.Table.Col.List.Row.List;

namespace Priorities
{
    class ProgramMenu
    {
        /// <summary>
        ///   The list of items for the program menu.
        /// </summary>
        private MenuItems _items;
        /// <summary>
        ///   
        /// </summary>
        /// <param name="options"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        Options EditOptions(Options options, ColoredConsole con)
        {
            var optionsMenu = new OptionsMenu(options, con);
            optionsMenu.PRELoop(options, con);
            return options;
        }
        /// <summary>
        ///   Run menu print/read/execute loop.
        /// </summary>
        /// <param name="options">
        ///   Program options.
        /// </param>
        /// <param name="con">
        ///   The console on which to print prompts and error messages.
        /// </param>
        /// <param name="tasks">
        ///   The task list.
        /// </param>
        public void PRELoop(Options options, ColoredConsole con, Tasks tasks)
        {
            var menu = new Menu();
            do
            {
                if ((bool)options["AutoList"]) TasksList(tasks, con);
                con = menu.PRELoop(_items, con);
            } while (menu.Selection != ConsoleKey.Q);
        }
        /// <summary>
        ///   Creates the menu _items.
        /// </summary>
        /// <param name="options">
        ///   Program options.
        /// </param>
        /// <param name="con">
        ///   The console on which to print prompts and error messages.
        /// </param>
        /// <param name="tasks">
        ///   The task list.
        /// </param>
        public ProgramMenu(Options options, ColoredConsole con, Tasks tasks)
        {
            _items = new MenuItems();
            _items.ItemAdd(new MenuItem(ConsoleKey.A, "Add Task", () => TaskAdd(tasks, con)));
            _items.ItemAdd(new MenuItem(ConsoleKey.D, "Delete Task", () => TaskDelete(tasks, con)));
            if (!(bool)options["AutoList"])
            {
                _items.ItemAdd(new MenuItem(ConsoleKey.L, "List Tasks", () => TasksList(tasks, con)));
            }
            _items.ItemAdd(new MenuItem(ConsoleKey.M, "Move Task/Change Priority", () => TaskMove(tasks, con)));
            _items.ItemAdd(new MenuItem(ConsoleKey.O, "Options", () => options = EditOptions(options, con)));
            _items.ItemAdd(new MenuItem(ConsoleKey.R, "Rename Task", () => TaskRename(tasks, con)));
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
        ///   number of _items so it can be added one past the last item.
        /// </param>
        /// <returns></returns>
        public int PromptForPriority(string prompt, Tasks tasks, ColoredConsole con, bool adding)
        {
            prompt = string.Format("{0} [1..{1}, 0 to cancel] > ", prompt, tasks.Count + (adding ? 1 : 0));
            int priority = 1;
            if (tasks.Count > 0)
            {
                priority = con.PromptForInt(prompt, ConsoleColor.Green, (int i) => ValidatePriority(tasks, i, adding));
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
        void TaskAdd(Tasks tasks, ColoredConsole con)
        {
            // Prompt for task name
            string task = con.PromptForString("Enter task name > ", ConsoleColor.Green, ValidateTaskName);
            // Abort if the user enters a blank task
            if (task == string.Empty)
            {
                return;
            }
            // Prompt for priority (if applicable)
            int priority = PromptForPriority("Enter task priority", tasks, con, true);
            tasks.Add(task, priority);
            con.PrintColored(string.Format("Task \"{0}\" added.", task), ConsoleColor.White, true);
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
        void TaskDelete(Tasks tasks, ColoredConsole con)
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
            con.PrintColored(string.Format("Task \"{0}\" deleted.", task), ConsoleColor.White, true);
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
        void TaskMove(Tasks tasks, ColoredConsole con)
        {
            TasksList(tasks, con);
            int priorityTaskSrc = PromptForPriority("Enter priority of item to move", tasks, con, false);
            if (priorityTaskSrc == 0) return;
            int priorityNew = PromptForPriority("Enter new priority of item", tasks, con, false);
            if (priorityNew == 0) return;
            tasks.PriorityTaskChange(priorityTaskSrc, priorityNew);
            con.PrintColored(string.Format("Task \"{0}\" priority changed.", tasks.Get(priorityTaskSrc)), ConsoleColor.White, true);
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
        void TaskRename(Tasks tasks, ColoredConsole con)
        {
            TasksList(tasks, con);
            int priorityTaskSrc = PromptForPriority("Enter priority of item to rename", tasks, con, false);
            // Cancel if the user enters a blank or zero for the priority
            if (priorityTaskSrc == 0) return;
            // Prompt for task name
            string nameTask = con.PromptForString("Enter task name > ", ConsoleColor.Green, ValidateTaskName);
            // Cancel if the user enters a blank task
            if (nameTask == string.Empty) return;
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
        void TasksList(Tasks tasks, ColoredConsole con)
        {
            List<Colored> colColorses = new List<Colored>();
            colColorses.Add(new Colored(ConsoleColor.Yellow));
            colColorses.Add(new Colored(ConsoleColor.Cyan));
            TableCols cols = new TableCols(colColorses);
            TableRows rows = new TableRows();
            tasks.ForEach((string task, int priority) => {
                rows.Add(new TableRow(cols, "  " + (priority--).ToString(), task));
                return true;
            });
            cols.PaddingAdd(2);
            rows.Print(cols, con);
        }
        /// <summary>
        ///   Validates the _range of an integer value.
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
        ///   Error prompt string if the value falls outside the given _range, otherwise an empty string.
        /// </returns>
        string ValidateIntegerRange(int value, int minValue, int maxValue)
        {
            if (value < minValue || value > maxValue)
            {
                return String.Format("Enter a number between {0} and {1} > ", minValue, maxValue);
            }
            return String.Empty;
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
        ///   number of _items so it can be added one past the last item.
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
