﻿using NUnit.Framework;
using RightsResolver.Implementation;
using Tests.Generators;

namespace Tests.Tests
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
            foreach (var rule in validRules)
            {
                Assert.IsTrue(validator.IsValid(rule));
            }
        }

        [Test]
        public void Validator_ReturnFalse_OnInvalidRules()
        {
            var invalidRules = RulesGenerator.GenerateInvalidRules(false);
            foreach (var rule in invalidRules)
            {
                Assert.IsFalse(validator.IsValid(rule));
            }

            invalidRules = RulesGenerator.GenerateInvalidRules(true);
            foreach (var rule in invalidRules)
            {
                Assert.IsFalse(validator.IsValid(rule));
            }
        }
    }
}