using System.IO;
using NUnit.Framework;
using RightsResolver;

namespace Tests
{
    [TestFixture]
    public class RuleApplierShould
    {
        private RulesApplier applier;
        private string rulesDirectory;

        [SetUp]
        public void SetUp()
        {
            applier = new RulesApplier(AllProductsArray.Products);

            var currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            rulesDirectory = Path.Combine(currentDirectory, "Rules");
        }

        [Test]
        public void ApplyRulesCorrectly_NoUnpack()
        {
            var rulePath = Path.Combine(rulesDirectory, "WithoutUnpackProducts.xml");
            var rules = new RulesReader(rulePath).ReadRules();
            var rights = applier.ApplyRules(rules);

            Assert.AreEqual(1, rights.Count);
            Assert.AreEqual(1, rights[0].PlatformAccesses.Count);
            Assert.AreEqual(1, rights[0].ProductAccesses.Count);

            var productList = rights[0].ProductAccesses[Platform.Support];
            Assert.AreEqual(2,productList.Count);
        }

        [Test]
        public void ApplyRulesCorrectly_WithUnpack()
        {
            var rulePath = Path.Combine(rulesDirectory, "WithUnpackProducts.xml");
            var rules = new RulesReader(rulePath).ReadRules();
            var rights = applier.ApplyRules(rules);

            Assert.AreEqual(1, rights.Count);
            Assert.AreEqual(1, rights[0].PlatformAccesses.Count);
            Assert.AreEqual(1, rights[0].ProductAccesses.Count);

            var productList = rights[0].ProductAccesses[Platform.Support];
            Assert.AreEqual(5,productList.Count);
        }

        [Test] public void ConcatRights_OnMultipleRules()
        {
            var rulePath1 = Path.Combine(rulesDirectory, "WithoutUnpackProducts.xml");
            var rulePath2 = Path.Combine(rulesDirectory, "WithUnpackProducts.xml");
            var rules = new RulesReader(rulePath1).ReadRules();
            rules.AddRange(new RulesReader(rulePath2).ReadRules());
            var rights = applier.ApplyRules(rules);
            
            Assert.AreEqual(2, rights.Count);
            Assert.AreEqual(1, rights[0].PlatformAccesses.Count);
            Assert.AreEqual(1, rights[0].ProductAccesses.Count);
            Assert.AreEqual(1, rights[1].PlatformAccesses.Count);
            Assert.AreEqual(1, rights[1].ProductAccesses.Count);

            var productList = rights[0].ProductAccesses[Platform.Support];
            Assert.AreEqual(2,productList.Count);

            productList = rights[1].ProductAccesses[Platform.Support];
            Assert.AreEqual(5,productList.Count);
        }
    }
}