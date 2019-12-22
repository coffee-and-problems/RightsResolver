using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using RightsResolver.BusinessObjects;
using RightsResolver.Models;
using RightsResolver.Implementation;
using RightsResolver.Implementation.Exceptions;
using Tests.Generators;

namespace Tests.Tests
{
    [TestFixture]
    public class RulesReaderShould
    {
        private RulesReader reader;

        [SetUp]
        public void SetUp()
        {
            reader = new RulesReader();
        }

        [TestCaseSource(nameof(GenerateRulesReaderSuccesses))]
        public void TestRulesReaderWhenAllFine(string rulesPath, List<Rule> expectedRules)
        {
            var rules = reader.ReadRules(rulesPath);
            rules.Should().BeEquivalentTo(expectedRules);
        }

        [TestCaseSource(nameof(GenerateRulesReaderFailures))]
        public void TestRulesReaderWhenReadFails(string rulesPath, ErrorTypes errorType)
        {
            Action read = () => reader.ReadRules(rulesPath);

            read.Should().Throw<InvalidRulesException>()
                .Where(e => e.ErrorType == errorType)
                .Where(e => !string.IsNullOrEmpty(e.Message));
        }

        private static IEnumerable<TestCaseData> GenerateRulesReaderSuccesses()
        {
            var currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            var rulesDirectory = Path.Combine(currentDirectory, "Rules");
            var validRulesDirectory = Path.Combine(rulesDirectory, "Valid");
            var testRules = RulesGenerator.GenerateValidRules(true);
            var multipleRules = RulesGenerator.GenerateMultipleRules();

            yield return new TestCaseData(
                Path.Combine(validRulesDirectory, "TestRules.xml"),
                testRules
            ).SetName("Корректный файл. Успех");

            yield return new TestCaseData(
                Path.Combine(validRulesDirectory, "MultipleRules.xml"),
                multipleRules
                ).SetName("Множественные правила в одном файле. Успех");

            yield return new TestCaseData(
                validRulesDirectory,
                testRules.Concat(multipleRules).ToList()
                ).SetName("Чтение всех правил в директории. Успех");
        }

        private static IEnumerable<TestCaseData> GenerateRulesReaderFailures()
        {
            var currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            var rulesDirectory = Path.Combine(currentDirectory, "Rules");
            var invalidRulesDirectory = Path.Combine(rulesDirectory, "Invalid");

            yield return new TestCaseData(
                Path.Combine(invalidRulesDirectory, "NotExists.xml"),
                ErrorTypes.IncorrectFile)
                .SetName("Не найден файл. Исключение");
            yield return new TestCaseData(
                    Path.Combine(invalidRulesDirectory, "EmptyFile.xml"),
                    ErrorTypes.IncorrectFile)
                .SetName("Пустой файл. Исключение");
            yield return new TestCaseData(
                    Path.Combine(invalidRulesDirectory, "InvalidRules.xml"),
                    ErrorTypes.InvalidRules)
                .SetName("Невалидные правила. Исключение");
            yield return new TestCaseData(
                    Path.Combine(invalidRulesDirectory, "NodeMissing.xml"),
                    ErrorTypes.InvalidRules)
                .SetName("Некорректная структура правила. Исключение");
        }
    }
}