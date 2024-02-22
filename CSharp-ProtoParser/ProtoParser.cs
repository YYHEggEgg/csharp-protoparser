using YYHEggEgg.ProtoParser.RawData;

namespace YYHEggEgg.ProtoParser;

public static class ProtoParser
{
    /// <summary>
    /// Parse a .proto file text, and return the result.
    /// </summary>
    /// <param name="protoText">The content of the .proto file.</param>
    /// <returns>The proto2json result.</returns>
    public static async Task<Proto?> ParseFromTextAsRawAsync(string protoText) =>
        await ExecutableInvoke.StartProcessStdinAsync(protoText);

    /// <summary>
    /// Parse .proto files under a directory (recursive), and return the result list.
    /// </summary>
    /// <param name="dirPath">The directory you want to process.</param>
    /// <returns>The dictionary of found file and its proto2json result.</returns>
    public static async Task<Dictionary<string, Proto?>> ParseFromDirectoryAsRawAsync(string dirPath) =>
        await ExecutableInvoke.StartProcessDirAsync(dirPath, null);

    /// <summary>
    /// Parse .proto files under a directory (recursive), and make files available at <paramref name="outputPath"/>.
    /// </summary>
    /// <param name="dirPath">The directory you want to process.</param>
    /// <param name="outputPath">The path you want to store [filename].proto.json files.</param>
    public static async Task ParseFromDirectoryAsRawAsync(string dirPath, string outputPath) =>
        await ExecutableInvoke.StartProcessDirAsync(dirPath, outputPath);

    /// <summary>
    /// Parse specified .proto files, and return the result dictionary.
    /// </summary>
    /// <param name="fileList">The files you want to process.</param>
    /// <returns>The dictionary of specified file and its proto2json result.</returns>
    public static async Task<Dictionary<string, Proto?>> ParseFromFilesAsRawAsync(IEnumerable<string> fileList) =>
        await ExecutableInvoke.StartProcessFilesAsync(fileList, null);

    /// <summary>
    /// Parse specified .proto files, and make files available at <paramref name="outputPath"/>.
    /// </summary>
    /// <param name="fileList">The files you want to process.</param>
    /// <param name="outputPath">The path you want to store [filename].proto.json files.</param>
    public static async Task ParseFromFilesAsRawAsync(IEnumerable<string> fileList, string outputPath) =>
        await ExecutableInvoke.StartProcessFilesAsync(fileList, outputPath);

    /// <summary>
    /// Parse a .proto file text, and return the result.
    /// </summary>
    /// <param name="protoText">The content of the .proto file.</param>
    /// <returns>The proto2json result.</returns>
    public static Proto? ParseFromTextAsRaw(string protoText) => ParseFromTextAsRawAsync(protoText).Result;

    /// <summary>
    /// Parse .proto files under a directory (recursive), and return the result list.
    /// </summary>
    /// <param name="dirPath">The directory you want to process.</param>
    /// <returns>The dictionary of found file and its proto2json result.</returns>
    public static Dictionary<string, Proto?> ParseFromDirectoryAsRaw(string dirPath) => ParseFromDirectoryAsRawAsync(dirPath).Result;

    /// <summary>
    /// Parse .proto files under a directory (recursive), and make files available at <paramref name="outputPath"/>.
    /// </summary>
    /// <param name="dirPath">The directory you want to process.</param>
    /// <param name="outputPath">The path you want to store [filename].proto.json files.</param>
    public static void ParseFromDirectoryAsRaw(string dirPath, string outputPath) => ParseFromDirectoryAsRawAsync(dirPath, outputPath).Wait();

    /// <summary>
    /// Parse specified .proto files, and return the result dictionary.
    /// </summary>
    /// <param name="fileList">The files you want to process.</param>
    /// <returns>The dictionary of specified file and its proto2json result.</returns>
    public static Dictionary<string, Proto?> ParseFromFilesAsRaw(IEnumerable<string> fileList) => ParseFromFilesAsRawAsync(fileList).Result;

    /// <summary>
    /// Parse specified .proto files, and make files available at <paramref name="outputPath"/>.
    /// </summary>
    /// <param name="fileList">The files you want to process.</param>
    /// <param name="outputPath">The path you want to store [filename].proto.json files.</param>
    public static void ParseFromFilesAsRaw(IEnumerable<string> fileList, string outputPath) => ParseFromFilesAsRawAsync(fileList, outputPath).Wait();
}