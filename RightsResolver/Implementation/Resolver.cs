using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace RightsResolver
{
    public class Resolver
    {
        private readonly string[] products;
        private readonly string rulesPath;

        public Resolver(string rulesPath, string[] products)
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
                var applier = new RulesApplier(products);
                var merger = new Merger();

                foreach (var user in users)
                {
                    var applicableRules = rulesFinder.GetApplicableRules(user);
                    var allPossibleRights = applier.ApplyRules(applicableRules);
                    var actualRight = merger.MergeRights(allPossibleRights);
                    usersRights.Add(new UserRights(user.UserId, actualRight));
                }
            }
            catch (Exception e)
            {
                return Result.GenerateFail(e.Message);
            }
            
            return Result.GenerateSuccess(usersRights);
        }
    }
}
