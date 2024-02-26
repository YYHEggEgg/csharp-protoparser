namespace YYHEggEgg.ProtoParser.RawData;

/// <summary>
/// RPCRequest is a request of RPC.
/// </summary>
public class RPCRequest : WithMetaBase
{
    public bool IsStream { get; set; }
#if NET7_0_OR_GREATER
    public required string MessageType { get; set; }
#else
    public string MessageType { get; set; } = null!;
#endif
}

/// <summary>
/// RPCResponse is a response of RPC.
/// </summary>
public class RPCResponse : WithMetaBase
{
    public bool IsStream { get; set; }
#if NET7_0_OR_GREATER
    public required string MessageType { get; set; }
#else
    public string MessageType { get; set; } = null!;
#endif
}

/// <summary>
/// RPC is a Remote Procedure Call.
/// </summary>
public class RPC : WithInlineCommentWithLeftCurlyBase
{
#if NET7_0_OR_GREATER
    public required string RPCName { get; set; }
#else
    public string RPCName { get; set; } = null!;
#endif
#if NET7_0_OR_GREATER
    public required RPCRequest RPCRequest { get; set; }
#else
    public RPCRequest RPCRequest { get; set; } = null!;
#endif
#if NET7_0_OR_GREATER
    public required RPCResponse RPCResponse { get; set; }
#else
    public RPCResponse RPCResponse { get; set; } = null!;
#endif
    public List<Option>? Options { get; set; }
}

public class ServiceBody
{
    public List<Option>? Options { get; set; }
    public List<RPC>? RPCs { get; set; }
}

/// <summary>
/// Service consists of RPCs.
/// </summary>
public class Service : WithInlineCommentWithLeftCurlyBase
{
#if NET7_0_OR_GREATER
    public required string ServiceName { get; set; }
#else
    public string ServiceName { get; set; } = null!;
#endif
#if NET7_0_OR_GREATER
    public required ServiceBody ServiceBody { get; set; }
#else
    public ServiceBody ServiceBody { get; set; } = null!;
#endif
}

