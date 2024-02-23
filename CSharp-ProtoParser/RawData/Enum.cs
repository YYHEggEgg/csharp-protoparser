namespace YYHEggEgg.ProtoParser.RawData;

/// <summary>
/// EnumValueOption is an option of a enumField.
/// </summary>
public class EnumValueOption
{
    public required string OptionName { get; set; }
    public required string Constant { get; set; }
}

/// <summary>
/// EnumField is a field of enum.
/// </summary>
public class EnumField : WithCommentsBase
{
    public required string Ident { get; set; }
    public required string Number { get; set; }
    public List<EnumValueOption>? EnumValueOptions { get; set; }
}

public class EnumBody
{
    public List<Option>? Options { get; set; }
    public List<EnumField>? EnumFields { get; set; }
    public List<Reserved>? Reserveds { get; set; }
    public List<EmptyStatement>? EmptyStatements { get; set; }
}

/// <summary>
/// EnumBase consists of a name and an enum body.
/// </summary>
public class EnumBase : WithInlineCommentWithLeftCurlyBase
{
    public required string EnumName { get; set; }
    /// <summary>
    /// EnumBody can have options and enum fields.
	/// The element of this is the union of an option, enumField, reserved, and emptyStatement.
    /// </summary>
    public required EnumBody EnumBody { get; set; }
}