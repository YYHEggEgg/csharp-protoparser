namespace YYHEggEgg.ProtoParser.RawData;

public abstract class WithCommentsBase : WithMetaBase
{
    /// <summary>
    /// Comments are the optional ones placed at the beginning.
    /// </summary>
    public required List<Comment> Comments { get; set; }
    /// <summary>
    /// InlineComment is the optional one placed at the ending.
    /// </summary>
    public required Comment InlineComment { get; set; }
}