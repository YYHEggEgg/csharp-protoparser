namespace YYHEggEgg.ProtoParser.RawData;

/// <summary>
/// OneofField is a constituent field of oneof.
/// </summary>
public class OneofField : WithCommentsBase
{
    public required string Type { get; set; }
    public required string FieldName { get; set; }
    public required string FieldNumber { get; set; }
    public required List<FieldOption> FieldOptions { get; set; }
}

/// <summary>
/// Oneof consists of oneof fields and a oneof name.
/// </summary>
public class Oneof : WithInlineCommentWithLeftCurlyBase
{
    public required List<OneofField> OneofFields { get; set; }
    public required string OneofName { get; set; }

    public required List<Option> Options { get; set; }
}