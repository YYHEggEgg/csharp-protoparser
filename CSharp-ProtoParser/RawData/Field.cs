namespace YYHEggEgg.ProtoParser.RawData;

/// <summary>
/// FieldOption is an option for the field.
/// </summary>
public class FieldOption
{
    public required string OptionName { get; set; }
    public required string Constant { get; set; }
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
    public required string Type { get; set; }
    public required string FieldName { get; set; }
    public required string FieldNumber { get; set; }
    public List<FieldOption>? FieldOptions { get; set; }
}