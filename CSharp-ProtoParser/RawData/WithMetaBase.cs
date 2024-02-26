using YYHEggEgg.ProtoParser.RawData.RawMeta;

namespace YYHEggEgg.ProtoParser.RawData;

public abstract class WithMetaBase
{
    /// <summary>
    /// Meta is the meta information.
    /// </summary>
#if NET7_0_OR_GREATER
    public required Meta Meta { get; set; }
#else
    public Meta Meta { get; set; } = null!;
#endif
}