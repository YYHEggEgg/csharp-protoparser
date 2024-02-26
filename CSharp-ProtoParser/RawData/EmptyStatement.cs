namespace YYHEggEgg.ProtoParser.RawData;

/// <summary>
/// EmptyStatement represents ";".
/// </summary>
public class EmptyStatement
{
    /// <summary>
    /// InlineComment is the optional one placed at the ending.
    /// </summary>
#if NET7_0_OR_GREATER
    public required Comment InlineComment { get; set; }
#else
    public Comment InlineComment { get; set; } = null!;
#endif
}