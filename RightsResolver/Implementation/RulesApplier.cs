using System;
using System.Collections.Generic;
using System.Linq;

namespace RightsResolver
{
    public class RulesApplier
    {
        private string[] allProducts;

        public RulesApplier(string[] allProducts)
        {
            this.allProducts = allProducts;
        }

        public (Dictionary<Platform, Role> platformAccesses,
            Dictionary<Platform, List<ProductRole>> productAccess)
            ApplyRules(List<Rule> rules)
        {

        }
        private List<ProductRole> GetRolesForAllProducts(List<ProductRole> products)
        {
            var allProductRoles = new Dictionary<string, Role>();
            var productRoleForAll = products.Where(productRole =>
                    string.Equals(productRole.ProductId, "All", StringComparison.OrdinalIgnoreCase))
                .ToArray();

            var unpackProducts = productRoleForAll.Length > 0;
            if (unpackProducts)
            {
                foreach (var product in allProducts)
                {
                    allProductRoles.Add(product, productRoleForAll[0].Role);
                }
            }

            foreach (var productRole in products)
            {
                if (allProductRoles.ContainsKey(productRole.ProductId))
                    allProductRoles[productRole.ProductId] = EnumExtention.Max(
                        allProductRoles[productRole.ProductId], productRole.Role);
            }

            return allProductRoles.Select(
                    productRole => new ProductRole(productRole.Key, productRole.Value))
                .ToList();
        }
    }
}