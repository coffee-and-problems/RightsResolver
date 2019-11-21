using System.IO;
using NUnit.Framework;
using RightsResolver;

namespace Tests
{
    [TestFixture]
    public class RuleApplierShould
    {
        private RulesApplier applier;

        [SetUp]
        public void SetUp()
        {
            applier = new RulesApplier(AllProductsArray.Products);
        }

        [Test]
        public void ApplyRulesCorrectly_NoUnpack()
        {
            var rules = RulesGenerator.GenerateValidRules(false);
            var rights = applier.ApplyRules(rules);

            Assert.AreEqual(1, rights.Count);
            Assert.AreEqual(1, rights[0].PlatformAccesses.Count);
            Assert.AreEqual(1, rights[0].ProductAccesses.Count);

            var productList = rights[0].ProductAccesses[Platform.Support];
            Assert.AreEqual(1,productList.Count);
        }

        [Test]
        public void ApplyRulesCorrectly_WithUnpack()
        {
            var rules = RulesGenerator.GenerateValidRules(true);
            var rights = applier.ApplyRules(rules);

            Assert.AreEqual(1, rights.Count);
            Assert.AreEqual(1, rights[0].PlatformAccesses.Count);
            Assert.AreEqual(1, rights[0].ProductAccesses.Count);

            var productList = rights[0].ProductAccesses[Platform.Support];
            Assert.AreEqual(5,productList.Count);
        }

        [Test] public void AllFlagHasLowestPriority()
        {
            var rules = RulesGenerator.GenerateValidRules(true);
            rules[0].ProductAccesses[Platform.Support]["Product1"] = Role.Admin;
            var rights = applier.ApplyRules(rules);
            
            Assert.AreEqual(1, rights.Count);
            Assert.AreEqual(1, rights[0].PlatformAccesses.Count);
            Assert.AreEqual(1, rights[0].ProductAccesses.Count);

            var productDict = rights[0].ProductAccesses[Platform.Support];
            Assert.AreEqual(5,productDict.Count);
            Assert.AreEqual(Role.Admin,productDict["Product1"]);
        }
    }
}