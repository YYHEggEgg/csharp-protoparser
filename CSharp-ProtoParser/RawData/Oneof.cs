namespace YYHEggEgg.ProtoParser.RawData;

/// <summary>
/// OneofField is a constituent field of oneof.
/// </summary>
public class OneofField : WithCommentsBase
{
#if NET7_0_OR_GREATER
    public required string Type { get; set; }
    public required string FieldName { get; set; }
    public required string FieldNumber { get; set; }
#else
    public string Type { get; set; } = null!;
    public string FieldName { get; set; } = null!;
    public string FieldNumber { get; set; } = null!;
#endif
    public List<FieldOption>? FieldOptions { get; set; }
}

/// <summary>
/// Oneof consists of oneof fields and a oneof name.
/// </summary>
public class Oneof : WithInlineCommentWithLeftCurlyBase
{
    public List<OneofField>? OneofFields { get; set; }
#if NET7_0_OR_GREATER
    public required string OneofName { get; set; }
#else
    public string OneofName { get; set; } = null!;
#endif

    public List<Option>? Options { get; set; }
}