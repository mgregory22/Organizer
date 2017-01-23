//
// Priorities/Types/Task.cs
//

using System;

namespace Priorities.Types
{
    public class Task : IEquatable<Task>
    {
        string name;

        public Task(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool Equals(Task other)
        {
            return name.Equals(other.name);
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }
    }

}
