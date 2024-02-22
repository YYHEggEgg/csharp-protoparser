namespace YYHEggEgg.ProtoParser.RawData.RawMeta;

/// <summary>
/// Meta represents a meta information about the parsed element.
/// </summary>
public class Meta
{
    /// <summary>
    /// Pos is the source position.
    /// </summary>
    public required Position Pos { get; set; }
    /// <summary>
    /// LastPos is the last source position.
	/// Currently it is set when the parsed element type is
	/// syntax, package, comment, import, option, message, enum, oneof, rpc or service.
    /// </summary>
    public required Position LastPos { get; set; }
}