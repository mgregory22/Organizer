using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Priorities
{
    public class Menu
    {

    }

    /// <summary>
    ///   The responsibility of this class is to hold a (keystroke, description) pair,
    ///   draw them on the screen in a user-friendly way, accept an input keystroke,
    ///   and if the input keystroke equals the keystroke, perform the commands as
    ///   stated in the description.
    /// </summary>
    public class MenuItem
    {
        string description;
        ConsoleKey keystroke;
        int maxWidth;
        List<string> lines;

        private void CalcLines()
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

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                CalcLines();
            }
        }

        public ConsoleKey Keystroke
        {
            get { return keystroke; }
            set
            {
                keystroke = value;
                CalcLines();
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
                CalcLines();
            }
        }

        public MenuItem(string description, ConsoleKey keystroke, int maxWidth = 80)
        {
            this.description = description;
            this.keystroke = keystroke;
            this.maxWidth = maxWidth;
            this.lines = new List<string>();
            CalcLines();
        }

        public string ToString(int index = 0)
        {
            return lines[index];
        }

        public void WrapSplit(string s, int maxWidth, List<string> lines)
        {
            while (s.Length > maxWidth)
            {
                int wrapPos = s.LastIndexOf(' ', maxWidth);
                lines.Add(s.Substring(0, wrapPos));
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
            MenuItem menuItem = new MenuItem("Test menu item, test of wrapping text " +
                "within a 40 column area of the screen for a nice display of things and stuff that " +
                "is nice to think about before work when I get there I will work and play.", ConsoleKey.T, width);
            DrawRuler(width);
            for (int i = 0; i < menuItem.LineCount; i++)
                Console.WriteLine(menuItem.ToString(i));
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
