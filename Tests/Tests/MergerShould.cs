using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RightsResolver.Implementation;
using RightsResolver.Models;
using RightsResolver.BusinessObjects;
using Tests.Generators;

namespace Tests.Tests
{
    [TestFixture]
    public class MergerShould
    {
        private RightsMerger merger;

        [SetUp]
        public void SetUp()
        {
            merger = new RightsMerger();
        }

        [Test]
        public void ReturnEmpty_OnEmptyList()
        {
            var allRights = new List<RuleRights>();
            Assert.DoesNotThrow(() => merger.MergeRights(allRights));
            var mergedRules = merger.MergeRights(allRights);
            Assert.IsEmpty(mergedRules.ProductAccesses);
            Assert.IsEmpty(mergedRules.PlatformAccesses);
        }

        [Test]
        public void NotMerge_WhenNotNeeded()
        {
            var allRights = RightsGenerator.GenerateRights(false);
            var mergedRules = merger.MergeRights(allRights);

            Assert.AreEqual(1, mergedRules.PlatformAccesses.Count);
            Assert.AreEqual(Role.RoleI, mergedRules.PlatformAccesses[Platform.Health]);
            Assert.AreEqual(1, mergedRules.ProductAccesses.Count);
            var productAccesses = mergedRules.ProductAccesses[Platform.Support];
            Assert.AreEqual(5, productAccesses.Count);
            foreach (var role in productAccesses.Values)
            {
                Assert.AreEqual(Role.RoleI, role);
            }
        }

        [Test]
        public void Merge_PlatformAccesses()
        {
            var allRights = RightsGenerator.GenerateRights(true);
            var mergedRules = merger.MergeRights(allRights);

            Assert.AreEqual(1, mergedRules.PlatformAccesses.Count);
            Assert.AreEqual(Role.RoleII, mergedRules.PlatformAccesses[Platform.Health]);
        }

        [Test]
        public void Merge_ProductAccesses()
        {
            var allRights = RightsGenerator.GenerateRights(true);
            var mergedRules = merger.MergeRights(allRights);

            Assert.AreEqual(1, mergedRules.ProductAccesses.Count);
            var productAccesses = mergedRules.ProductAccesses[Platform.Support];
            Assert.AreEqual(5, productAccesses.Count);
            foreach (var role in productAccesses.Values)
            {
                Assert.AreEqual(Role.RoleII, role);
            }
        }

        [Test]
        public void Merge_ManyRights()
        {
            var allRights = RightsGenerator.GenerateRights(true);
            var productAccessesWithAdmin = AllProductsArray.Products.ToDictionary(product => product, r => Role.RoleII);
            productAccessesWithAdmin["Product3"] = Role.Admin;
            productAccessesWithAdmin["Product5"] = Role.Admin;
            var platformAccessesWithAdmin = new Dictionary<Platform, Role>
            {
                {Platform.Health, Role.Admin},
                {Platform.Oorv, Role.RoleI}
            };

            allRights.Add(new RuleRights(
                platformAccessesWithAdmin,
                new Dictionary<Platform, Dictionary<string, Role>> {{Platform.Support, productAccessesWithAdmin}}));
            var mergedRules = merger.MergeRights(allRights);

            Assert.AreEqual(2, mergedRules.PlatformAccesses.Count);
            Assert.AreEqual(platformAccessesWithAdmin, mergedRules.PlatformAccesses);
            var productAccesses = mergedRules.ProductAccesses[Platform.Support];
            Assert.AreEqual(5, productAccesses.Count);
            foreach (var product in AllProductsArray.Products)
            {
                if (product == "Product3" || product == "Product5")
                    Assert.AreEqual(Role.Admin, productAccesses[product]);
                else 
                    Assert.AreEqual(Role.RoleII, productAccesses[product]);
            }
        }
    }
}