//
// Wordwrapper.cs
//

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
        public class WordWrapper
        {
            bool dewrap;
            
            /// <summary>
            ///   Marks the buffer position of the end of each line exclusive.  So a
            ///   first lineBreak of 6 would put the first char of the next line at
            ///   position 6 and the first line would be length 6.
            /// </summary>
            List<int> lineBreaks;

            int linesScrolledFromWrapping;

            EndlessArray<int> lineWidths;

            /// <summary>
            ///   Scans the given text with respect to the given window width
            ///   and determines the substring coordinates needed to wordwrap
            ///   the text in the window.
            /// </summary>
            /// <param name="text">
            ///   Text to scan for line breaks
            /// </param>
            /// <param name="point">
            ///   The current cursor in the text
            /// </param>
            /// <param name="lineWidths">
            ///   The maximum width of each line in characters.  If there are
            ///   more lines than there are entries in lineWidths, then the
            ///   last entry is used for subsequent lines.
            /// </param>
            public WordWrapper(string text, EndlessArray<int> lineWidths)
            {
                this.dewrap = false;
                this.lineBreaks = new List<int>();
                this.lineWidths = lineWidths;
                this.linesScrolledFromWrapping = 0;
                Update(text);
            }

            /// <summary>
            ///   Adds a line break to the list.
            /// </summary>
            /// <param name="lineBreak">
            ///   The position of the char right after the line break.
            /// </param>
            private void AddLineBreak(int lineBreak)
            {
                lineBreaks.Add(lineBreak);
                linesScrolledFromWrapping = Math.Max(linesScrolledFromWrapping, lineBreaks.Count - 1);
            }

            /// <summary>
            ///   Converts a point in the buffer to a console cursor position.
            /// </summary>
            /// <param name="point">
            ///   Buffer point
            /// </param>
            /// <returns>
            ///   The (left, top) position within the editor (eg (0,0) is the first column on the
            ///   first line, etc)
            /// </returns>
            public ConsolePos BufferPointToEditorPos(int point)
            {
                int top = lineBreaks.FindIndex(
                    lineBreak => point < lineBreak
                );
                // If the cursor is sitting at the end of the text, then top will be the last line
                if (top == -1)
                {
                    top = lineBreaks.Count - 1;
                }
                ConsolePos editorPos;
                editorPos.left = point - GetLineStart(top);
                // Cursor wraps around if it's at the end of a full line
                if (editorPos.left == lineWidths[top])
                {
                    editorPos.left = 0;
                    top++;
                }
                editorPos.top = top;
                return editorPos;
            }

            /// <summary>
            ///   Returns the number of lines of wrapped text.
            /// </summary>
            public int Count
            {
                get { return lineBreaks.Count; }
            }

            /// <summary>
            ///   This is set to true when the last operation reduced the
            ///   number of lines to display (ie when the previously last
            ///   line needs to be erased).
            /// </summary>
            public bool Dewrap
            {
                get { return dewrap; }
            }

            /// <summary>
            ///   Returns the text of the given wrapped line.
            /// </summary>
            public string GetLine(string text, int wrappedLineIndex)
            {
                int startIndex = GetLineStart(wrappedLineIndex);
                return text.Substring(startIndex, GetLineBreak(wrappedLineIndex) - startIndex);
            }

            /// <summary>
            ///   Returns the break position of the line given by 
            ///   _lineIndex_, which is one past the last character 
            ///   on the line.
            /// </summary>
            public int GetLineBreak(int wrappedLineIndex)
            {
                return lineBreaks[wrappedLineIndex];
            }

            /// <summary>
            ///   Returns the length of the given wrapped line.
            /// </summary>
            public int GetLineLen(int wrappedLineIndex)
            {
                return GetLineBreak(wrappedLineIndex) - GetLineStart(wrappedLineIndex);
            }

            /// <summary>
            ///   Returns the position of the start of the line 
            ///   given by _lineIndex_.
            /// </summary>
            public int GetLineStart(int wrappedLineIndex)
            {
                // The start of the first line is zero.  The start of 
                // any other line is the previous break.
                return wrappedLineIndex == 0 ? 0 : lineBreaks[wrappedLineIndex - 1];
            }

            /// <summary>
            ///   Returns true if the cursor is on a LF or CR char.
            /// </summary>
            private bool IsIAtAHardLineReturnChar(char charAtI)
            {
                return charAtI == '\n' || charAtI == '\r';
            }

            /// <summary>
            ///   Returns true if the cursor is just after a word
            ///   (ie cursor is on a space just after a nonspace).
            /// </summary>
            private bool IsIJustAfterAWord(char charBeforeI, char charAtI)
            {
                return !char.IsWhiteSpace(charBeforeI) && char.IsWhiteSpace(charAtI);
            }

            /// <summary>
            ///   Returns true if the cursor is on the beginning
            ///   of a word (ie cursor is on a nonspace just after
            ///   a space).
            /// </summary>
            private bool IsIOnWordBeginning(char charBeforeI, char charAtI)
            {
                return char.IsWhiteSpace(charBeforeI) && !char.IsWhiteSpace(charAtI);
            }

            /// <summary>
            ///   For symmetry
            /// </summary>
            public bool IsFirstLine(int wrappedLineIndex)
            {
                return wrappedLineIndex == 0;
            }

            /// <summary>
            ///   Returns true if the given line index refers to the last
            ///   wrapped line of input.
            /// </summary>
            public bool IsLastLine(int wrappedLineIndex)
            {
                return wrappedLineIndex == lineBreaks.Count - 1;
            }

            /// <summary>
            ///   Returns true if the length of the wrapped line text is equal
            ///   to the width of the window.
            /// </summary>
            private bool IsWindowLineCompletelyFilled(int wrappedLineLen, int wrappedLineIndex)
            {
                return wrappedLineLen == this.lineWidths[wrappedLineIndex];
            }

            /// <summary>
            ///   The maximum number of lines that were needed for editing, minus one
            /// </summary>
            public int LinesScrolledFromWrapping
            {
                get { return linesScrolledFromWrapping; }
            }

            /// <summary>
            ///   Performs the word wrapping algorithm and creates
            ///   a list of word break positions for each extra line
            ///   that needs to be displayed.
            /// </summary>
            /// <param name="text">
            ///   Text to scan for line breaks
            /// </param>
            public void Update(string text)
            {
                // Shortcuts
                int textLen = text.Length;
                char charBeforeI = '\n';
                // Set dewrap flag at the end of this method if the user deleted 
                // enough characters to reduce the number of lines (ie the lost 
                // line needs to be erased).
                int previousLineCnt = lineBreaks.Count;
                // Clear previous calculations
                lineBreaks.Clear();
                // Start index of first line is always 0
                int lineStart = 0;
                // Save the cursor position of last scanned word break
                int lastWordBreak = 0;
                // Scan through all cursor positions
                for (int i = 0; i < textLen; i++)
                {
                    // Shortcut
                    char charAtI = text[i];
                    // Save the latest line break candidate
                    if (IsIOnWordBeginning(charBeforeI, charAtI))
                    {
                        lastWordBreak = i;
                    }
                    // Length of line so far in the scan
                    int lineLen = i - lineStart;
                    // Break the line unconditionally at a line break char
                    if (IsIAtAHardLineReturnChar(charAtI))
                    {
                        // Advance cursor past line return
                        i = Scan.SkipHardReturn(text, i);
                        // Break line for hard return
                        AddLineBreak(i);
                        // Next start of line
                        lineStart = i;
                        // Save word break position
                        lastWordBreak = lineStart;
                    }
                    // When the window edge is reached, break at a word if possible
                    else if (IsWindowLineCompletelyFilled(lineLen, lineBreaks.Count))
                    {
                        // Found word break during scan?
                        if (lastWordBreak > lineStart)
                        {
                            // Back up cursor to last word break
                            i = lastWordBreak;
                            // Set eol at word break
                            lineLen = i - lineStart;
                        }
                        // Add the line indexes
                        AddLineBreak(i);
                        // Next bol
                        lineStart = i;
                        // Save word break position
                        lastWordBreak = lineStart;
                    }
                    charBeforeI = charAtI;
                    // End of scanning loop
                }
                // Add a break for the end of the text
                AddLineBreak(textLen);
                // If the cursor wrapped around but no text did, then there is
                // another scrolled line.
                if (textLen - lineStart == lineWidths[lineBreaks.Count - 1])
                {
                    linesScrolledFromWrapping++;
                }
                // If the number of lines decreased since the last update, flag it
                dewrap = lineBreaks.Count < previousLineCnt;
            }
        }
    }
}
