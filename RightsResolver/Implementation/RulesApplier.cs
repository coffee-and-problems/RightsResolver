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

        private Dictionary<Platform, List<ProductRole>> GetProductAccesses
            (Dictionary<Platform, List<ProductRole>> ruleAccesses)
        {
            var productAccesses = new Dictionary<Platform, List<ProductRole>>();

            foreach (var platform in ruleAccesses.Keys)
            {
                 productAccesses.Add(platform, GetUserProductRoles(ruleAccesses[platform]));
            }

            return productAccesses;
        }

        private List<ProductRole> GiveAccessToAllProductsWithRole(Role role)
        {
            return allProducts.Select(product => new ProductRole(product, role)).ToList();
        }

        private List<ProductRole> GetUserProductRoles(List<ProductRole> products)
        {
            var forAllProducts = products.Where(productRole =>
                    string.Equals(productRole.ProductId, "All", StringComparison.OrdinalIgnoreCase))
                .ToArray()
                .FirstOrDefault();

            var userProductRoles = new List<ProductRole>();
            if (forAllProducts != null)
            {
                userProductRoles = GiveAccessToAllProductsWithRole(forAllProducts.Role);
            }

            userProductRoles.AddRange(products.Where(productRole =>
                !string.Equals(productRole.ProductId, "All", StringComparison.OrdinalIgnoreCase))
                .ToList());

            return userProductRoles;
        }
    }
}