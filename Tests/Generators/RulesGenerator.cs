using System.Collections.Generic;
using RightsResolver.Models;

namespace Tests.Generators
{
    public static class RulesGenerator
    {
        public static List<Rule> GenerateValidRules(bool allProductsFlag)
        {
            var rules = new List<Rule>();

            var productAccesses = new Dictionary<Platform, Dictionary<string, Role>>();
            var platformAccesses = new Dictionary<Platform, Role>();

            platformAccesses.Add(Platform.Oorv, Role.RoleI);

            if (allProductsFlag)
                productAccesses.Add(Platform.Support, new Dictionary<string, Role>
                    {{"All", Role.RoleI}});
            else productAccesses.Add(Platform.Support, new Dictionary<string, Role>
                {{"Product1", Role.Admin}});

            rules.Add(new Rule(
                0,
                "Any",
                productAccesses,
                platformAccesses));

            return rules;
        }

        public static List<Rule> GenerateInvalidRules(bool wrongPlatformAccess)
        {
            var rules = new List<Rule>();

            var productAccesses = new Dictionary<Platform, Dictionary<string, Role>>();
            var platformAccesses = new Dictionary<Platform, Role>();

            if (wrongPlatformAccess) platformAccesses.Add(Platform.Support, Role.RoleI);
            else productAccesses.Add(Platform.Oorv, new Dictionary<string, Role>
                    {{"Product1", Role.RoleI}});

            rules.Add(new Rule(
                0,
                "Any",
                productAccesses,
                platformAccesses));

            return rules;
        }
    }
}