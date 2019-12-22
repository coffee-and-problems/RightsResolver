using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using RightsResolver.BusinessObjects;
using RightsResolver.Models;
using FluentAssertions;

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
        public void TestRightsResolverWhenAllIsFile()
        {
            var user = GetUser();
            var expectedRights = new UserRights(user.UserId, GetRights());
            var result = resolver.GetUserRights(new List<User> {user});
            var expectedResult = new Result(true, userRights: new List<UserRights> {expectedRights});
            
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void TestRightsResolverWhenIncorrectFile()
        {
            var resolverToFail = new RightsResolver.Implementation
                .RightsResolver("NotExists.xml", AllProductsArray.Products);
            var user = GetUser();
            var result = resolverToFail.GetUserRights(new List<User> {user});
            var expectedResult = new Result(false, "", errorType: ErrorTypes.IncorrectFile);

            result.Should().BeEquivalentTo(expectedResult, options => options.Excluding(x => x.Message));
            result.Message.Should().NotBeEmpty();
        }

        private User GetUser()
        {
            return new User(new Guid("FD624BB9-E6D5-4E57-9C10-59B282DBE9CE"),
                new List<Position> { new Position(new[] {0, 1}, "post1"),
                new Position(new[] {2}, "post2") });
        }

        private Rights GetRights()
        {
            var expectedProductRights = AllProductsArray.Products
                .ToDictionary(product => product, product => Role.RoleII);
            expectedProductRights["Product1"] = Role.Admin;
            return new Rights(
                new Dictionary<Platform, Role> {{ Platform.Health, Role.Admin }, { Platform.Oorv, Role.Admin }},
                new Dictionary<Platform, Dictionary<string, Role>> {{ Platform.Support, expectedProductRights }}
            );
        }
    }
}