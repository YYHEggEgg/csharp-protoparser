using YYHEggEgg.ProtoParser.RawData.RawMeta;

namespace YYHEggEgg.ProtoParser.RawData;

public abstract class WithMetaBase
{
    /// <summary>
    /// Meta is the meta information.
    /// </summary>
    public required Meta Meta { get; set; }
}