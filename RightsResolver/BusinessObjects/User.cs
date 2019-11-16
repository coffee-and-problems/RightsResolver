using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace RightsResolver
{
    public class User
    {
        public Guid UserId { get; }
        [NotNull] public List<Position> UserPositions { get; }

        public User(Guid userId, List<Position> userPositions)
        {
            UserId = userId;
            UserPositions = userPositions;
        }
    }
}
