namespace YYHEggEgg.ProtoParser.RawData;

public class ExtendBody
{
    public List<Field>? Fields { get; set; }
    public List<EmptyStatement>? EmptyStatements { get; set; }
}

/// <summary>
/// Extend consists of a messageType and an extend body.
/// </summary>
public class Extend : WithInlineCommentWithLeftCurlyBase
{
#if NET7_0_OR_GREATER
    public required string MessageType { get; set; }
#else
    public string MessageType { get; set; } = null!;
#endif
    /// <summary>
    /// ExtendBody can have fields and emptyStatements
    /// </summary>
#if NET7_0_OR_GREATER
    public required ExtendBody ExtendBody { get; set; }
#else
    public ExtendBody ExtendBody { get; set; } = null!;
#endif
}