using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Elhori.Portfolio.Domain.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum value)
    {
        var enumType = value.GetType();
        var enumValueName = Enum.GetName(enumType, value)!;

        MemberInfo memberInfo = enumType.GetField(enumValueName)!;
        var displayAttribute = memberInfo.GetCustomAttribute<DisplayAttribute>()!;

        return displayAttribute?.Name ?? enumValueName;
    }
}