namespace YYHEggEgg.ProtoParser.RawData;

public abstract class WithInlineCommentWithLeftCurlyBase : WithCommentsBase
{
    /// <summary>
    /// InlineCommentBehindLeftCurly is the optional one placed behind a left curly.
    /// </summary>
    public required Comment InlineCommentBehindLeftCurly { get; set; }
}