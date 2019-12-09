using System;
using JetBrains.Annotations;

namespace RightsResolver.BusinessObjects
{
    public class UserRights
    {
        public Guid UserId { get; }
        [NotNull] public RuleRights Rights { get; }

        public UserRights(Guid userId, [NotNull] RuleRights rights)
        {
            UserId = userId;
            Rights = rights;
        }
    }
}
