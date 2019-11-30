using System.Collections.Generic;
using RightsResolver.Models;
using RightsResolver.BusinessObjects;

namespace Tests.Generators
{
    public static class RulesGenerator
    {
        public static List<Rule> GenerateValidRules(bool allProductsFlag)
        {
            var rules = new List<Rule>();

            var productAccesses = new Dictionary<Platform, Dictionary<string, Role>>();
            var platformAccesses = new Dictionary<Platform, Role> { { Platform.Oorv, Role.RoleI } };

            if (allProductsFlag)
                productAccesses.Add(Platform.Support, new Dictionary<string, Role>
                    {{"All", Role.RoleI}});
            else productAccesses.Add(Platform.Support, new Dictionary<string, Role>
                    {{"Product1", Role.RoleII}});

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

        public static List<Rule> GenerateMultipleRules()
        {
            var rules = new List<Rule>();

            rules.Add(
                new Rule(0,
                    "Any",
                    new Dictionary<Platform, Dictionary<string, Role>>(),
                    new Dictionary<Platform, Role> { { Platform.Oorv, Role.RoleI } }));
            rules.Add(
                new Rule(0,
                "post1",
                    new Dictionary<Platform, Dictionary<string, Role>>
                        {{
                            Platform.Support, new Dictionary<string, Role> {{ "All", Role.RoleII }}
                        }},
                    new Dictionary<Platform, Role>()));
            rules.Add(
                new Rule(1,
                "any",
                    new Dictionary<Platform, Dictionary<string, Role>>
                    {{
                        Platform.Support, new Dictionary<string, Role> {{ "All", Role.RoleII }, 
                                                                        { "Product1", Role.Admin }}
                    }},
                    new Dictionary<Platform, Role> {{ Platform.Oorv, Role.Admin },
                                                    { Platform.Health, Role.RoleI }}));
            rules.Add(
                new Rule(2,
                    "post2",
                    new Dictionary<Platform, Dictionary<string, Role>>(),
                    new Dictionary<Platform, Role> {{ Platform.Health, Role.Admin }}));
            return rules;
        }
    }
}