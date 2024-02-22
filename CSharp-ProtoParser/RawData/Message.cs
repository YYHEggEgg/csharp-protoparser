namespace YYHEggEgg.ProtoParser.RawData;

public class MessageBody
{
    public required List<Field> Fields { get; set; }
    public required List<Enum> Enums { get; set; }
    public required List<Message> Messages { get; set; }
    public required List<Option> Options { get; set; }
    public required List<Oneof> Oneofs { get; set; }
    public required List<MapField> Maps { get; set; }
    public required List<GroupField> Groups { get; set; }
    public required List<Reserved> Reserves { get; set; }
    public required List<Extend> Extends { get; set; }
    public required List<EmptyStatement> EmptyStatements { get; set; }
    public required List<Extensions> Extensions { get; set; }
}

/// <summary>
/// Message consists of a message name and a message body.
/// </summary>
public class Message : WithInlineCommentWithLeftCurlyBase
{
    public required string MessageName { get; set; }
    /// <summary>
    /// MessageBody can have fields, nested enum definitions, nested message definitions,
	/// options, oneofs, map fields, group fields(proto2 only), extends, reserved, and extensions(proto2 only) statements.
    /// </summary>
    public required MessageBody MessageBody { get; set; }
}