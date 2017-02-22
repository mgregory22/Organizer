//
// Priorities/OpenFile.cs
//

using MSG.Patterns;

namespace Priorities
{
    public class OpenFile : Cmd
    {
        public OpenFile()
        {
        }

        public override Result Do()
        {
            return CANTDO;
        }

        public override Result Undo()
        {
            return CANTUNDO;
        }
    }
}
