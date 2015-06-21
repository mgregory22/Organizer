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
            ///   Converts the position of a character in the buffer to a console cursor position.
            /// </summary>
            /// <param name="bufferPos">
            ///   Buffer position
            /// </param>
            /// <returns>
            ///   The (left, top) position within the editor (eg (0,0) is the first column on the
            ///   first line, etc)
            /// </returns>
            public ConsolePos BufferPosToEditorPos(int bufferPos)
            {
                int top = this.FindLastIndex(
                    line => bufferPos >= line.StartIndex && bufferPos <= line.StartIndex + line.Length
                );
                // This is tricky here . . . If the line wrapping is hiding a space and bufferPos
                // is on that space, then the cursor position returned will be out of the window
                // and cause an exception.  The cursor needs to be moved to the last nonwhitespace
                // character on the previous line.  But that means the left cursor key will skip
                // over a character.  Therefore, this whitespace skipping thing is not going to
                // work.  R.I.P.
                return new ConsolePos(bufferPos - this[top].StartIndex, top);
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

            public bool IsCharAtIAHardLineReturnChar(char charAtI)
            {
                return charAtI == '\n' || charAtI == '\r';
            }

            public bool IsIAtWindowBreak(int lineLen, char charAtI)
            {
                return lineLen == this.lineWidths[base.Count];
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
                int lineEnd = 0;
                // Save the cursor position of last scanned word break
                int lastWordBreak = 0;
                // Scan through all cursor positions
                for (int i = 0; i < textLen; i++)
                {
                    // lineEnd is the end of the current line, which is just another name for i
                    lineEnd = i;
                    // Shortcut
                    char charAtI = text[i];
                    // I is just after a word?
                    if (IsIOnAWordBreak(charBeforeI, charAtI))
                    {
                        // Save word break position
                        lastWordBreak = i;
                    }
                    // Length of line up to i
                    int lineLen = lineEnd - lineStart;
                    // text[i] is a hard line return?
                    if (IsCharAtIAHardLineReturnChar(charAtI))
                    {
                        // If whitespace precedes the hard return, don't put it at
                        // the end of the line
                        if (char.IsWhiteSpace(charBeforeI))
                            lineEnd = lastWordBreak;
                        // Break line for hard return
                        AddLine(lineStart, lineEnd);
                        // Advance cursor past line return for next bol
                        lineEnd = i = Scan.SkipHardReturn(text, i);
                        // Next bol
                        lineStart = i;
                        // Save word break position
                        lastWordBreak = lineStart;
                    }
                    // Reached window break?
                    else if (IsIAtWindowBreak(lineLen, charAtI))
                    {
                        // Found word break during scan?
                        if (lastWordBreak > lineStart)
                        {
                            // Back up cursor to last word break
                            lineEnd = i = lastWordBreak;
                            // Set eol at word break
                            lineLen = lineEnd - lineStart;
                        }
                        // Add the line indexes
                        AddLine(lineStart, lineEnd);
                        // Advance cursor through whitespace to set next bol
                        lineEnd = i = Scan.SkipWhiteSpace(text, i);
                        // Next bol
                        lineStart = lineEnd;
                        // Save word break position
                        lastWordBreak = lineStart;
                    }
                    charBeforeI = charAtI;
                }
                lineEnd = textLen;
                // Add the last line, whatever it is
                AddLine(lineStart, lineEnd);
                // Okay, one more...add a blank line if the cursor wrapped around
                // but no text did, so that the scrolled line count is correct.
                if (lineEnd - lineStart == lineWidths[base.Count - 1])
                {
                    lineStart = textLen;
                    AddLine(lineStart, textLen);
                }
                // Update dewrap
                dewrap = base.Count < previousLineCnt;
            }
        }
    }
}
