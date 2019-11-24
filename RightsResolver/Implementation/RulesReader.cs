using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using JetBrains.Annotations;
using RightsResolver.BusinessObjects;
using RightsResolver.Implementation.Exceptions;
using RightsResolver.Implementation.Extensions;
using RightsResolver.Models;

namespace RightsResolver.Implementation
{
    public class RulesReader
    {
        [NotNull] private readonly XmlDocument rulesDocument;
        [NotNull] private readonly string rulesPath;

        public RulesReader([NotNull] string rulesPath)
        {
            this.rulesPath = rulesPath;
            rulesDocument = new XmlDocument();
            rulesDocument = LoadXmlFromFile();
        }

        private XmlDocument LoadXmlFromFile()
        {
            if (!File.Exists(rulesPath)) throw new InvalidRulesException($"Не найден файл {rulesPath}",
                ErrorTypes.NoRulesFound);
            rulesDocument.Load(rulesPath);
            return rulesDocument;
        }

        [NotNull]
        public List<Rule> ReadRules()
        {
            var rules = new List<Rule>();

            if (rulesDocument.DocumentElement == null)
                throw new InvalidRulesException($"{rulesPath}", ErrorTypes.InvalidRules);
            
            foreach (XmlNode xmlRule in rulesDocument.DocumentElement)
            {
                var productAccesses = new Dictionary<Platform, Dictionary<string, Role>>();
                var platformAccesses = new Dictionary<Platform, Role>();
                var department = "";
                var post = "";

                foreach (XmlNode userOrAccess in xmlRule.ChildNodes)
                {
                    if (userOrAccess.Name == "Access")
                    {
                        var (role, productAccess) = ReadAccess(userOrAccess);
                        var platform = (Platform) Enum.Parse(typeof(Platform),
                            userOrAccess.Get("Platform"), true);
                        if (role != null) platformAccesses.Add(platform, role.Value);
                        if (productAccess != null) productAccesses.Add(platform, productAccess);
                    }
                    else if (userOrAccess.Name == "User")
                    {
                        department = userOrAccess.Get("Department");
                        post = userOrAccess.Get("Post");
                    }
                }

                var rule = new Rule(int.Parse(department), post, productAccesses, platformAccesses);
                if (new Validator().IsValid(rule))
                    rules.Add(rule);
                else
                    throw new InvalidRulesException($"{rulesPath}", ErrorTypes.InvalidRules);
            }

            return rules;
        }

        private (Role?, Dictionary<string, Role>) ReadAccess(XmlNode access)
        {
            Role? platformAccess = null;
            var productAccess = new Dictionary<string, Role>();

            foreach (XmlNode roleOrProductRole in access.ChildNodes)
            {

                if (roleOrProductRole.Name == "Role")
                {
                    var role = access.Get("Role").SafeParseNullableEnum<Role>();
                    if (role == null) throw new InvalidRulesException("Не указана роль",
                        ErrorTypes.InvalidRules);
                    platformAccess = role;
                }

                if (roleOrProductRole.Name == "ProductRole")
                {
                    var product = roleOrProductRole.Get("Product");
                    if (productAccess.ContainsKey(product))
                        throw new InvalidRulesException("Продукт встречается неоднократно в рамках одного правила",
                            ErrorTypes.InvalidRules);

                    var role = roleOrProductRole.Get("Role").SafeParseNullableEnum<Role>();
                    if (role == null) throw new InvalidRulesException($"Для продукта {product} не указана роль",
                        ErrorTypes.InvalidRules);
                    productAccess.Add(product, role.Value);
                }
            }

            return (platformAccess, productAccess.Count > 0 ? productAccess : null);
        }
    }
}
