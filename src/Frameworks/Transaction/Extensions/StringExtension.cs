namespace Transaction.Framework.Extensions
{
    using System;

    public static class StringExtension
    {
        public static TEnum TryParseEnum<TEnum>(this string item) where TEnum : struct
        {
            return Enum.TryParse(item, true, out TEnum tEnumResult) ?
                   tEnumResult : default(TEnum);
        }
    }
}
