using System.IO;
using NUnit.Framework;
using RightsResolver;

namespace Tests
{
    [TestFixture]
    public class ValidatorShould
    {
        private Validator validator;

        [SetUp]
        public void SetUp()
        {
            validator = new Validator();
        }

        [Test]
        public void Validator_ReturnTrue_OnValidRules()
        {
            var validRules = RulesGenerator.GenerateValidRules(false);
            Assert.IsTrue(validator.IsValid(validRules));
        }

        [Test]
        public void Validator_ReturnFalse_OnInvalidRules()
        {
            var invalidRules = RulesGenerator.GenerateInvalidRules(false);
            Assert.IsFalse(validator.IsValid(invalidRules));

            invalidRules = RulesGenerator.GenerateInvalidRules(true);
            Assert.IsFalse(validator.IsValid(invalidRules));
        }
    }
}