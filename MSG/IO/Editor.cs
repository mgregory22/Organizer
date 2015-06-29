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
                if (IsPrintable(keyInfo))
                {
                    // insert any of the printable keys
                    buffer.Insert(keyInfo.KeyChar);
                    view.RedrawEditor();
                }
                else if (IsShiftEnter(keyInfo))
                {
                    buffer.Insert('\n');
                    view.RedrawEditor();
                }
                else if (IsBackspace(keyInfo))
                {
                    buffer.Backspace();
                    view.RedrawEditor();
                }
                else if (IsCursorLeft(keyInfo))
                {
                    buffer.RetreatPoint();
                    view.UpdateCursor();
                }
                else if (IsCursorRight(keyInfo))
                {
                    buffer.AdvancePoint();
                    view.UpdateCursor();
                }
                else if (IsDelete(keyInfo))
                {
                    buffer.Delete();
                    view.RedrawEditor();
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
            }
            return buffer.ToString();
        }
    }
}
