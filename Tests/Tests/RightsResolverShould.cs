using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using RightsResolver.BusinessObjects;
using RightsResolver.Implementation;
using RightsResolver.Models;

namespace Tests.Tests
{
    [TestFixture]
    public class RightsResolverShould
    {
        private RightsResolver.Implementation.RightsResolver resolver;

        [SetUp]
        public void SetUp()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var rulesPath = Path.Combine(currentDirectory, "Rules", "Valid", "MultipleRules.xml");
            resolver = new RightsResolver.Implementation.RightsResolver(rulesPath, AllProductsArray.Products);
        }

        [Test]
        public void ResolveRight_WhenAllIsFile()
        {
            var user = GetUser();
            var expectedRights = GetRights();
            var result = resolver.GetUserRights(new List<User> {user});
            
            Assert.IsTrue(result.IsSuccessful);
            Assert.NotNull(result.UserRights);
            Assert.AreEqual(user.UserId, result.UserRights[0].UserId);
            var actualRights = result.UserRights[0].Rights;
            Assert.AreEqual(expectedRights.PlatformAccesses, actualRights.PlatformAccesses);
            Assert.AreEqual(expectedRights.ProductAccesses, actualRights.ProductAccesses);
            Assert.IsNull(result.Message);
            Assert.IsNull(result.ErrorType);
        }

        [Test]
        public void CreateFail_WhenNoFile()
        {
            var resolverToFail = new RightsResolver.Implementation.RightsResolver("NotExists.xml", AllProductsArray.Products);
            var user = GetUser();
            var result = resolverToFail.GetUserRights(new List<User> { user });

            Assert.IsFalse(result.IsSuccessful);
            Assert.IsNotEmpty(result.Message);
            Assert.AreEqual(ErrorTypes.WrongFile, result.ErrorType);
            Assert.IsNull(result.UserRights);
        }

        private User GetUser()
        {
            return new User(new Guid("FD624BB9-E6D5-4E57-9C10-59B282DBE9CE"),
                new List<Position> { new Position(new[] { 0, 1 }, "post1"),
                new Position(new[] { 2 }, "post2") });
        }

        private RuleRights GetRights()
        {
            var expectedProductRights = AllProductsArray.Products
                .ToDictionary(product => product, product => Role.RoleII);
            expectedProductRights["Product1"] = Role.Admin;
            return new RuleRights(
                new Dictionary<Platform, Role> {{ Platform.Health, Role.Admin }, { Platform.Oorv, Role.Admin }},
                new Dictionary<Platform, Dictionary<string, Role>> {{ Platform.Support, expectedProductRights }}
            );
        }
    }
}