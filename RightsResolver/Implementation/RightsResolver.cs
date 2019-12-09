using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using RightsResolver.BusinessObjects;
using RightsResolver.Implementation.Exceptions;

namespace RightsResolver.Implementation
{
    public class RightsResolver
    {
        [NotNull] private readonly string[] products;
        [NotNull] private readonly string rulesPath;

        public RightsResolver([NotNull] string rulesPath, [NotNull] string[] products)
        {
            this.rulesPath = rulesPath;
            this.products = products;
        }

        [NotNull]
        public Result GetUserRights([NotNull] List<User> users)
        {
            var usersRights = new List<UserRights>();

            try
            {
                var allRules = new RulesReader(rulesPath).ReadRules();
                var rulesFinder = new RulesFinder(allRules);
                var rulesApplier = new RulesApplier(products);
                var rightsMerger = new RightsMerger();

                foreach (var user in users)
                {
                    var applicableRules = rulesFinder.GetApplicableRules(user);
                    var allPossibleRights = rulesApplier.ApplyRules(applicableRules);
                    var actualRight = rightsMerger.MergeRights(allPossibleRights);
                    usersRights.Add(new UserRights(user.UserId, actualRight));
                }
            }
            catch (InvalidRulesException e)
            {
                return Result.CreateFail(e.Message, e.ErrorType);
            }
            catch (Exception e)
            {
                return Result.CreateFail(e.Message);
            }
            
            return Result.CreateSuccess(usersRights);
        }
    }
}
