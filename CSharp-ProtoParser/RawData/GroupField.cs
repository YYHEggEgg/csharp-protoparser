namespace YYHEggEgg.ProtoParser.RawData;

/// <summary>
/// GroupField is one way to nest information in message definitions.
/// proto2 only.
/// </summary>
public class GroupField : WithInlineCommentWithLeftCurlyBase
{
    public bool IsRepeated { get; set; }
    public bool IsRequired { get; set; }
    public bool IsOptional { get; set; }
#if NET7_0_OR_GREATER
    public required string GroupName { get; set; }
    public required MessageBody MessageBody { get; set; }
    public required string FieldNumber { get; set; }
#else
    public string GroupName { get; set; } = null!;
    public MessageBody MessageBody { get; set; } = null!;
    public string FieldNumber { get; set; } = null!;
#endif
}