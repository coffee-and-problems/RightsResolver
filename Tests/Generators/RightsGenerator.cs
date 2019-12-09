﻿using System.Collections.Generic;
using System.Linq;
using RightsResolver.BusinessObjects;
using RightsResolver.Models;

namespace Tests.Generators
{
    public static class RightsGenerator
    {
        public static List<RuleRights> GenerateRights(bool merge)
        {
            var rights = new List<RuleRights>();
            var productAccessesRoleI = AllProductsArray.Products.ToDictionary(product => product, r => Role.RoleI);

            rights.Add(new RuleRights(
                new Dictionary<Platform, Role> {{Platform.Health, Role.RoleI}},
                new Dictionary<Platform, Dictionary<string, Role>> {{Platform.Support, productAccessesRoleI}}));

            if (merge)
            {
                var productAccessesHigherRole = AllProductsArray.Products.ToDictionary(
                    product => product, r => Role.RoleII);
                rights.Add(new RuleRights(
                    new Dictionary<Platform, Role> {{Platform.Health, Role.RoleII}},
                    new Dictionary<Platform, Dictionary<string, Role>> {{Platform.Support, productAccessesHigherRole}}));
            }

            return rights;
        }
    }
}