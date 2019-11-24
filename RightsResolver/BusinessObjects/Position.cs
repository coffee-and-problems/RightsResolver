using JetBrains.Annotations;

namespace RightsResolver.BusinessObjects
{
    public class Position
    {
        [NotNull] public int[] Departments { get; }
        [NotNull] public string Post { get; }

        public Position([NotNull] int[] departments, [NotNull] string post)
        {
            Departments = departments;
            Post = post;
        }
    }
}
