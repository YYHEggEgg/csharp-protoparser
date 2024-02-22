namespace YYHEggEgg.ProtoParser.RawData.RawMeta;

/// <summary>
/// Position represents a source position.
/// </summary>
public class Position
{
    /// <summary>
    /// Filename is a name of file, if any
    /// </summary>
    public required string Filename { get; set; }
    /// <summary>
    /// Offset is a byte offset, starting at 0
    /// </summary>
    public int Offset { get; set; }
    /// <summary>
    /// Line is a line number, starting at 1
    /// </summary>
    public int Line { get; set; }
    /// <summary>
    /// Column is a column number, starting at 1 (character count per line)
    /// </summary>
    public int Column { get; set; }
}