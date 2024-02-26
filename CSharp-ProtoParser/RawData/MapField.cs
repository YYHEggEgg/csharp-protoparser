namespace YYHEggEgg.ProtoParser.RawData;

/// <summary>
/// MapField is an associative map.
/// </summary>
public class MapField : WithCommentsBase
{
#if NET7_0_OR_GREATER
    public required string KeyType { get; set; }
    public required string Type { get; set; }
    public required string MapName { get; set; }
    public required string FieldNumber { get; set; }
#else
    public string KeyType { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string MapName { get; set; } = null!;
    public string FieldNumber { get; set; } = null!;
#endif
    public List<FieldOption>? FieldOptions { get; set; }
}