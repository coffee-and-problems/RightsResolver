using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightsResolver
{
    class UserDto
    {
        public Guid UserId { get; }
        public List<Position> UserPositions { get; }
    }

}
