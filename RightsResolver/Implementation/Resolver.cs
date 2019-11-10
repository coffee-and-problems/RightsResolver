using System.Collections.Generic;

namespace RightsResolver
{
    public class Resolver
    {
        private string[] products;
        private string rulesPath;

        public Resolver(string rulesPath, string[] products)
        {
            this.rulesPath = rulesPath;
            this.products = products;
        }

        public List<UserRights> GetUserRights(List<User> users)
        {
            var usersRights = new List<UserRights>();
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

            return usersRights;
        }
    }
}
