namespace YYHEggEgg.ProtoParser.RawData;

/// <summary>
/// FieldOption is an option for the field.
/// </summary>
public class FieldOption
{
#if NET7_0_OR_GREATER
    public required string OptionName { get; set; }
#else
    public string OptionName { get; set; } = null!;
#endif
#if NET7_0_OR_GREATER
    public required string Constant { get; set; }
#else
    public string Constant { get; set; } = null!;
#endif
}

/// <summary>
/// Field is a normal field that is the basic element of a protocol buffer message.
/// </summary>
public class Field : WithCommentsBase
{
    public bool IsRepeated { get; set; }
    /// <summary>
    /// proto2 only
    /// </summary>
    public bool IsRequired { get; set; }
    /// <summary>
    /// proto2 only
    /// </summary>
    public bool IsOptional { get; set; }
#if NET7_0_OR_GREATER
    public required string Type { get; set; }
#else
    public string Type { get; set; } = null!;
#endif
#if NET7_0_OR_GREATER
    public required string FieldName { get; set; }
#else
    public string FieldName { get; set; } = null!;
#endif
#if NET7_0_OR_GREATER
    public required string FieldNumber { get; set; }
#else
    public string FieldNumber { get; set; } = null!;
#endif
    public List<FieldOption>? FieldOptions { get; set; }
}