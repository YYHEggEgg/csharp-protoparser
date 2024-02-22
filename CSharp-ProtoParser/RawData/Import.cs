namespace YYHEggEgg.ProtoParser.RawData;

using ImportModifier = UInt32;

/// <summary>
/// Import is used to import another .proto's definitions.
/// </summary>
public class Import : WithCommentsBase
{
    public ImportModifier Modifier { get; set; }
    public required string Location { get; set; }
}