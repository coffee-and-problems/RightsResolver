using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using RightsResolver;

namespace Tests
{
    [TestFixture]
    public class ValidatorShould
    {
        private RulesValidator validator;
        private List<Rule> validRules;
        private List<Rule> invalidRules;

        [SetUp]
        public void SetUp()
        {
            validator = new RulesValidator();

            var currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            var validRulesPath = Path.Combine(currentDirectory, "Rules", "TestRules.xml");
            var invalidRulesPath = Path.Combine(currentDirectory, "Rules", "InvalidTestRules.xml");

            validRules = new RulesReader(validRulesPath).ReadRules();
            invalidRules = new RulesReader(invalidRulesPath).ReadRules();
        }

        [Test]
        public void Validator_ReturnTrue_OnValidRules()
        {
            Assert.IsTrue(validator.IsValid(validRules));
        }

        [Test]
        public void Validator_ReturnFalse_OnInvalidRules()
        {
            Assert.IsFalse(validator.IsValid(invalidRules));
        }
    }
}