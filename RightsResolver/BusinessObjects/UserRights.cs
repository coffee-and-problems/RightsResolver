using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightsResolver
{
    public class UserRights
    {
        public Guid UserId { get; }
        public Rights Rights { get; }

        public UserRights(Guid userId, Rights rights)
        {
            UserId = userId;
            Rights = rights;
        }
    }
}
