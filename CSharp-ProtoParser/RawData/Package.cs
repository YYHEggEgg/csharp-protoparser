namespace YYHEggEgg.ProtoParser.RawData;

/// <summary>
/// Package can be used to prevent name clashes between protocol message types.
/// </summary>
public class Package : WithCommentsBase
{
    public required string Name { get; set; }
}