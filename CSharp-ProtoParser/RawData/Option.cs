namespace YYHEggEgg.ProtoParser.RawData;

/// <summary>
/// Option can be used in proto files, messages, enums and services.
/// </summary>
public class Option : WithCommentsBase
{
    public required string OptionName { get; set; }
    public required string Constant { get; set; }
}