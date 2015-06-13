using MSG.Types.Array;
using MSG.Types.String;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MSG.IO
{
    public partial class Editor
    {
        /// <summary>
        ///   Manages the console state for the editor.
        /// </summary>
        public class View
        {
            /// <summary>
            ///   Width of the console
            /// </summary>
            int consoleWidth;
            /// <summary>
            ///   The width of each line in the edit buffer
            /// </summary>
            EndlessArray<int> lineWidths;
            /// <summary>
            ///   Current console cursor position
            /// </summary>
            ConsolePos pos;
            /// <summary>
            ///   The output object
            /// </summary>
            Print print;
            /// <summary>
            ///   The starting position of the console cursor
            /// </summary>
            public ConsolePos start;
            /// <summary>
            ///   Calculates the positions of line breaks, cursor, etc.  This object
            ///   acts as a mediator for the view's interactions with the model. 
            /// </summary>
            public WordWrapper wordWrapper;


            /// <summary>
            ///   Initializes the console state tracking class.
            /// </summary>
            /// <param name="buffer">
            ///   A reference to the editor buffer used to initialize
            ///   the WordWrapper object.  The buffer object is not
            ///   modified within this class or the WordWrapper class.
            /// </param>
            /// <param name="print">
            ///   All output is done through the print object
            /// </param>
            public View(Buffer buffer, Print print)
            {
                this.print = print;
                this.start = new ConsolePos(print.CursorPos);
                this.pos = new ConsolePos(print.CursorPos);
                this.consoleWidth = print.BufferWidth;
                this.lineWidths = new EndlessArray<int>(
                    this.consoleWidth - LineLeft(0),
                    this.consoleWidth - LineLeft(1)
                );
                this.wordWrapper = new WordWrapper(buffer, lineWidths);
            }

            /// <summary>
            ///   Handle backspace key.
            /// </summary>
            public void Backspace()
            {
                RedrawEditor();
            }

            /// <summary>
            ///   Moves the cursor to the end of the line and returns the
            ///   resulting buffer position.
            /// </summary>
            public int CursorEnd()
            {
                return Left = this.start.Left + wordWrapper.EndPosOfCurrentLine;
            }

            /// <summary>
            ///   Moves the cursor to the beginning of the line and returns
            ///   the resulting buffer position.
            /// </summary>
            public int CursorHome()
            {
                return Left = start.Left;
            }

            /// <summary>
            ///   Handles the left cursor key.
            /// </summary>
            public void CursorLeft()
            {
                if (Top == start.Top)
                {
                    if (Left > start.Left)
                        Left--;
                }
                else
                {
                    if (Left > 0)
                        Left--;
                    else
                    {
                        Top--;
                        Left = lineWidths[Top] + (Top == 0 ? start.Left : 0);
                    }
                }
            }

            /// <summary>
            ///   Handles the right cursor key.
            /// </summary>
            public void CursorRight()
            {
                if (Top < wordWrapper.Count)
                    if (Left < wordWrapper.TextLength)
                        Left++;
            }

            /// <summary>
            ///   Deletes the character at the cursor.
            /// </summary>
            public void Delete()
            {
                //System.Console.MoveBufferArea(
                //    Left + 1, Top
                //    , System.Console.BufferWidth - Left - 1, 1
                //    , Left, Top
                //    , ' ', ConsoleColor.Gray, ConsoleColor.Black);
                RedrawEditor();
            }

            /// <summary>
            ///   Insert a character into the current line on the console, moving
            ///   other characters forward or breaking the line if necessary.
            /// </summary>
            public void Insert()
            {
                // Move chars over to make room
                //System.Console.MoveBufferArea(Left, Top, System.Console.BufferWidth - 1 - Left, 1, Left + 1, Top);
                // Put char onto display
                //print.Char(model.GetChar(model.Cursor - 1));
                RedrawEditor();
            }

            /// <summary>
            ///   Gets or sets cursor left and updates console cursor.
            /// </summary>
            public int Left
            {
                get { return pos.Left; }
                set
                {
                    if (value < consoleWidth)
                        pos.Left = value;
                    else
                    {
                        pos.Left = value - consoleWidth;
                        Top++;
                    }
                    SetCursorPos(pos);
                }
            }

            /// <summary>
            ///   The cursor left coordinate of the start of line lineIndex.
            /// </summary>
            /// <param name="lineIndex">
            ///   Index of the line whose left coordinate to return
            /// </param>
            /// <returns>
            ///   Left coordinate of the given line
            /// </returns>
            public int LineLeft(int lineIndex)
            {
                if (lineIndex == 0)
                    return this.start.Left;
                return 0;
            }

            /// <summary>
            ///   Updates the console when the text has changed.
            /// </summary>
            public void RedrawEditor()
            {
                // Hide cursor during redraw
                print.IsCursorVisible = false;
                // Start at the beginning
                SetCursorPos(start);
                // Calculate word wrapping
                wordWrapper.Update();
                // Loop through each line returned by the word wrapper
                for (int i = 0; i < wordWrapper.Count; i++)
                {
                    // Get next word-wrapped line
                    var s = wordWrapper[i].ToString();
                    print.String(s);
                    // Calc right padding length (do not scroll the window with the padding)
                    int padLen = lineWidths[i] - s.Length - 1;
                    // If lines have been added (and possibly erased) after this line, then
                    // pad all the way to the right edge of the window
                    if (i < wordWrapper.LinesScrolledFromWrapping)
                    {
                        padLen++;
                    }
                    if (padLen > 0)
                    {
                        // Right pad the line to erase any lingering text from backspacing
                        string padding = Format.Padding(padLen);
                        print.String(padding);
                    }
                }
                if (wordWrapper.Dewrap)
                {
                    print.String(Format.Padding(lineWidths[wordWrapper.Count]));
                }
                // Position cursor with respect to wrapping
                pos.Left = LineLeft(wordWrapper.CursorPos.Top) + wordWrapper.CursorPos.Left;
                pos.Top = start.Top + wordWrapper.CursorPos.Top;
                if (pos.Left >= consoleWidth)
                {
                    pos.Left -= consoleWidth;
                    pos.Top++;
                }
                SetCursorPos(pos);
                print.IsCursorVisible = true;
            }

            /// <summary>
            ///   Prints a newline after the user is done typing.
            /// </summary>
            public void Return()
            {
                print.Newline();
            }

            /// <summary>
            ///   Sets the cursor position to coordinate (l, t).
            /// </summary>
            private void SetCursorPos(ConsolePos pos)
            {
                if (pos.Left >= consoleWidth)
                    throw new ArgumentOutOfRangeException("Invalid cursor left position: " + pos.Left);
                print.CursorPos = pos;
            }

            /// <summary>
            ///   Gets or sets cursor top and updates console cursor.
            /// </summary>
            public int Top
            {
                get { return pos.Top; }
                set
                {
                    pos.Top = value;
                    SetCursorPos(pos);
                }
            }
        }
    }
}
