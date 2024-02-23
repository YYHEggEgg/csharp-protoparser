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
    public required string MessageType { get; set; }
    /// <summary>
    /// ExtendBody can have fields and emptyStatements
    /// </summary>
    public required ExtendBody ExtendBody { get; set; }
}