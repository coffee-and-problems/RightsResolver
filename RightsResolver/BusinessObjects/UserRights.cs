using System;
using JetBrains.Annotations;

namespace RightsResolver.BusinessObjects
{
    public class UserRights
    {
        public Guid UserId { get; }
        [NotNull] public Rights Rights { get; }

        public UserRights(Guid userId, [NotNull] Rights rights)
        {
            UserId = userId;
            Rights = rights;
        }
    }
}
