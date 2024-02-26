namespace YYHEggEgg.ProtoParser.RawData;

/// <summary>
/// Syntax is used to define the protobuf version.
/// </summary>
public class Syntax : WithCommentsBase
{
#if NET7_0_OR_GREATER
    public required string ProtobufVersion { get; set; }
#else
    public string ProtobufVersion { get; set; } = null!;
#endif
    /// <summary>
    /// ProtobufVersionQuote includes quotes
    /// </summary>
#if NET7_0_OR_GREATER
    public required string ProtobufVersionQuote { get; set; }
#else
    public string ProtobufVersionQuote { get; set; } = null!;
#endif
}