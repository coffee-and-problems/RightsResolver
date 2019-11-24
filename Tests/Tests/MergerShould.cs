using NUnit.Framework;
using RightsResolver.Implementation;
using RightsResolver.Models;
using Tests.Generators;

namespace Tests.Tests
{
    [TestFixture]
    public class MergerShould
    {
        private Merger merger;

        [SetUp]
        public void SetUp()
        {
            merger = new Merger();
        }

        [Test]
        public void NoMerge_WhenNotNeeded()
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
            Assert.AreEqual(Role.Admin, mergedRules.PlatformAccesses[Platform.Health]);
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
                Assert.AreEqual(Role.Admin, role);
            }
        }

    }
}