using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RightsResolver.Implementation;
using RightsResolver.BusinessObjects;
using RightsResolver.Models;
using Tests.Generators;

namespace Tests.Tests
{
    [TestFixture]
    public class RulesFinderShould
    {
        private List<Rule> rules;
        private RulesFinder finder;

        [SetUp]
        public void SetUp()
        {
            rules = RulesGenerator.GenerateMultipleRules();
            finder = new RulesFinder(rules);
        }

        [Test]
        public void FindRules_WhenOnlyOneIsApplicable()
        {
            var user = GetUser(new Position(new[] { 2 }, "post2"));
            var applicableRules = finder.GetApplicableRules(user);
            Assert.AreEqual(1, applicableRules.Count);
            Assert.AreEqual(rules[3], applicableRules[0]);
        }

        [Test]
        public void FindRules_WhenMultipleDepartments()
        {
            var user = GetUser(new Position(new[] { 0, 1 }, "post2"));
            var applicableRules = finder.GetApplicableRules(user);
            Assert.AreEqual(2, applicableRules.Count);
            Assert.AreEqual(rules[0], applicableRules[0]);
            Assert.AreEqual(rules[2], applicableRules[1]);
        }

        [Test]
        public void FindRules_WhenDepartmentAndPost()
        {
            var user = GetUser(new Position(new[] { 0 }, "post1"));
            var applicableRules = finder.GetApplicableRules(user);
            Assert.AreEqual(2, applicableRules.Count);
            Assert.AreEqual(rules[0], applicableRules[0]);
            Assert.AreEqual(rules[1], applicableRules[1]);
        }

        [Test]
        public void FindRules_WhenMultiplePositions()
        {
            var user = GetUser(new Position(new[] { 0, 1 }, "post1"),
                new Position(new[] { 2 }, "post2"));
            var applicableRules = finder.GetApplicableRules(user);
            Assert.AreEqual(4, applicableRules.Count);
            Assert.AreEqual(rules[0], applicableRules[0]);
            Assert.AreEqual(rules[1], applicableRules[1]);
            Assert.AreEqual(rules[2], applicableRules[2]);
            Assert.AreEqual(rules[3], applicableRules[3]);
        }

        private User GetUser(params Position[] positions)
        {
            return new User(new Guid("FD624BB9-E6D5-4E57-9C10-59B282DBE9CE"),
                positions.ToList());
        }
    }
}