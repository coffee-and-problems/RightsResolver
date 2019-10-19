using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightsResolver
{
    class Rule
    {
        private User user;
        public List<IAccess> Access { get; }
        public Guid GetUserId() => user.UserId;
    }
}
