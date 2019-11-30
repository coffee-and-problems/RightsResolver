using System.IO;
using NUnit.Framework;
using RightsResolver.BusinessObjects;
using RightsResolver.Implementation;
using RightsResolver.Implementation.Exceptions;

namespace Tests.Tests
{
    [TestFixture]
    public class ReaderShould
    {
        private string filePath;
        [SetUp]
        public void SetUp()
        {
            var currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            filePath = Path.Combine(currentDirectory, "Rules");
        }

        [Test]
        public void ReadRules_WhenAllIsFine()
        {
            var reader = new RulesReader(Path.Combine(filePath, "Valid", "TestRules.xml"));
            var rules = reader.ReadRules();

            Assert.AreEqual(1, rules.Count);
            var rule = rules[0];
            Assert.AreEqual(0, rule.Department);
            Assert.AreEqual("post0", rule.Post);
            Assert.AreEqual(2,rule.PlatformAccesses.Count);
            Assert.AreEqual(1,rule.ProductAccesses.Count);
            Assert.AreEqual(2, rule.ProductAccesses[Platform.Support].Count);
        }

        [Test]
        public void ReadRules_WhenMultipleRules()
        {
            var reader = new RulesReader(Path.Combine(filePath, "Valid", "MultipleRules.xml"));
            var rules = reader.ReadRules();
           
            Assert.AreEqual(4, rules.Count);
            var rule = rules[0];
            Assert.AreEqual(0, rule.Department);
            Assert.AreEqual("Any", rule.Post);
            Assert.AreEqual(1,rule.PlatformAccesses.Count);
            Assert.AreEqual(0,rule.ProductAccesses.Count);
            rule = rules[1];
            Assert.AreEqual(0, rule.Department);
            Assert.AreEqual("post1", rule.Post);
            Assert.AreEqual(0,rule.PlatformAccesses.Count);
            Assert.AreEqual(1,rule.ProductAccesses.Count);
            Assert.AreEqual(1, rule.ProductAccesses[Platform.Support].Count);
            rule = rules[2];
            Assert.AreEqual(1, rule.Department);
            Assert.AreEqual("any", rule.Post);
            Assert.AreEqual(2,rule.PlatformAccesses.Count);
            Assert.AreEqual(1,rule.ProductAccesses.Count);
            Assert.AreEqual(2, rule.ProductAccesses[Platform.Support].Count);
            rule = rules[3];
            Assert.AreEqual(2, rule.Department);
            Assert.AreEqual("post2", rule.Post);
            Assert.AreEqual(1,rule.PlatformAccesses.Count);
            Assert.AreEqual(0,rule.ProductAccesses.Count);
        }

        [Test]
        public void ReadRules_InDirectory()
        {
            var reader = new RulesReader(Path.Combine(filePath, "Valid"));
            var rules = reader.ReadRules();

            Assert.AreEqual(5, rules.Count);
        }

        [Test]
        public void Throws_WhenNoFile()
        {
            var rulesPath = Path.Combine(filePath, "Invalid", "NotExists.xml");
            var exception = Assert.Throws<InvalidRulesException>(() => new RulesReader(rulesPath));
            Assert.IsNotEmpty(exception.Message);
        }

        [Test]
        public void Throws_WhenEmptyFile()
        {
            var rulesPath = Path.Combine(filePath, "Invalid", "EmptyFile.xml");
            var reader = new RulesReader(rulesPath);
            var exception = Assert.Throws<InvalidRulesException>(() => reader.ReadRules());
            Assert.AreEqual($"Пустые правила: {rulesPath}", exception.Message);
        }

        [Test]
        public void Throws_WhenInvalidRules()
        {
            var rulesPath = Path.Combine(filePath, "Invalid", "InvalidRules.xml");
            var reader = new RulesReader(rulesPath);
            var exception = Assert.Throws<InvalidRulesException>(() => reader.ReadRules());
            Assert.AreEqual($"{rulesPath}", exception.Message);
        }

        [Test]
        public void Throws_WhenNodeMissing()
        {
            var rulesPath = Path.Combine(filePath, "Invalid", "NodeMissing.xml");
            var reader = new RulesReader(rulesPath);
            var exception = Assert.Throws<InvalidRulesException>(() => reader.ReadRules());
            Assert.AreEqual("Нод User не содержит элемента Post", exception.Message);
        }
    }
}