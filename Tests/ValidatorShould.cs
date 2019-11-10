using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using RightsResolver;

namespace Tests
{
    [TestFixture]
    public class ValidatorShould
    {
        private Validator validator;
        string validRulesPath;
        string invalidRulesPath;

        [SetUp]
        public void SetUp()
        {
            validator = new Validator();
            var currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            validRulesPath = Path.Combine(currentDirectory, "Rules", "TestRules.xml");
            invalidRulesPath = Path.Combine(currentDirectory, "Rules", "InvalidTestRules.xml");
        }

        [Test]
        public void Validator_ReturnTrue_OnValidRules()
        {
            var validRules = new RulesReader(validRulesPath).ReadRules();
            Assert.IsTrue(validator.IsValid(validRules));
        }

        [Test]
        public void Validator_ReturnFalse_OnInvalidRules()
        {
            var invalidRules = new RulesReader(invalidRulesPath).ReadRules();
            Assert.IsFalse(validator.IsValid(invalidRules));
        }
    }
}