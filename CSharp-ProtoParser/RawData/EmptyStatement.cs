namespace YYHEggEgg.ProtoParser.RawData;

/// <summary>
/// EmptyStatement represents ";".
/// </summary>
public class EmptyStatement
{
    /// <summary>
    /// InlineComment is the optional one placed at the ending.
    /// </summary>
    public required Comment InlineComment { get; set; }
}