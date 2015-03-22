using MSG.Patterns;
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
        Command action;
        string description;
        char keystroke;
        int maxWidth;
        List<string> lines;

        /// <summary>
        ///   An object that encapsulates the action to perform when the menu item is selected.
        /// </summary>
        virtual public Command Action
        {
            get { return action; }
            set { action = value; }
        }
        /// <summary>
        ///   Name or short description of the menu item.
        /// </summary>
        virtual public string Description
        {
            get { return description; }
            set
            {
                description = value;
                UpdateLines();
            }
        }
        /// <summary>
        ///   Performs the menu item action if the given keystroke matches
        ///   the menu item keystroke.  The intention is to loop through
        ///   a list of menu items and pass the user input to each item,
        ///   terminating the loop when true is returned.
        /// </summary>
        /// <param name="keystroke"></param>
        /// <returns>
        ///   True if there was a match and the action was executed, whether
        ///   the action was successful or not.
        /// </returns>
        public bool ExecuteActionIfKeystrokeMatches(char keystroke)
        {
            if (keystroke == this.keystroke)
            {
                this.action.Execute();
                return true;
            }
            return false;
        }
        /// <summary>
        ///   The keystroke to activate the menu item.
        /// </summary>
        virtual public char Keystroke
        {
            get { return keystroke; }
            set
            {
                keystroke = value;
                UpdateLines();
            }
        }
        /// <summary>
        ///   The number of lines in the description when it's word
        ///   wrapped according to the MaxWidth property.
        /// </summary>
        virtual public int LineCount
        {
            get { return lines.Count; }
        }
        /// <summary>
        ///   The amount of horizontal space available to print the
        ///   description.
        /// </summary>
        virtual public int MaxWidth
        {
            get { return maxWidth; }
            set
            {
                maxWidth = value;
                UpdateLines();
            }
        }
        /// <summary>
        ///   Initializes a menu item object.
        /// </summary>
        /// <param name="keystroke"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="maxWidth"></param>
        public MenuItem(char keystroke, Command action, string description, int maxWidth = 80)
        {
            this.action = action;
            this.description = description;
            this.keystroke = keystroke;
            this.maxWidth = maxWidth;
            this.lines = new List<string>();
            UpdateLines();
        }
        /// <summary>
        ///   Returns the string representation of the menu item.  If the
        ///   description is long enough to be wrapped, an index less than
        ///   LineCount can be given to retrieve the associated line of text.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        virtual public string ToString(int index = 0)
        {
            return lines[index];
        }
        /// <summary>
        ///   Calculates the line breaks of the display.
        /// </summary>
        /// <remarks>
        ///   I could have called this just once in ToString(), but then it
        ///   would calculating all the lines for every line printed and that
        ///   seemed like a waste.  So to work around that, I would have to
        ///   create a dirty flag and update the flag every place I currently
        ///   call UpdateLines(), so the winner is calling UpdateLines() 
        ///   every place a change happens.
        /// </remarks>
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
        /// <summary>
        ///   Splits a string (s) based on line length (maxWidth) and word wrap
        ///   and inserts the lines into the given string list (lines).
        /// </summary>
        /// <param name="s"></param>
        /// <param name="maxWidth"></param>
        /// <param name="lines"></param>
        /// <remarks>
        ///   This method has nothing to do with menu items per se and should
        ///   probably be moved to a more general lib.
        /// </remarks>
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
