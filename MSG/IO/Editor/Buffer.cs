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
            /// <summary>
            ///   Insertion point
            /// </summary>
            int point;

            /// <summary>
            ///   Text being edited
            /// </summary>
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
            /// <param name="text">
            ///   Text to edit
            /// </param>
            /// <param name="point">
            ///   Initial insertion point
            /// </param>
            public Buffer(string text, int point)
            {
                Text = text;
                Point = point;
            }

            /// <summary>
            ///   Moves the point right one character, ensuring it can go
            ///   just to the end of the text and no further.
            /// </summary>
            public void AdvancePoint()
            {
                if (Point < Text.Length)
                {
                    Point++;
                }
            }

            /// <summary>
            ///   Removes all the text and resets the point.
            /// </summary>
            public void Clear()
            {
                Text = "";
                Point = 0;
            }

            /// <summary>
            ///   Deletes the character at the point.
            /// </summary>
            public void Delete()
            {
                if (Point < Text.Length)
                {
                    Text = Text.Remove(Point, 1);
                }
            }

            public char GetChar(int p)
            {
                return Text[p];
            }

            /// <summary>
            ///   Inserts the given character at the point.
            /// </summary>
            /// <param name="c">
            ///   Character to insert
            /// </param>
            public void Insert(char c)
            {
                Text = Text.Insert(Point++, c.ToString());
            }

            /// <summary>
            ///   Returns true if the text is empty.
            /// </summary>
            public bool IsEmpty()
            {
                return Text.Length == 0;
            }

            /// <summary>
            ///   Moves the point to the given position.
            /// </summary>
            /// <param name="point">
            ///   New insertion point
            /// </param>
            public void MovePoint(int point)
            {
                if (point < 0)
                    throw new ArgumentOutOfRangeException("Insertion point must be nonnegative");
                if (point > Text.Length)
                    throw new ArgumentOutOfRangeException("Insertion point cannot be set past end of text");
                Point = point;
            }

            /// <summary>
            ///   Gets or sets the insertion point.
            /// </summary>
            public int Point
            {
                get { return point; }
                set { point = value; }
            }

            /// <summary>
            ///   Moves the point left one character, ensuring it can go
            ///   just to the beginning of the text and no further.
            /// </summary>
            public void RetreatPoint()
            {
                if (Point > 0)
                {
                    Point--;
                }
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
            public override string ToString()
            {
                return Text;
            }
        }
    }
}
