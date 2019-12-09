using System.ComponentModel;

namespace RightsResolver.BusinessObjects
{
    public enum ErrorTypes
    {
        [Description("Что-то пошло не так")]
        SomethingWentWrong,
        [Description("Некорректный файл правил")]
        IncorrectFile,
        [Description("Невалидные правила")]
        InvalidRules
    }
}