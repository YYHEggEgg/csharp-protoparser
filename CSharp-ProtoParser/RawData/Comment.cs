namespace YYHEggEgg.ProtoParser.RawData;

/// <summary>
/// Comment is a comment in either C/C++-style // and /* ... */ syntax.
/// </summary>
public class Comment : WithMetaBase
{
    /// <summary>
    /// Raw includes a comment syntax like // and /* */.
    /// </summary>
#if NET7_0_OR_GREATER
    public required string Raw { get; set; }
#else
    public string Raw { get; set; } = null!;
#endif
}