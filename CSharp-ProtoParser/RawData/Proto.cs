namespace YYHEggEgg.ProtoParser.RawData;

/// <summary>
/// ProtoMeta represents a meta information about the Proto.
/// </summary>
public class ProtoMeta
{
    /// <summary>
    /// Filename is a name of file, if any.
    /// </summary>
    public required string Filename { get; set; }
}

public class ProtoBody
{
    public List<Import>? Imports { get; set; }
    public List<Package>? Packages { get; set; }
    public List<Option>? Options { get; set; }
    public List<Message>? Messages { get; set; }
    public List<Extend>? Extends { get; set; }
    public List<EnumBase>? Enums { get; set; }
    public List<Service>? Services { get; set; }
    public List<EmptyStatement>? EmptyStatements { get; set; }
}

/// <summary>
/// Proto represents a protocol buffer definition.
/// </summary>
public class Proto
{
    public required Syntax Syntax { get; set; }
    // ProtoBody is a slice of sum type consisted of *Import, *Package, *Option, *Message, *Enum, *Service, *Extend and *EmptyStatement.
    public required ProtoBody ProtoBody { get; set; }
}