using MSG.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSG.IO
{
    public partial class Editor
    {
        /// <summary>
        ///   Console editor memory buffer class.
        /// </summary>
        public class Buffer
        {
            int cursor;
            string text;

            /// <summary>
            ///   Initializes a buffer for creating new text.
            /// </summary>
            public Buffer()
            {
                Clear();
            }

            /// <summary>
            ///   Initializes a memory buffer for editing previously
            ///   existing text and for testing.
            /// </summary>
            /// <param name="text">Text to edit</param>
            /// <param name="cursor">Initial cursor position</param>
            public Buffer(string text, int cursor)
            {
                Text = text;
                Cursor = cursor;
            }

            /// <summary>
            ///   Deletes the character before the cursor position.
            /// </summary>
            public void Backspace()
            {
                if (Cursor > 0)
                {
                    Text = Text.Remove(--Cursor, 1);
                }
            }

            /// <summary>
            ///   Removes all the text and resets the cursor.
            /// </summary>
            public void Clear()
            {
                Cursor = 0;
                Text = "";
            }

            /// <summary>
            ///   Gets or sets the cursor position.
            /// </summary>
            public int Cursor
            {
                get { return cursor; }
                set { cursor = value; }
            }

            /// <summary>
            ///   Moves the cursor left one character, ensuring it can go
            ///   just to the beginning of the text and no further.
            /// </summary>
            public void CursorLeft()
            {
                if (Cursor > 0)
                {
                    Cursor--;
                }
            }

            /// <summary>
            ///   Moves the cursor to the given position.
            /// </summary>
            /// <param name="pos">
            ///   Cursor position
            /// </param>
            public void CursorMove(int pos)
            {
                if (pos < 0)
                    throw new ArgumentOutOfRangeException("Cursor position must be nonnegative");
                if (pos > Text.Length)
                    throw new ArgumentOutOfRangeException("Cursor position cannot be set past end of text");
                Cursor = pos;
            }

            /// <summary>
            ///   Moves the cursor right one character, ensuring it can go
            ///   just to the end of the text and no further.
            /// </summary>
            public void CursorRight()
            {
                if (Cursor < Text.Length)
                {
                    Cursor++;
                }
            }

            /// <summary>
            ///   Deletes the character at the cursor position.
            /// </summary>
            public void Delete()
            {
                if (Cursor < Text.Length)
                {
                    Text = Text.Remove(Cursor, 1);
                }
            }

            public char GetChar(int p)
            {
                return Text[p];
            }

            /// <summary>
            ///   Inserts the given character at the cursor position.
            /// </summary>
            /// <param name="c">Character to insert</param>
            public void Insert(char c)
            {
                Text = Text.Insert(Cursor++, c.ToString());
            }

            /// <summary>
            ///   Returns true if the line is empty.
            /// </summary>
            /// <returns>True if the line is empty</returns>
            public bool IsEmpty()
            {
                return Text.Length == 0;
            }

            /// <summary>
            ///   Gets or sets the edited text.
            /// </summary>
            public string Text
            {
                get { return text; }
                set { text = value; }
            }

            /// <summary>
            ///   Returns the text being edited.
            /// </summary>
            /// <returns>Text being edited.</returns>
            public override string ToString()
            {
                return Text;
            }
        }
    }
}
