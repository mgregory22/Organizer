using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSG.Console
{
    /// <summary>
    ///   The responsibilities of this class are:
    ///   1. Generate the display of a shortcut and description.
    ///   2. Receive a user-entered shortcut and if it matches the 
    ///      shortcut defined by the menu item, perform the action(s)
    ///      stated in the description.  The action can be a function
    ///      or submenu.
    /// </summary>
    public class MenuItem
    {
        string description;
        ConsoleKey keystroke;
        int maxWidth;
        List<string> lines;

        virtual public string Description
        {
            get { return description; }
            set
            {
                description = value;
                UpdateLines();
            }
        }

        virtual public ConsoleKey Keystroke
        {
            get { return keystroke; }
            set
            {
                keystroke = value;
                UpdateLines();
            }
        }

        virtual public int LineCount
        {
            get { return lines.Count; }
        }

        virtual public int MaxWidth
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

        virtual public string ToString(int index = 0)
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

        virtual public void WrapSplit(string s, int maxWidth, List<string> lines)
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

}
