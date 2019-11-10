using System;
using System.Collections.Generic;
using System.Linq;

namespace RightsResolver
{
    public class RulesApplier
    {
        private readonly string[] allProducts;

        public RulesApplier(string[] allProducts)
        {
            this.allProducts = allProducts;
        }

        public List<Rights> ApplyRules(List<Rule> rules)
        {
            return rules.Select(ApplyRule).ToList();
        }

        private Rights ApplyRule(Rule rule)
        {
            var platformAccesses = rule.PlatformAccesses;
            var productAccesses = GetProductAccesses(rule.ProductAccesses);

            return new Rights(platformAccesses, productAccesses);
        }

        private Dictionary<Platform, Dictionary<string, Role>> GetProductAccesses
            (Dictionary<Platform, List<ProductRole>> ruleAccesses)
        {
            var productAccesses = new Dictionary<Platform, Dictionary<string, Role>>();

            foreach (var platform in ruleAccesses.Keys)
            {
                 productAccesses.Add(platform, GetUserProductRoles(ruleAccesses[platform]));
            }

            return productAccesses;
        }

        private Dictionary<string, Role> GiveAccessToAllProductsWithRole(Role role)
        {
            return allProducts.ToDictionary(product => product, r => role);
        }

        private Dictionary<string, Role> GetUserProductRoles(List<ProductRole> productRoles)
        {
            var forAllProducts = productRoles.Where(productRole =>
                    string.Equals(productRole.ProductId, "All", StringComparison.OrdinalIgnoreCase))
                .ToArray()
                .FirstOrDefault();

            var userProductRoles = new Dictionary<string, Role>();
            if (forAllProducts != null)
            {
                userProductRoles = GiveAccessToAllProductsWithRole(forAllProducts.Role);
            }

            foreach (var productRole in 
                productRoles.Where(productRole => 
                    !string.Equals(productRole.ProductId, "All", StringComparison.OrdinalIgnoreCase)))
            {
                userProductRoles.AddOrUpdate(productRole.ProductId, productRole.Role);
            }

            return userProductRoles;
        }
    }
}