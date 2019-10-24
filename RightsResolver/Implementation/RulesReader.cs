using System;
using System.Collections.Generic;
using System.Linq;
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

                foreach (XmlNode userAndAccess in rule.ChildNodes)
                {
                    if (userAndAccess.Name == "Access")
                    {
                        accesses = ReadAccess(userAndAccess);
                    }
                }

                rules.Add(new Rule(
                    int.Parse(rule["User"]["Position"]["Department"].InnerXml),
                    rule["User"]["Position"]["Post"].InnerText,
                    accesses));
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
                        access["Platform"].InnerText,
                        (Role) Enum.Parse(typeof(Role), access["Role"].InnerText, true)));
                }

                if (node.Name == "ProductRole")
                {
                    productRoles.Add(new ProductRole(
                        node["Product"].InnerText,
                        (Role) Enum.Parse(typeof(Role), node["Role"].InnerText, true)));
                }
            }

            if (productRoles.Count != 0)
                accesses.Add(new ProductAccess(
                    access["Platform"].InnerText,
                    productRoles));
            return accesses;
        }
    }
}
