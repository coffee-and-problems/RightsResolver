using System.Collections.Generic;
using RightsResolver;
using System.Linq;

namespace Tests
{
    public static class RightsGenerator
    {
        public static List<Rights> GenerateRights(bool merge)
        {
            var rights = new List<Rights>();
            var productAccessesRoleI = AllProductsArray.Products.ToDictionary(product => product, r => Role.RoleI);

            rights.Add(new Rights(
                new Dictionary<Platform, Role> {{Platform.Health, Role.RoleI}},
                new Dictionary<Platform, Dictionary<string, Role>> {{Platform.Support, productAccessesRoleI}}));

            if (merge)
            {
                var productAccessesAdmin = AllProductsArray.Products.ToDictionary(product => product, r => Role.Admin);
                rights.Add(new Rights(
                    new Dictionary<Platform, Role> {{Platform.Health, Role.Admin}},
                    new Dictionary<Platform, Dictionary<string, Role>> {{Platform.Support, productAccessesAdmin}}));
            }

            return rights;
        }
    }
}