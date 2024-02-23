namespace YYHEggEgg.ProtoParser.RawData;

/// <summary>
/// Extensions declare that a range of field numbers in a message are available for third-party extensions.
/// </summary>
public class Extensions : WithCommentsBase
{
    public List<Range>? Ranges { get; set; }
}