//
// MSGTest/IO/TestPrint.cs
//

using MSG.IO;

namespace MSGTest.IO
{
    public class TestPrint : Print
    {
        int bufferWidth = 80;
        bool isCursorVisible = true;
        ConsolePos pos;
        string output;

        public TestPrint()
        {
            pos = new ConsolePos(0, 0);
        }

        override public int BufferWidth
        {
            get { return bufferWidth; }
            set { bufferWidth = value; }
        }

        override public void Char(char c)
        {
            output += c;
            pos.left++;
            if (pos.left > bufferWidth)
            {
                pos.left -= bufferWidth;
                pos.top++;
            }
        }

        override public void CharNL(char c)
        {
            Char(c);
            Newline();
        }

        public void ClearOutput()
        {
            output = "";
        }

        override public int CursorLeft
        {
            get { return pos.left; }
            set { /*output += "◄" + pos.left.ToString();*/ pos.left = value; output += "<" + value.ToString(); }
        }

        override public ConsolePos CursorPos
        {
            get { return pos; }
            set { SetCursorPos(value.left, value.top); }
        }

        override public int CursorTop
        {
            get { return pos.top; }
            set { /*output += "▲" + pos.top.ToString();*/ pos.top = value; output += "^" + value.ToString(); }
        }

        override public bool IsCursorVisible
        {
            get { return isCursorVisible; }
            set { isCursorVisible = value; }
        }

        override public void Newline(int n = 1)
        {
            for (int i = 0; i < n; i++)
            {
                output += '\n';
                // Mimic the cursor motion
                pos.left = 0;
                pos.top++;
            }
        }

        public string Output
        {
            get { return output; }
            set { output = value; }
        }

        override public void SetCursorPos(int left, int top)
        {
            CursorLeft = left;
            CursorTop = top;
        }

        override public void String(string s)
        {
            output += s;
            // Mimic the cursor motion
            pos.left += s.Length;
            while (pos.left > bufferWidth)
            {
                pos.left -= bufferWidth;
                pos.top++;
            }
        }

        override public void StringNL(string s)
        {
            String(s);
            Newline();
        }
    }
}
