using System.ComponentModel;

namespace CCM.TesteAcesso.Domain.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription<TEnum>(this TEnum enumValue)
                    where TEnum : Enum
        {
            if (!typeof(TEnum).IsEnum)
                return string.Empty;

            var description = Enum.GetName(typeof(TEnum), enumValue);

            var fieldInfo = typeof(TEnum).GetField(enumValue.ToString());

            if (fieldInfo == null) return description;

            var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attrs is { Length: > 0 })
            {
                description = ((DescriptionAttribute)attrs[0]).Description;
            }

            return description;
        }
    }
}
