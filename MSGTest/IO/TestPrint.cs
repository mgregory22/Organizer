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

        public override int BufferWidth
        {
            get { return bufferWidth; }
            set { bufferWidth = value; }
        }

        public override void Char(char c)
        {
            output += c;
        }

        public override void CharNL(char c)
        {
            Char(c);
            Newline();
        }

        public void ClearOutput()
        {
            output = "";
        }

        public override int CursorLeft
        {
            get { return pos.left; }
            set { pos.left = value; output += "<" + value.ToString(); }
        }

        public override ConsolePos CursorPos
        {
            get { return pos; }
            set { SetCursorPos(value.left, value.top); }
        }

        public override int CursorTop
        {
            get { return pos.top; }
            set { pos.top = value; output += "^" + value.ToString(); }
        }

        public override bool IsCursorVisible
        {
            get { return isCursorVisible; }
            set { isCursorVisible = value; }
        }

        public override void Newline(int n = 1)
        {
            for (int i = 0; i < n; i++) output += '\n';
        }

        public string Output
        {
            get { return output; }
            set { output = value; }
        }

        public override void SetCursorPos(int left, int top)
        {
            CursorLeft = left;
            CursorTop = top;
        }

        public override void String(string s)
        {
            output += s;
        }

        public override void StringNL(string s)
        {
            String(s);
            Newline();
        }
    }
}
