using System.Xml;
using JetBrains.Annotations;

namespace RightsResolver
{
    public static class XmlNodeExtension
    {
        [NotNull]
        public static string SafeGet(this XmlNode node, string element)
        {
            if (node[element] != null) return node[element]?.InnerText;
            throw new InvalidRulesException($"Нод {node.Name} не содержит элемента {element}");
        }
    }
}