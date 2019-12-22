using FluentAssertions;
using NUnit.Framework;
using RightsResolver.Implementation;
using Tests.Generators;

namespace Tests.Tests
{
    [TestFixture]
    public class RuleValidatorShould
    {
        private RuleValidator validator;

        [SetUp]
        public void SetUp()
        {
            validator = new RuleValidator();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void TestRulesValidatorOnValidRules(bool allProductsFlag)
        {
            var validRules = RulesGenerator.GenerateValidRules(allProductsFlag);
            foreach (var rule in validRules)
            {
                validator.IsValid(rule).Should().BeTrue();
            }
        }

        [TestCase(true)]
        [TestCase(false)]
        public void TestRulesValidatorOnInvalidRules(bool wrongPlatformAccesses)
        {
            var invalidRules = RulesGenerator.GenerateInvalidRules(wrongPlatformAccesses);
            foreach (var rule in invalidRules)
            {
                validator.IsValid(rule).Should().BeFalse();
            }
        }
    }
}