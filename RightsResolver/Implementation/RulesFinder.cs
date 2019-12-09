using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using RightsResolver.BusinessObjects;
using RightsResolver.Models;

namespace RightsResolver.Implementation
{
    public class RulesFinder
    {
        [NotNull] private readonly List<Rule> allRules;

        public RulesFinder([NotNull] List<Rule> allRules)
        {
            this.allRules = allRules;
        }

        [NotNull]
        public List<Rule> GetApplicableRules(User user)
        {
            var applicableRules = new List<Rule>();
            foreach (var userPosition in user.UserPositions)
            {
                applicableRules.AddRange(GetRulesForPosition(userPosition));
            }

            return applicableRules;
        }

        [NotNull]
        private List<Rule> GetRulesForPosition(Position userPosition)
        {
            return allRules.Where(
                    rule => userPosition.Departments.Contains(rule.Department) 
                            && PostIsOkForRule(userPosition.Post, rule.Post))
                   .ToList();
        }

        private bool PostIsOkForRule(string userPost, string rulePost)
        {
            return string.Equals(rulePost, "Any", StringComparison.OrdinalIgnoreCase)
                || string.Equals(rulePost, userPost, StringComparison.OrdinalIgnoreCase);
        }
    }
}