using System.Collections.Concurrent;
using System.Runtime.Serialization;

namespace DataService.GetDataCacheHttpClient.HttpClientTinkoff.Extensions
{
    public static class EnumExtensions
    {
        private static readonly ConcurrentDictionary<object, string> CachedEnumMemberValues = new();

        public static string GetEnumMemberValue<T>(this T @enum) where T : Enum =>
            CachedEnumMemberValues.GetOrAdd(@enum, (e) => ((EnumMemberAttribute)typeof(T).GetMember(e.ToString())[0].GetCustomAttributes(typeof(EnumMemberAttribute), inherit: false)[0]).Value);
    }
}
