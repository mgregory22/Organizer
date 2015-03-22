using MSG.Patterns;

namespace Priorities.Commands
{
    class Quit : Command
    {
        bool done = false;
        public bool Done
        {
            get { return done; }
            set { done = value; }
        }
        public override void Do()
        {
            done = true;
        }
    }
}
