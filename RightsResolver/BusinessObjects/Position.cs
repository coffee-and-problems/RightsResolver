using JetBrains.Annotations;

namespace RightsResolver
{
    public class Position
    {
        [NotNull] public int[] Departments { get; }
        [NotNull] public string Post { get; }

        public Position(int[] departments, string post)
        {
            Departments = departments;
            Post = post;
        }
    }
}
