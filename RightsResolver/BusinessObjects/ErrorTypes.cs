using System.ComponentModel;

namespace RightsResolver.BusinessObjects
{
    public enum ErrorTypes
    {
        [Description("Что-то пошло не так")]
        SomethingWentWrong,
        [Description("Некорректный файл")]
        WrongFile,
        [Description("Невалидные правила")]
        InvalidRules
    }
}