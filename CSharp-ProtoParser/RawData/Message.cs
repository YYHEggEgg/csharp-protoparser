namespace YYHEggEgg.ProtoParser.RawData;

public class MessageBody
{
    public List<Field>? Fields { get; set; } = new();
    public List<EnumBase>? Enums { get; set; } = new();
    public List<Message>? Messages { get; set; } = new();
    public List<Option>? Options { get; set; } = new();
    public List<Oneof>? Oneofs { get; set; } = new();
    public List<MapField>? Maps { get; set; } = new();
    public List<GroupField>? Groups { get; set; } = new();
    public List<Reserved>? Reserves { get; set; } = new();
    public List<Extend>? Extends { get; set; } = new();
    public List<EmptyStatement>? EmptyStatements { get; set; } = new();
    public List<Extensions>? Extensions { get; set; } = new();
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