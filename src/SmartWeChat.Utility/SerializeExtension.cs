using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Formatting = Newtonsoft.Json.Formatting;

namespace SmartWeChat.Utility
{
    public static class SerializeExtension
    {
        #region json

        public static string JsonSerialize(this object value, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(value, formatting);
        }

        public static T JsonDeserialize<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        #endregion

        #region xml

        public static T XmlDeserialize<T>(this string value) where T : class
        {
            var serializer = new XmlSerializer(typeof(T), new XmlRootAttribute("xml"));

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(value)))
            {
                return serializer.Deserialize(ms) as T;
            }
        }

        #endregion
    }
}
