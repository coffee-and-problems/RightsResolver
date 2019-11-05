using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightsResolver
{
    public class User
    {
        public Guid UserId { get; }
        public List<Position> UserPositions { get; }
    }

}
