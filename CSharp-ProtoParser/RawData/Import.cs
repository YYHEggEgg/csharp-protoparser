namespace YYHEggEgg.ProtoParser.RawData;

using ImportModifier = UInt32;

/// <summary>
/// Import is used to import another .proto's definitions.
/// </summary>
public class Import : WithCommentsBase
{
    public ImportModifier Modifier { get; set; }
#if NET7_0_OR_GREATER
    public required string Location { get; set; }
#else
    public string Location { get; set; } = null!;
#endif
}