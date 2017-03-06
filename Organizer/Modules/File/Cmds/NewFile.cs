//
// Organizer/Modules/File/Cmds/NewFile.cs
//

using MSG.Patterns;
using Organizer.Modules.Tasks;

namespace Organizer.Modules.File.Cmds
{
    public class NewFile : Cmd
    {
        protected string fileName;

        public NewFile(string fileName)
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
