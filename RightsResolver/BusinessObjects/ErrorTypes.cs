using System.ComponentModel;

namespace RightsResolver.BusinessObjects
{
    public enum ErrorTypes
    {
        [Description("Что-то пошло не так")]
        SomethingWentWrong,
        [Description("Некорректные правила")]
        WrongRules,
        [Description("Невалидные правила")]
        InvalidRules
    }
}