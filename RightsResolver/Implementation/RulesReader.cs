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
                var productAccesses = new List<ProductAccessDTO>();
                var platformAccesses = new List<PlatformAccessDTO>();
                var department = "";
                var post = "";

                foreach (XmlNode userAndAccess in rule.ChildNodes)
                {
                    if (userAndAccess.Name == "Access")
                    {
                        var (platformAccess, productAccess) = ReadAccess(userAndAccess);
                        if (platformAccess != null) platformAccesses.Add(platformAccess);
                        if (productAccess != null) productAccesses.Add(productAccess);
                    }
                    else if (userAndAccess.Name == "User")
                    {
                        department = userAndAccess.SafeGet("Department");
                        post = userAndAccess.SafeGet("Post");
                    }
                }

                rules.Add(new Rule(int.Parse(department), post, productAccesses, platformAccesses));
            }

            return rules;
        }

        private (PlatformAccessDTO, ProductAccessDTO) ReadAccess(XmlNode access)
        {
            PlatformAccessDTO platformAccess = null;
            ProductAccessDTO productAccess = null;

            var productRoles = new List<ProductRole>();
            foreach (XmlNode node in access.ChildNodes)
            {
                if (node.Name == "Role")
                {
                    platformAccess = new PlatformAccessDTO(
                        access.SafeGet("Platform"),
                        (Role) Enum.Parse(typeof(Role), access.SafeGet("Role"), true));
                }

                if (node.Name == "ProductRole")
                {
                    productRoles.Add(new ProductRole(
                        node.SafeGet("Product"),
                        (Role) Enum.Parse(typeof(Role), node.SafeGet("Role"), true)));
                }
            }

            if (productRoles.Count > 0) 
                productAccess = new ProductAccessDTO(access.SafeGet("Platform"), productRoles);

            return (platformAccess, productAccess);
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
