using System.IO;
using NUnit.Framework;
using RightsResolver;

namespace Tests
{
    [TestFixture]
    public class ResolverShould
    {
        private RulesReader reader;
        private string filePath;
        [SetUp]
        public void SetUp()
        {
            var currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            filePath = Path.Combine(currentDirectory, "Rules", "TestRules.xml");
            reader = new RulesReader(filePath);
        }

        [Test]
        public void ReadRulesCorrectly()
        {
            var rules = reader.ReadRules();
            Assert.IsNotEmpty(rules);
            var rule = rules[0];
            Assert.AreEqual(0, rule.Department);
            Assert.AreEqual("post0", rule.Post);
            Assert.AreEqual(2,rule.PlatformAccesses.Count);
            Assert.AreEqual(1,rule.ProductAccesses.Count);
            Assert.AreEqual(2, rule.ProductAccesses[Platform.Support].Count);
        }
    }
}