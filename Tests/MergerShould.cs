﻿using System.IO;
using NUnit.Framework;
using RightsResolver;

namespace Tests
{
    [TestFixture]
    public class MergerShould
    {
        private Merger merger;
        private RulesApplier applier;
        private string rulesDirectory;

        [SetUp]
        public void SetUp()
        {
            merger = new Merger();

            applier = new RulesApplier(AllProductsArray.Products);
            var currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            rulesDirectory = Path.Combine(currentDirectory, "Rules");
        }

        [Test]
        public void MergeCorrectly()
        {
            var rulePath = Path.Combine(rulesDirectory, "TestRules.xml");
            var rules = new RulesReader(rulePath).ReadRules();
            var allRights = applier.ApplyRules(rules);
            var mergedRules = merger.MergeRights(allRights);

            Assert.AreEqual(2, mergedRules.PlatformAccesses.Count);
            Assert.AreEqual(1, mergedRules.ProductAccesses.Count);
            Assert.AreEqual(5, mergedRules.ProductAccesses[Platform.Support].Count);
        }
    }
}