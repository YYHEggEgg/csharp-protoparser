namespace YYHEggEgg.ProtoParser.RawData;

/// <summary>
/// EnumValueOption is an option of a enumField.
/// </summary>
public class EnumValueOption
{
#if NET7_0_OR_GREATER
    public required string OptionName { get; set; }
#else
    public string OptionName { get; set; } = null!;
#endif
#if NET7_0_OR_GREATER
    public required string Constant { get; set; }
#else
    public string Constant { get; set; } = null!;
#endif
}

/// <summary>
/// EnumField is a field of enum.
/// </summary>
public class EnumField : WithCommentsBase
{
#if NET7_0_OR_GREATER
    public required string Ident { get; set; }
#else
    public string Ident { get; set; } = null!;
#endif
#if NET7_0_OR_GREATER
    public required string Number { get; set; }
#else
    public string Number { get; set; } = null!;
#endif
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
#if NET7_0_OR_GREATER
    public required string EnumName { get; set; }
#else
    public string EnumName { get; set; } = null!;
#endif
    /// <summary>
    /// EnumBody can have options and enum fields.
	/// The element of this is the union of an option, enumField, reserved, and emptyStatement.
    /// </summary>
#if NET7_0_OR_GREATER
    public required EnumBody EnumBody { get; set; }
#else
    public EnumBody EnumBody { get; set; } = null!;
#endif
}