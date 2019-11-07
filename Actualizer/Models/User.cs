using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actualizer
{
    class User
    {
        public Guid UserId { get; }
        public List<Position> UserPositions { get; }

        public User(Guid userId, List<Position> userPositions)
        {
            UserId = userId;
            UserPositions = userPositions;
        }
    }
}
