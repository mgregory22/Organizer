using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSG.IO
{
    /// <summary>
    ///   More featureful console input editor.  
    /// </summary>
    public partial class Editor
    {
        /// <summary>
        ///   Gets one or more lines of input from the user.
        /// </summary>
        /// <param name="print">
        ///   Object used for raw printing
        /// </param>
        /// <param name="read">
        ///   Object used for raw reading
        /// </param>
        public static string GetInput(Print print, Read read)
        {
            Buffer buffer = new Buffer();
            View view = new View(buffer, print);
            bool done = false;

            while (!done)
            {
                ConsoleKeyInfo keyInfo = read.Key();
                done = ProcessKey(keyInfo, buffer, view);
            }
            return buffer.ToString();
        }

        /// <summary>
        ///   Performs the key command action
        /// </summary>
        /// <param name="keyInfo">
        ///   Key command/input
        /// </param>
        /// <param name="buffer">
        ///   Editor buffer
        /// </param>
        /// <param name="view">
        ///   Editor console
        /// </param>
        /// <returns>
        ///   True if the user quit
        /// </returns>
        public static bool ProcessKey(ConsoleKeyInfo keyInfo, Buffer buffer, View view)
        {
            bool done = false;

            if (IsPrintable(keyInfo))
            {
                // insert any of the printable keys
                buffer.Insert(keyInfo.KeyChar);
                view.RedrawEditor(buffer.Text, buffer.Point);
            }
            else if (IsShiftEnter(keyInfo))
            {
                buffer.Insert('\n');
                view.RedrawEditor(buffer.Text, buffer.Point);
            }
            else if (IsBackspace(keyInfo))
            {
                buffer.RetreatPoint();
                buffer.Delete();
                view.RedrawEditor(buffer.Text, buffer.Point);
            }
            else if (IsCursorDown(keyInfo))
            {
                int point = view.CursorDown(buffer.Point);
                buffer.MovePoint(point);
            }
            else if (IsCursorLeft(keyInfo))
            {
                buffer.RetreatPoint();
                view.UpdateCursor(buffer.Point);
            }
            else if (IsCursorRight(keyInfo))
            {
                buffer.AdvancePoint();
                view.UpdateCursor(buffer.Point);
            }
            else if (IsCursorUp(keyInfo))
            {
                int point = view.CursorUp(buffer.Point);
                buffer.MovePoint(point);
            }
            else if (IsDelete(keyInfo))
            {
                buffer.Delete();
                view.RedrawEditor(buffer.Text, buffer.Point);
            }
            else if (IsEnd(keyInfo))
            {
                int point = view.CursorEnd();
                buffer.MovePoint(point);
            }
            else if (IsEnter(keyInfo))
            {
                // ignore enter on empty line
                if (!buffer.IsEmpty())
                {
                    view.ExitEditor();
                    done = true;
                }
            }
            else if (IsEscape(keyInfo))
            {
                buffer.Clear();
                view.ExitEditor();
                done = true;
            }
            else if (IsHome(keyInfo))
            {
                int point = view.CursorHome();
                buffer.MovePoint(point);
            }
            else
            {
                // Ignore everything else
            }
            return done;
        }
    }
}
