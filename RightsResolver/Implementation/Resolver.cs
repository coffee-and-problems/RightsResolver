using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

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

        public UserRights GetUserRights(RightsResolver.User user)
        {
            var allRules = new RulesReader(rulesPath).ReadRules();
            var applicableRules = new RulesFinder(allRules).GetApplicableRules(user);
            var allPossibleRights = new RulesApplier(products).ApplyRules(applicableRules);
            var actualRight = new Merger().MergeRights(allPossibleRights);
            return new UserRights(user.UserId, actualRight);
        }
    }
}
