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
        public class WordWrapper : List<LineDisplaySubstring>
        {
            ConsolePos cursorPos;
            Editor.Buffer buffer;
            EndlessArray<int> lineWidths;
            bool dewrap;
            int linesScrolledFromWrapping;

            /// <summary>
            ///   Scans the given text with respect to the given window width
            ///   and determines the substring coordinates needed to wordwrap
            ///   the text in the window.
            /// </summary>
            /// <param name="buffer">
            ///   The memory buffer for the text being edited.  Should be
            ///   treated as read-only.  Do not modify this object within
            ///   this class.
            /// </param>
            /// <param name="lineWidths">
            ///   The maximum width of each line in characters.  If there are
            ///   more lines than there are entries in lineWidths, then the
            ///   last entry is used for subsequent lines.
            /// </param>
            public WordWrapper(Editor.Buffer buffer, EndlessArray<int> lineWidths)
            {
                this.buffer = buffer;
                this.cursorPos = new ConsolePos(0, 0);
                this.dewrap = false;
                this.lineWidths = lineWidths;
                this.linesScrolledFromWrapping = 0;
                Update();
            }

            public void AddLine(int lineStart, int lineEnd)
            {
                base.Add(new LineDisplaySubstring(buffer.Text, lineStart, lineEnd - lineStart));
                linesScrolledFromWrapping = Math.Max(linesScrolledFromWrapping, base.Count - 1);
            }

            /// <summary>
            ///   Returns or moves the cursor position.
            /// </summary>
            public ConsolePos CursorPos
            {
                get { return cursorPos; }
                set {
                    cursorPos.Left = value.Left;
                    cursorPos.Top = value.Top;
                }
            }

            /// <summary>
            ///   This is set to true when the last operation reduced the
            ///   number of lines to display (ie the previously last line
            ///   needs to be erased).
            /// </summary>
            public bool Dewrap
            {
                get { return dewrap; }
            }

            /// <summary>
            ///   Returns the buffer position of the end of the
            ///   current line (for the End cursor key).
            /// </summary>
            public int EndPosOfCurrentLine
            {
                get { return this[this.CursorPos.Top].Length; }
            }

            public bool IsCharAtIAHardLineReturnChar(char charAtI)
            {
                return charAtI == '\n' || charAtI == '\r';
            }

            /// <summary>
            ///   Returns true if the pointer at i is on a word break
            ///   (ie char i is a space and char i-1 is a letter).
            /// </summary>
            /// <param name="charBeforeCursor">
            ///   Character before the cursor
            /// </param>
            /// <param name="charAtCursor">
            ///   Character at the cursor
            /// </param>
            /// <returns>
            ///   True if cursor is on a word break
            /// </returns>
            public bool IsIOnAWordBreak(char charBeforeI, char charAtI)
            {
                return !char.IsWhiteSpace(charBeforeI) && char.IsWhiteSpace(charAtI);
            }

            /// <summary>
            ///   The maximum number of lines that were needed for editing, minus one
            /// </summary>
            public int LinesScrolledFromWrapping
            {
                get { return linesScrolledFromWrapping; }
            }

            /// <summary>
            ///   Returns the length of the buffer text.
            /// </summary>
            public int TextLength
            {
                get { return buffer.Text.Length; }
            }

            /// <summary>
            ///   Performs the word wrapping algorithm and creates
            ///   the list of buffer substring (start, length) pairs
            ///   for each line to display.
            /// </summary>
            public void Update()
            {
                // Shortcuts
                string text = buffer.Text;
                int textLen = buffer.Text.Length;
                char charBeforeI = '\n';
                // Sanity check
                if (buffer.Cursor < 0 || buffer.Cursor > textLen)
                    throw new IndexOutOfRangeException(String.Format("Cursor cannot move outside text: Position {0}", buffer.Cursor));
                // Set dewrap flag at the end of this method if this.Count < previousLineCnt.
                // This means the user deleted enough characters to reduce the number of lines, so
                // the lost line needs to be erased.
                int previousLineCnt = base.Count;
                // Clear previous calculations
                base.Clear();
                // Start index of first line is always 0
                int lineStart = 0;
                // Save the cursor position of last scanned word break
                int lastWordBreak = 0;
                // Scan through all cursor positions
                for (int i = 0; i < textLen; i++)
                {
                    // Shortcut
                    char charAtI = text[i];
                    // I is just after a word?
                    if (IsIOnAWordBreak(charBeforeI, charAtI))
                    {
                        // Save word break position
                        lastWordBreak = i;
                    }
                    // Length of line up to i
                    int lineLen = i - lineStart; // + 1 here gives it a different feel (cursor is never on a
                                                 // different line than the character just typed, and there's
                                                 // always one space to represent the collapsed space at the
                                                 // line wrap)
                    // text[i] is a hard line return?
                    if (IsCharAtIAHardLineReturnChar(charAtI))
                    {
                        // If whitespace precedes the hard return, don't put it at
                        // the end of the line
                        int lineEnd = char.IsWhiteSpace(charBeforeI) ? lastWordBreak : i;
                        // Break line for hard return
                        AddLine(lineStart, lineEnd);
                        // Advance cursor past line return for next bol
                        i = Scan.SkipHardReturn(text, i);
                        // Next bol
                        lineStart = i;
                        // Save word break position
                        lastWordBreak = i;
                    }
                    // Reached window break?
                    else if (lineLen == lineWidths[base.Count])
                    {
                        int lineEnd = lastWordBreak > lineStart ? lastWordBreak : i;
                        // Found word break during scan?
                        if (lastWordBreak > lineStart)
                        {
                            // Back up cursor to last word break
                            i = lastWordBreak;
                            // Set eol at word break
                            lineLen = lastWordBreak - lineStart;
                        }
                        // Add the line indexes
                        AddLine(lineStart, lineEnd);
                        // Advance cursor through whitespace to set next bol
                        i = Scan.SkipWhiteSpace(text, i);
                        // Next bol
                        lineStart = i;
                        // Save word break position
                        lastWordBreak = i;
                    }
                    // Set screen cursor position based on buffer cursor position
                    if (i == buffer.Cursor)
                    {
                        CursorPos.Left = lineLen;
                        CursorPos.Top = base.Count;
                    }
                    charBeforeI = charAtI;
                }
                // Set screen cursor position based on buffer cursor position
                if (buffer.Cursor == textLen)
                {
                    CursorPos.Left = textLen - lineStart;
                    CursorPos.Top = base.Count;
                }
                // Add the last line, whatever it is
                AddLine(lineStart, textLen);
                // Update dewrap
                dewrap = base.Count < previousLineCnt;
            }
        }
    }
}
