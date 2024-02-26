namespace YYHEggEgg.ProtoParser.RawData;

/// <summary>
/// Package can be used to prevent name clashes between protocol message types.
/// </summary>
public class Package : WithCommentsBase
{
#if NET7_0_OR_GREATER
    public required string Name { get; set; }
#else
    public string Name { get; set; } = null!;
#endif
}