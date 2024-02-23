namespace YYHEggEgg.ProtoParser.RawData;

/// <summary>
/// RPCRequest is a request of RPC.
/// </summary>
public class RPCRequest : WithMetaBase
{
    public bool IsStream { get; set; }
    public required string MessageType { get; set; }
}

/// <summary>
/// RPCResponse is a response of RPC.
/// </summary>
public class RPCResponse : WithMetaBase
{
    public bool IsStream { get; set; }
    public required string MessageType { get; set; }
}

/// <summary>
/// RPC is a Remote Procedure Call.
/// </summary>
public class RPC : WithInlineCommentWithLeftCurlyBase
{
    public required string RPCName { get; set; }
    public required RPCRequest RPCRequest { get; set; }
    public required RPCResponse RPCResponse { get; set; }
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
    public required string ServiceName { get; set; }
    public required ServiceBody ServiceBody { get; set; }
}

