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
            var rulesToApply = new Merger(allRules).MergeRules(applicableRules);
            var (platformAccesses, productAccess) = new RulesApplier(products).ApplyRules(rulesToApply);
            return new UserRights(user.UserId, platformAccesses, productAccess);
        }
    }
}
