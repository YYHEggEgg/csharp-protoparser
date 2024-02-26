namespace YYHEggEgg.ProtoParser.RawData;

/// <summary>
/// Option can be used in proto files, messages, enums and services.
/// </summary>
public class Option : WithCommentsBase
{
#if NET7_0_OR_GREATER
    public required string OptionName { get; set; }
    public required string Constant { get; set; }
#else
    public string OptionName { get; set; } = null!;
    public string Constant { get; set; } = null!;
#endif
}