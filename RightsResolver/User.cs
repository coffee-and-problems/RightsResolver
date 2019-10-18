using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightsResolver
{
    class User
    {
        public int UserId { get; }
        public IEnumerable<Position> UserPositions { get; }
    }

    class Position
    {
        public int[] Departments { get; }
        public string Post { get; }
    }
}
