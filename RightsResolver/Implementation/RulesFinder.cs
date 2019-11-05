﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace RightsResolver
{
    public class RulesFinder
    {
        private List<Rule> allRules;

        public RulesFinder(List<Rule> allRules)
        {
            this.allRules = allRules;
        }

        public List<Rule> GetApplicableRules(User user)
        {
            var applicableRules = new List<Rule>();
            foreach (var userPosition in user.UserPositions)
            {
                applicableRules.AddRange(GetRulesForPosition(userPosition));
            }

            return applicableRules;
        }

        private List<Rule> GetRulesForPosition(Position userPosition)
        {
            return allRules.Where(
                    rule => userPosition.Departments.Contains(rule.Department) 
                            && PostIsOkForRule(userPosition.Post, rule))
                .ToList();
        }

        private bool PostIsOkForRule(string post, Rule rule)
        {
            return string.Equals(rule.Post, "Any", StringComparison.OrdinalIgnoreCase)
                || string.Equals(rule.Post, post, StringComparison.OrdinalIgnoreCase);
        }
    }
}