using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
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
        private static readonly List<Rule> AllRules = RulesGenerator.GenerateMultipleRules();
        private RulesFinder finder;

        [SetUp]
        public void SetUp()
        {
            finder = new RulesFinder(AllRules);
        }

        [TestCaseSource(nameof(GenerateRulesFinderCases))]
        public void TestRulesFinder(User user, List<Rule> expectedApplicableRules)
        {
            var applicableRules = finder.GetApplicableRules(user);
            applicableRules.Should().BeEquivalentTo(expectedApplicableRules);
        }

        private static IEnumerable<TestCaseData> GenerateRulesFinderCases()
        {
            yield return new TestCaseData(
                GetUser(new Position(new[] { 2 }, "post2")),
                new List<Rule> { AllRules[3] })
                .SetName("Только одно правило подходит. Успех");
            yield return new TestCaseData(
                GetUser(new Position(new[] { 0, 1 }, "post2")),
                new List<Rule> { AllRules[0], AllRules[2] })
                .SetName("Несколько департаментов. Успех");
            yield return new TestCaseData(
                GetUser(new Position(new[] { 0 }, "post1")),
                new List<Rule> { AllRules[0], AllRules[1] })
                .SetName("Несколько департаментов и подходящий пост. Успех");
            yield return new TestCaseData(
                GetUser(new Position(new[] { 0, 1 }, "post1"),
                    new Position(new[] { 2 }, "post2")),
                new List<Rule> { AllRules[0], AllRules[1], AllRules[2], AllRules[3] })
                .SetName("Несколько должностей. Успех");
            yield return new TestCaseData(
                GetUser(new Position(new int[0], "somePost")),
                new List<Rule>())
                .SetName("Ни одного подходящего. Возвращается пустой лист");
        }

        private static User GetUser(params Position[] positions)
        {
            return new User(new Guid("FD624BB9-E6D5-4E57-9C10-59B282DBE9CE"),
                positions.ToList());
        }
    }
}