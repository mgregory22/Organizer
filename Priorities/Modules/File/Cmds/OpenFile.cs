//
// Priorities/Modules/File/Cmds/OpenFile.cs
//

using MSG.Patterns;

namespace Priorities.Modules.File.Cmds
{
    public class OpenFile : Cmd
    {
        protected string fileName;

        public OpenFile(string fileName)
        {
            this.fileName = fileName;
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
