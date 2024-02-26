using YYHEggEgg.ProtoParser.RawProtoHandler;

namespace YYHEggEgg.ProtoParser;

public static partial class ProtoParser
{
    /// <summary>
    /// Parse a .proto file text, and return the result.
    /// </summary>
    /// <param name="protoText">The content of the .proto file.</param>
    /// <returns>The proto2json result.</returns>
    public static async Task<ProtoJsonResult> ParseFromTextAsync(string protoText) =>
        ProtoJsonRawDataAnalyzer.AnalyzeRawProto(await ParseFromTextAsRawAsync(protoText));

    /// <summary>
    /// Parse .proto files under a directory (recursive), and return the result list.
    /// </summary>
    /// <param name="dirPath">The directory you want to process.</param>
    /// <returns>The dictionary of found file (Full Path) and its proto2json result.</returns>
    public static async Task<Dictionary<string, ProtoJsonResult>> ParseFromDirectoryAsync(string dirPath) =>
        new Dictionary<string, ProtoJsonResult>((await ParseFromDirectoryAsRawAsync(dirPath)).Select(
            x => new KeyValuePair<string, ProtoJsonResult>(x.Key, ProtoJsonRawDataAnalyzer.AnalyzeRawProto(x.Value))));

    /// <summary>
    /// Parse specified .proto files, and return the result dictionary.
    /// </summary>
    /// <param name="fileList">The files you want to process.</param>
    /// <returns>The dictionary of specified file (Full Path) and its proto2json result.</returns>
    public static async Task<Dictionary<string, ProtoJsonResult>> ParseFromFilesAsync(IEnumerable<string> fileList) =>
        new Dictionary<string, ProtoJsonResult>((await ParseFromFilesAsRawAsync(fileList)).Select(
            x => new KeyValuePair<string, ProtoJsonResult>(x.Key, ProtoJsonRawDataAnalyzer.AnalyzeRawProto(x.Value))));

    /// <summary>
    /// Parse a .proto file text, and return the result.
    /// </summary>
    /// <param name="protoText">The content of the .proto file.</param>
    /// <returns>The proto2json result.</returns>
    public static ProtoJsonResult ParseFromText(string protoText) => ParseFromTextAsync(protoText).Result;

    /// <summary>
    /// Parse .proto files under a directory (recursive), and return the result list.
    /// </summary>
    /// <param name="dirPath">The directory you want to process.</param>
    /// <returns>The dictionary of found file (Full Path) and its proto2json result.</returns>
    public static Dictionary<string, ProtoJsonResult> ParseFromDirectory(string dirPath) => ParseFromDirectoryAsync(dirPath).Result;

    /// <summary>
    /// Parse specified .proto files, and return the result dictionary.
    /// </summary>
    /// <param name="fileList">The files you want to process.</param>
    /// <returns>The dictionary of specified file (Full Path) and its proto2json result.</returns>
    public static Dictionary<string, ProtoJsonResult> ParseFromFiles(IEnumerable<string> fileList) => ParseFromFilesAsync(fileList).Result;
}