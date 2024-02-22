using System.Text.Json;
using System.Text.Json.Serialization;
using YYHEggEgg.ProtoParser.RawData;

namespace YYHEggEgg.ProtoParser.Aot;

#if NET8_0_OR_GREATER
[JsonSourceGenerationOptions(
    AllowTrailingCommas = true,
    ReadCommentHandling = JsonCommentHandling.Skip
)]
[JsonSerializable(typeof(Proto))]
public partial class ProtoContext : JsonSerializerContext
{

}
#endif