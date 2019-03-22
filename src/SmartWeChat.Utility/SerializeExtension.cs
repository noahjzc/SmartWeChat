using Newtonsoft.Json;

namespace SmartWeChat.Utility
{
    public static class SerializeExtension
    {
        #region json

        public static string JsonSerialize(this object value)
        {
            return JsonSerialize(value, Formatting.Indented);
        }

        public static string JsonSerialize(this object value, Formatting formatting)
        {
            return JsonConvert.SerializeObject(value, formatting);
        }

        public static T JsonDeserialize<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        #endregion
    }
}
