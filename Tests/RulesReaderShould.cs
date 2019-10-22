using System.Collections;
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
            reader = new RulesReader();
            var currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            filePath = Path.Combine(currentDirectory, "..", "..", "Rules", "Rules.xml");
        }

        [Test]
        public void ReaderReadFile()
        {
            Assert.DoesNotThrow(() => reader.LoadXmlFromFile(filePath));
        }

        [Test]
        public void ReaderReadRules()
        {
            Assert.IsNotEmpty((IEnumerable) reader.GetRulesDeclaration(filePath));
        }
    }
}