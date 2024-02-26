namespace YYHEggEgg.ProtoParser.RawData.RawMeta;

/// <summary>
/// Meta represents a meta information about the parsed element.
/// </summary>
public class Meta
{
    /// <summary>
    /// Pos is the source position.
    /// </summary>
#if NET7_0_OR_GREATER
    public required Position Pos { get; set; }
#else
    public Position Pos { get; set; } = null!;
#endif
    /// <summary>
    /// LastPos is the last source position.
	/// Currently it is set when the parsed element type is
	/// syntax, package, comment, import, option, message, enum, oneof, rpc or service.
    /// </summary>
#if NET7_0_OR_GREATER
    public required Position LastPos { get; set; }
#else
    public Position LastPos { get; set; } = null!;
#endif
}