﻿using System.Collections.Generic;
using System.Linq;
using RightsResolver.BusinessObjects;
using RightsResolver.Models;

namespace Tests.Generators
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
                var productAccessesHigherRole = AllProductsArray.Products.ToDictionary(
                    product => product, r => Role.RoleII);
                rights.Add(new Rights(
                    new Dictionary<Platform, Role> {{Platform.Health, Role.RoleII}},
                    new Dictionary<Platform, Dictionary<string, Role>> {{Platform.Support, productAccessesHigherRole}}));
            }

            return rights;
        }

        public static List<Rights> GenerateMultipleRightsForMerge()
        {
            var rights = GenerateRights(true);
            var productAccessesWithAdmin = AllProductsArray.Products.ToDictionary(product => product, r => Role.RoleI);
            productAccessesWithAdmin["Product3"] = Role.Admin;
            productAccessesWithAdmin["Product5"] = Role.Admin;
            var platformAccessesWithAdmin = new Dictionary<Platform, Role>
            {
                {Platform.Health, Role.Admin},
                {Platform.Oorv, Role.RoleI}
            };

            rights.Add(new Rights(
                platformAccessesWithAdmin,
                new Dictionary<Platform, Dictionary<string, Role>> {{Platform.Support, productAccessesWithAdmin}}));
            return rights;
        }
    }
}