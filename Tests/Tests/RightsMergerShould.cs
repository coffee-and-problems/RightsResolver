using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using FluentAssertions;
using NUnit.Framework;
using RightsResolver.Implementation;
using RightsResolver.Models;
using RightsResolver.BusinessObjects;
using Tests.Generators;

namespace Tests.Tests
{
    [TestFixture]
    public class RightsMergerShould
    {
        private RightsMerger merger;

        [SetUp]
        public void SetUp()
        {
            merger = new RightsMerger();
        }

        [TestCaseSource(nameof(GenerateRightsMergerCases))]
        public void TestRightsMerger(List<Rights> allRights, Rights expectedRights)
        {
            var mergedRights = merger.MergeRights(allRights);

            mergedRights.Should().BeEquivalentTo(expectedRights);
        }

        private static IEnumerable<TestCaseData> GenerateRightsMergerCases()
        {
            var noMergeRights = RightsGenerator.GenerateRights(false);
            var mergeRights = RightsGenerator.GenerateRights(true);
            var multipleRights = RightsGenerator.GenerateMultipleRightsForMerge();

            yield return new TestCaseData(new List<Rights>(),
                    new Rights(new Dictionary<Platform, Role>(),
                        new Dictionary<Platform, Dictionary<string, Role>>()))
                .SetName("Пустой лист");
            yield return new TestCaseData(noMergeRights, noMergeRights[0])
                .SetName("Объединение прав не нужно");
            yield return new TestCaseData(mergeRights, GetExpectedMergedRights(false))
                .SetName("Объединение внутри одного объекта прав");
            yield return new TestCaseData(multipleRights, GetExpectedMergedRights(true))
                .SetName("Объединение нескольких прав");
        }

        private static Rights GetExpectedMergedRights(bool multiple)
        {
            Dictionary<string, Role> productAccesses;

            if (!multiple)
            {
                productAccesses = AllProductsArray.Products.ToDictionary(product => product, r => Role.RoleII);
                return new Rights(
                    new Dictionary<Platform, Role> {{Platform.Health, Role.RoleII}},
                    new Dictionary<Platform, Dictionary<string, Role>> {{Platform.Support, productAccesses}});
            }

            productAccesses = AllProductsArray.Products.ToDictionary(product => product, r => Role.RoleII);
            productAccesses["Product3"] = Role.Admin;
            productAccesses["Product5"] = Role.Admin;
            var platformAccesses = new Dictionary<Platform, Role>
            {
                {Platform.Health, Role.Admin},
                {Platform.Oorv, Role.RoleI}
            };

            return new Rights(
                platformAccesses,
                new Dictionary<Platform, Dictionary<string, Role>> {{Platform.Support, productAccesses}});
        }
    }
}