using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightsResolver
{
    class User
    {
        public Guid UserId { get; }
        public List<Position> UserPositions { get; }
    }

    class Position
    {
        public int[] Departments { get; }
        public string Post { get; }
    }
}
