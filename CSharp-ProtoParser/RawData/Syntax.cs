namespace YYHEggEgg.ProtoParser.RawData;

/// <summary>
/// Syntax is used to define the protobuf version.
/// </summary>
public class Syntax : WithCommentsBase
{
    public required string ProtobufVersion { get; set; }
    /// <summary>
    /// ProtobufVersionQuote includes quotes
    /// </summary>
    public required string ProtobufVersionQuote { get; set; }
}