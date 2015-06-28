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
                    buffer.CursorLeft();
                    view.UpdateCursor();
                }
                else if (IsCursorRight(keyInfo))
                {
                    buffer.CursorRight();
                    view.UpdateCursor();
                }
                else if (IsDelete(keyInfo))
                {
                    buffer.Delete();
                    view.RedrawEditor();
                }
                else if (IsEnd(keyInfo))
                {
                    // Order reversed because the cursor position needs to come
                    // from the view . . . the buffer knows nothing of lines.
                    int pos = view.CursorEnd();
                    buffer.CursorMove(pos);
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
                    // Order reversed because the cursor position needs to come
                    // from the view . . . the buffer knows nothing of lines.
                    int pos = view.CursorHome();
                    buffer.CursorMove(pos);
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
