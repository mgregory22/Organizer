using MSG.IO;

namespace MSGTest.IO
{
    public class TestPrint : Print
    {
        string output;
        public override void Char(char c, bool nl = false)
        {
            output += c;
            if (nl) output += '\n';
        }
        public override void Newline(int n = 1)
        {
            for (int i = 0; i < n; i++) output += '\n';
        }
        public string Output
        {
            get { string r = output; output = ""; return r; }
            set { output = value; }
        }
        public override void String(string s, bool nl = false)
        {
            output += s;
            if (nl) output += '\n';
        }
    }
}
