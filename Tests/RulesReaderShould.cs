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
        public void ReaderReadFile()
        {
            Assert.DoesNotThrow(() => reader.LoadXmlFromFile(filePath));
        }

        [Test]
        public void ReaderReadRules()
        {
            var rules = reader.ReadRules();
            Assert.IsNotEmpty(rules);
            Assert.AreEqual(0, rules[0].Department);
            Assert.AreEqual("post0", rules[0].Post);
            Assert.AreEqual(1,rules[0].PlatformAccesses.Count);
            Assert.AreEqual(1,rules[0].ProductAccesses.Count);
        }
    }
}