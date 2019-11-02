using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Xml;
using System.Threading.Tasks;

namespace RightsResolver
{
    public class RulesReader
    {
        private readonly XmlDocument rulesDocument;

        public RulesReader(string rulesPath)
        {
            rulesDocument = new XmlDocument();
            rulesDocument = LoadXmlFromFile(rulesPath);
        }

        public XmlDocument LoadXmlFromFile(string fileName)
        {
            rulesDocument.Load(fileName);
            return rulesDocument;
        }

        public List<Rule> ReadRules()
        {
            var rules = new List<Rule>();

            foreach (XmlNode rule in rulesDocument.DocumentElement)
            {
                var productAccesses = new Dictionary<Platform, List<ProductRole>>();
                var platformAccesses = new Dictionary<Platform, Role>();
                var department = "";
                var post = "";

                foreach (XmlNode node in rule.ChildNodes)
                {
                    if (node.Name == "Access")
                    {
                        var (role, productAccess) = ReadAccess(node);
                        var platform = (Platform) Enum.Parse(typeof(Platform), node.SafeGet("Platform"), true);
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

            return rules;
        }

        private (Role?, List<ProductRole>) ReadAccess(XmlNode access)
        {
            Role? platformAccess = null;
            var productAccess = new List<ProductRole>();

            foreach (XmlNode node in access.ChildNodes)
            {
                if (node.Name == "Role")
                {
                    platformAccess = 
                        (Role) Enum.Parse(typeof(Role), access.SafeGet("Role"), true);
                }

                if (node.Name == "ProductRole")
                {
                    productAccess.Add(new ProductRole(
                        node.SafeGet("Product"),
                        (Role) Enum.Parse(typeof(Role), node.SafeGet("Role"), true)));
                }
            }

            return (platformAccess, productAccess.Count == 0? null : productAccess);
        }
    }

    public static class XmlNodeExtension
    {
        public static string SafeGet(this XmlNode node, string element)
        {
            if (node[element] != null) return node[element]?.InnerText;
            throw new ArgumentException($"Node {node.Name} does not contain element {element}");
        }
    }
}
