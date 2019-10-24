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
        private XmlDocument rulesDocument;

        public List<Rule> GetRulesDeclaration(string fileName)
        {
            rulesDocument = LoadXmlFromFile(fileName);
            return ReadRules();
        }

        public XmlDocument LoadXmlFromFile(string fileName)
        {
            rulesDocument = new XmlDocument();
            rulesDocument.Load(fileName);
            return rulesDocument;
        }

        private List<Rule> ReadRules()
        {
            var rules = new List<Rule>();

            foreach (XmlNode rule in rulesDocument.DocumentElement)
            {
                var accesses = new List<IAccess>();
                var department = "";
                var post = "";

                foreach (XmlNode userAndAccess in rule.ChildNodes)
                {
                    if (userAndAccess.Name == "Access")
                    {
                        accesses = ReadAccess(userAndAccess);
                    }
                    else if (userAndAccess.Name == "User")
                    {
                        department = userAndAccess.SafeGet("Department");
                        post = userAndAccess.SafeGet("Post");
                    }
                }

                rules.Add(new Rule(int.Parse(department), post, accesses));
            }

            return rules;
        }

        private List<IAccess> ReadAccess(XmlNode access)
        {
            var productRoles = new List<ProductRole>();
            var accesses = new List<IAccess>();

            foreach (XmlNode node in access.ChildNodes)
            {
                if (node.Name == "Role")
                {
                    accesses.Add(new PlatformAccess(
                        access.SafeGet("Platform"),
                        (Role) Enum.Parse(typeof(Role), access.SafeGet("Role"), true)));
                }

                if (node.Name == "ProductRole")
                {
                    productRoles.Add(new ProductRole(
                        node.SafeGet("Product"),
                        (Role) Enum.Parse(typeof(Role), node.SafeGet("Role"), true)));
                }
            }

            if (productRoles.Count != 0)
                accesses.Add(new ProductAccess(
                    access.SafeGet("Platform"),
                    productRoles));
            return accesses;
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
