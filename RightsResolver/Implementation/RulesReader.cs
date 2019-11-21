using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using JetBrains.Annotations;

namespace RightsResolver
{
    public class RulesReader
    {
        private readonly XmlDocument rulesDocument;
        private readonly string rulesPath;

        public RulesReader(string rulesPath)
        {
            this.rulesPath = rulesPath;
            rulesDocument = new XmlDocument();
            rulesDocument = LoadXmlFromFile();
        }

        private XmlDocument LoadXmlFromFile()
        {
            if (!File.Exists(rulesPath)) throw new InvalidRulesException($"Не найден файл с правилами {rulesPath}");
            rulesDocument.Load(rulesPath);
            return rulesDocument;
        }

        [NotNull]
        public List<Rule> ReadRules()
        {
            var rules = new List<Rule>();

            if (rulesDocument.DocumentElement != null)
            {
                foreach (XmlNode rule in rulesDocument.DocumentElement)
                {
                    var productAccesses = new Dictionary<Platform, Dictionary<string, Role>>();
                    var platformAccesses = new Dictionary<Platform, Role>();
                    var department = "";
                    var post = "";

                    foreach (XmlNode node in rule.ChildNodes)
                    {
                        if (node.Name == "Access")
                        {
                            var (role, productAccess) = ReadAccess(node);
                            var platform = (Platform) Enum.Parse(typeof(Platform),
                                node.SafeGet("Platform"), true);
                            if (role != null) platformAccesses.Add(platform, role.Value);
                            if (productAccess != null) productAccesses.Add(platform, productAccess);
                        }
                        else if (node.Name == "User")
                        {
                            department = node.SafeGet("Department");
                            post = node.SafeGet("Post");
                        }
                    }

                    rules.Add(new Rule(int.Parse(department), post, productAccesses, platformAccesses));
                }

                if (new Validator().IsValid(rules)) return rules;
            }
            
            throw new InvalidRulesException($"Некорректные правила {rulesPath}");
        }

        private (Role?, Dictionary<string, Role>) ReadAccess(XmlNode access)
        {
            Role? platformAccess = null;
            var productAccess = new Dictionary<string, Role>();

            foreach (XmlNode node in access.ChildNodes)
            {
                if (node.Name == "Role")
                {
                    platformAccess = 
                        (Role) Enum.Parse(typeof(Role), access.SafeGet("Role"), true);
                }

                if (node.Name == "ProductRole")
                {
                    if (productAccess.ContainsKey(node.SafeGet("Product")))
                        throw new InvalidRulesException("Продукт встречается неоднократно в рамках одного правила");
                    productAccess.Add(node.SafeGet("Product"),
                        (Role) Enum.Parse(typeof(Role), node.SafeGet("Role"), true));
                }
            }

            return (platformAccess, productAccess.Count > 0? productAccess : null);
        }
    }
}
