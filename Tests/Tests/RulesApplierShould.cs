using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using RightsResolver.Implementation;
using RightsResolver.Models;
using RightsResolver.BusinessObjects;
using Tests.Generators;

namespace Tests.Tests
{
    [TestFixture]
    public class RulesApplierShould
    {
        private RulesApplier applier;

        [SetUp]
        public void SetUp()
        {
            applier = new RulesApplier(AllProductsArray.Products);
        }

        [TestCaseSource(nameof(GenerateRulesApplierCases))]
        public void TestRulesApplier(List<Rule> rules, List<Rights> expectedRights)
        {
            var rights = applier.ApplyRules(rules);

            rights.Should().BeEquivalentTo(expectedRights);
        }

        private static IEnumerable<TestCaseData> GenerateRulesApplierCases()
        {
            var noUnpackRules = RulesGenerator.GenerateValidRules(false);
            var noUnpackRights = noUnpackRules
                .Select(rule => new Rights(rule.PlatformAccesses, rule.ProductAccesses))
                .ToList();
            var withUnpackRules = RulesGenerator.GenerateValidRules(true);
            var withExtraProduct = RulesGenerator.GenerateValidRules(false);
            withExtraProduct[0].ProductAccesses[Platform.Support].Add("Unknown", Role.RoleII);
            var specifiedRoleAndAllFlag = RulesGenerator.GenerateValidRules(true);
            specifiedRoleAndAllFlag[0].ProductAccesses[Platform.Support]["Product1"] = Role.Admin;
            var rightsForSpecifiedRole = GetRightWithAllFlag();
            rightsForSpecifiedRole[0].ProductAccesses[Platform.Support]["Product1"] = Role.Admin;

            yield return new TestCaseData(new List<Rule>(), new List<Rights>())
                .SetName("Нет правил для применения. Возвращает пустой лист");
            yield return new TestCaseData(noUnpackRules, noUnpackRights)
                .SetName("Нет флага All. Успех");
            yield return new TestCaseData(withExtraProduct, noUnpackRights)
                .SetName("Продукт, которого нет в списке. Игнорирует этот продукт");
            yield return new TestCaseData(withUnpackRules, GetRightWithAllFlag())
                .SetName("Есть флаг All. Успех");
            yield return new TestCaseData(specifiedRoleAndAllFlag, rightsForSpecifiedRole)
                .SetName("Роль, указанная в All, имеет наименьший приоретет");
        }

        private static List<Rights> GetRightWithAllFlag()
        {
            var productAccesses =
                AllProductsArray.Products.ToDictionary(product => product, r => Role.RoleI);
            var platformAccesses = new Dictionary<Platform, Role> {{ Platform.Oorv, Role.RoleI }};
            return new List<Rights> {new Rights(platformAccesses,
                new Dictionary<Platform, Dictionary<string, Role>> {{ Platform.Support, productAccesses }})};
        }
    }
}