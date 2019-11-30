﻿using System.Collections.Generic;
using NUnit.Framework;
using RightsResolver.Implementation;
using RightsResolver.Models;
using RightsResolver.BusinessObjects;
using Tests.Generators;

namespace Tests.Tests
{
    [TestFixture]
    public class RuleApplierShould
    {
        private RulesApplier applier;

        [SetUp]
        public void SetUp()
        {
            applier = new RulesApplier(AllProductsArray.Products);
        }

        [Test]
        public void ReturnEmpty_OnEmptyRules()
        {
            var rules = new List<Rule>();
            var rights = applier.ApplyRules(rules);

            Assert.IsEmpty(rights);
        }

        [Test]
        public void ApplyRulesCorrectly_WhenNoUnpack()
        {
            var rules = RulesGenerator.GenerateValidRules(false);
            var rights = applier.ApplyRules(rules);

            Assert.AreEqual(1, rights.Count);
            Assert.AreEqual(1, rights[0].PlatformAccesses.Count);
            Assert.AreEqual(1, rights[0].ProductAccesses.Count);

            var productList = rights[0].ProductAccesses[Platform.Support];
            Assert.AreEqual(1,productList.Count);
        }

        [Test]
        public void ApplyRules_WithSkipingUnknownProducts()
        {
            var rules = RulesGenerator.GenerateValidRules(false);
            rules[0].ProductAccesses[Platform.Support].Add("Unknown", Role.RoleII);
            var rights = applier.ApplyRules(rules);

            Assert.AreEqual(1, rights.Count);
            Assert.AreEqual(1, rights[0].PlatformAccesses.Count);
            Assert.AreEqual(1, rights[0].ProductAccesses.Count);

            var productList = rights[0].ProductAccesses[Platform.Support];
            Assert.AreEqual(1,productList.Count);
            Assert.IsFalse(productList.ContainsKey("Unknown"));
        }

        [Test]
        public void ApplyRulesCorrectly_WhenWithUnpack()
        {
            var rules = RulesGenerator.GenerateValidRules(true);
            var rights = applier.ApplyRules(rules);

            Assert.AreEqual(1, rights.Count);
            Assert.AreEqual(1, rights[0].PlatformAccesses.Count);
            Assert.AreEqual(1, rights[0].ProductAccesses.Count);

            var productList = rights[0].ProductAccesses[Platform.Support];
            Assert.AreEqual(5,productList.Count);
        }

        [Test] public void ApplyRules_WithAllFlagHavingLowestPriority()
        {
            var rules = RulesGenerator.GenerateValidRules(true);
            rules[0].ProductAccesses[Platform.Support]["Product1"] = Role.Admin;
            var rights = applier.ApplyRules(rules);
            
            Assert.AreEqual(1, rights.Count);
            Assert.AreEqual(1, rights[0].PlatformAccesses.Count);
            Assert.AreEqual(1, rights[0].ProductAccesses.Count);

            var productAccesses = rights[0].ProductAccesses[Platform.Support];
            Assert.AreEqual(5,productAccesses.Count);
            foreach (var product in AllProductsArray.Products)
            {
                Assert.AreEqual(product == "Product1" ? Role.Admin : Role.RoleI, productAccesses[product]);
            }
        }
    }
}