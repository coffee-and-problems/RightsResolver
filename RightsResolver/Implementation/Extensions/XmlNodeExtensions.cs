using System.Xml;
using JetBrains.Annotations;
using RightsResolver.BusinessObjects;
using RightsResolver.Implementation.Exceptions;

namespace RightsResolver.Implementation.Extensions
{
    public static class XmlNodeExtension
    {
        [NotNull]
        public static string Get([NotNull] this XmlNode node, string element)
        {
            if (node[element] != null) return node[element]?.InnerText;
            throw new InvalidRulesException($"Нод {node.Name} не содержит элемента {element}",
                ErrorTypes.InvalidRules);
        }
    }
}