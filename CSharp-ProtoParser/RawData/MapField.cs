namespace YYHEggEgg.ProtoParser.RawData;

/// <summary>
/// MapField is an associative map.
/// </summary>
public class MapField : WithCommentsBase
{
    public required string KeyType { get; set; }
    public required string Type { get; set; }
    public required string MapName { get; set; }
    public required string FieldNumber { get; set; }
    public List<FieldOption>? FieldOptions { get; set; }
}