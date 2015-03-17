using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Priorities
{
    /// <summary>
    ///   The responsibility of this class is to organize menu items 
    ///   together to create a menu and generate its display.
    /// </summary>
    public class Menu
    {
        MenuItem[] menuItems;

        public Menu(MenuItem[] menuItems)
        {
            this.menuItems = menuItems;
        }

        public override string ToString()
        {
            string s = "";
            foreach (MenuItem menuItem in menuItems)
            {
                for (int i = 0; i < menuItem.LineCount; i++)
                {
                    s += menuItem.ToString(i) + "\n";
                }
            }
            return s;
        }
    }

    /// TODO:
    ///   2. Receive a user-entered shortcut and perform the action(s)
    ///      of the corresponding menu item.
    ///   2. Receive a user-entered shortcut and if it matches the 
    ///      shortcut defined by the menu item, perform the action(s)
    ///      stated in the description.  The action can be a function
    ///      or submenu.

    /// <summary>
    ///   The responsibility of this class is to generate the display of
    ///   a shortcut and description.
    /// </summary>
    public class MenuItem
    {
        string description;
        ConsoleKey keystroke;
        int maxWidth;
        List<string> lines;

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                UpdateLines();
            }
        }

        public ConsoleKey Keystroke
        {
            get { return keystroke; }
            set
            {
                keystroke = value;
                UpdateLines();
            }
        }

        public int LineCount
        {
            get { return lines.Count;  }
        }

        public int MaxWidth
        {
            get { return maxWidth; }
            set
            {
                maxWidth = value;
                UpdateLines();
            }
        }

        public MenuItem(ConsoleKey keystroke, string description, int maxWidth = 80)
        {
            this.description = description;
            this.keystroke = keystroke;
            this.maxWidth = maxWidth;
            this.lines = new List<string>();
            UpdateLines();
        }

        public string ToString(int index = 0)
        {
            return lines[index];
        }

        private void UpdateLines()
        {
            lines.Clear();
            string k = String.Format("[{0}] ", keystroke.ToString());
            // Indent any wrapped description lines
            string p = new String(' ', k.Length);
            int maxDescWidth = maxWidth - p.Length;
            string d = String.Format("{0}", description);
            if (d.Length <= maxDescWidth)
            {
                lines.Add(k + d);
            }
            else
            {
                // Wrap the description
                WrapSplit(d, maxDescWidth, lines);
                // Insert the key and indent prefixes into the description lines
                lines[0] = k + lines[0];
                for (int i = 1; i < lines.Count; i++)
                {
                    lines[i] = p + lines[i];
                }
            }
        }

        public void WrapSplit(string s, int maxWidth, List<string> lines)
        {
            while (s.Length > maxWidth)
            {
                // Find the position of the line break
                int wrapPos = s.LastIndexOf(' ', maxWidth);
                // Add the front of the string to the line list
                lines.Add(s.Substring(0, wrapPos));
                // Remove the front of the string
                s = s.Remove(0, wrapPos + 1);
            }
            lines.Add(s);
        }
    }

    public sealed class Program
    {
        public static void Main(string[] args)
        {
            int width = 40;
            MenuItem[] menuItems = {
                new MenuItem(ConsoleKey.A, "Add Chicken"),
                new MenuItem(ConsoleKey.B, "Broast Chicken"),
                new MenuItem(ConsoleKey.C, "Chill Chicken"),
                new MenuItem(ConsoleKey.T, "Test menu item, test of wrapping text " +
                    "within a 40 column area of the screen for a nice display of things and stuff that " +
                    "is nice to think about before work when I get there I will work and play.", width)
            };
            Menu menu = new Menu(menuItems);
            DrawRuler(width);
            Console.WriteLine(menu.ToString());
            Console.ReadKey(true);
        }

        public static void DrawRuler(int width)
        {
            for (int i = 1; i <= width; i++)
                Console.Write(i % 10 > 0 ? "-" : (i / 10).ToString());
            Console.WriteLine();
        }
    }
}
