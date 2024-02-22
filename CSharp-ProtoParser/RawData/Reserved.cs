namespace YYHEggEgg.ProtoParser.RawData;

/// <summary>
/// Range is a range of field numbers. End is an optional value.
/// </summary>
public class Range
{
    public required string Begin { get; set; }
    public string? End { get; set; }
}

/// <summary>
/// Reserved declares a range of field numbers or field names that cannot be used in this message.
/// These component Ranges and FieldNames are mutually exclusive.
/// </summary>
public class Reserved : WithCommentsBase
{
    public required List<Range> Ranges { get; set; }
    public required List<string> FieldNames { get; set; }
}