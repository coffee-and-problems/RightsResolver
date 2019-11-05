using System.Collections.Generic;

namespace RightsResolver
{
    public class Merger
    {
        private List<Rule> allRules;

        public Merger(List<Rule> allRules)
        {
            this.allRules = allRules;
        }

        public List<Rule> MergeRules(List<Rule> applicableRules)
        {
            return applicableRules;
        }
    }
}