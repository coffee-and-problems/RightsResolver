using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightsResolver
{
    class UserRights
    {
        public int UserId { get; }
        public List<IAccess> Access { get; }
    }
}
