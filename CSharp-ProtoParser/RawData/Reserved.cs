namespace YYHEggEgg.ProtoParser.RawData;

/// <summary>
/// Range is a range of field numbers. End is an optional value.
/// </summary>
public class Range
{
#if NET7_0_OR_GREATER
    public required string Begin { get; set; }
#else
    public string Begin { get; set; } = null!;
#endif
    public string? End { get; set; }
}

/// <summary>
/// Reserved declares a range of field numbers or field names that cannot be used in this message.
/// These component Ranges and FieldNames are mutually exclusive.
/// </summary>
public class Reserved : WithCommentsBase
{
    public List<Range>? Ranges { get; set; }
    public List<string>? FieldNames { get; set; }
}