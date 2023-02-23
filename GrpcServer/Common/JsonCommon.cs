using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using Google.Protobuf;

namespace GrpcServer.Common
{
    public static class JsonCommon
    {
        public static JsonSerializerOptions Options => new()
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
        };


        /// <summary>
        /// Jsonシリアライズ
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(object? obj)
        {
            try
            {
                return JsonSerializer.Serialize(obj, Options);
            }
            catch (Exception ex)
            {             
                return String.Empty;
            }
        }

        /// <summary>
        /// oogle.Protobuf.ByteString変換
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static ByteString ToProtoByte(string? str)
        {
            if (str is null)
            {
                return ByteString.Empty;
            }
            return ByteString.CopyFrom(Encoding.UTF8.GetBytes(str).AsSpan());
        }

        public static T? FormJson<T>(Google.Protobuf.ByteString bytes)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(bytes.ToByteArray());
            }
            catch (Exception)
            {
                return default(T?);

                throw;
            }
        }   
    }
}
