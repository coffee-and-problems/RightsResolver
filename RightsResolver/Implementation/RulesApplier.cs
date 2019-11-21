using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace RightsResolver
{
    public class RulesApplier
    {
        private readonly string[] allProducts;

        public RulesApplier(string[] allProducts)
        {
            this.allProducts = allProducts;
        }

        [NotNull]
        public List<Rights> ApplyRules([NotNull] List<Rule> rules)
        {
            return rules.Select(ApplyRule).ToList();
        }

        private Rights ApplyRule([NotNull] Rule rule)
        {
            var platformAccesses = rule.PlatformAccesses;
            var productAccesses = GetProductAccesses(rule.ProductAccesses);

            return new Rights(platformAccesses, productAccesses);
        }

        private Dictionary<Platform, Dictionary<string, Role>> GetProductAccesses
            ([NotNull] Dictionary<Platform, Dictionary<string, Role>> ruleAccesses)
        {
            var productAccesses = new Dictionary<Platform, Dictionary<string, Role>>();

            foreach (var platform in ruleAccesses.Keys)
            {
                 productAccesses.Add(platform, ApplyProductRule(ruleAccesses[platform]));
            }

            return productAccesses;
        }

        private Dictionary<string, Role> ApplyProductRule(Dictionary<string, Role> productRoles)
        {
            Role? forAllProducts = null;
            if (productRoles.ContainsKey("All")) forAllProducts = productRoles["All"];

            var userProductRoles = new Dictionary<string, Role>();
            if (forAllProducts != null)
            {
                userProductRoles = GiveAccessToAllProducts(forAllProducts.Value);
            }

            foreach (var productRole in 
                productRoles.Where(productRole => allProducts.Contains(productRole.Key)))
            {
                userProductRoles.AddOrUpdate(productRole.Key, productRole.Value);
            }

            return userProductRoles;
        }
        
        private Dictionary<string, Role> GiveAccessToAllProducts(Role role)
        {
            return allProducts.ToDictionary(product => product, r => role);
        }
    }
}