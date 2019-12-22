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
        [NotNull]
        private string[] GetFilesNameByPath([NotNull]string rulesPath)
        {
            var isDirectory = Directory.Exists(rulesPath);
            if (!isDirectory && !File.Exists(rulesPath))
                throw new InvalidRulesException($"Не найден файл {rulesPath}", ErrorTypes.IncorrectFile); 

            var fileNames = isDirectory ? Directory.GetFiles(rulesPath) : new[] {rulesPath};
            return fileNames;
        }

        [NotNull]
        public List<Rule> ReadRules([NotNull] string rulesPath)
        {
            var rules = new List<Rule>();
            var fileNames = GetFilesNameByPath(rulesPath);
            foreach (var file in fileNames)
            {
                rules.ConcatOrCreate(ReadRulesFromFile(file));
            }

            return rules;
        }

        [NotNull]
        private List<Rule> ReadRulesFromFile(string file)
        {
            var rulesDocument = new XmlDocument();
            rulesDocument.Load(file);

            var rules = new List<Rule>();
            var documentElement = rulesDocument.DocumentElement;
            if (documentElement == null)
                throw new InvalidRulesException($"{file}", ErrorTypes.IncorrectFile);
            if (documentElement.ChildNodes.Count < 1)
                throw new InvalidRulesException($"Пустые правила: {file}", ErrorTypes.IncorrectFile);

            foreach (XmlNode xmlRule in documentElement)
            {
                var rule = ReadRule(xmlRule);
                if (new RuleValidator().IsValid(rule))
                    rules.Add(rule);
                else
                    throw new InvalidRulesException($"{file}", ErrorTypes.InvalidRules);
            }

            return rules;
        }

        private Rule ReadRule(XmlNode xmlRule)
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

            return new Rule(int.Parse(department), post, productAccesses, platformAccesses);
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
