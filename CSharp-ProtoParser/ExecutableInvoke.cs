using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Grpc.Core.Internal;
using YYHEggEgg.ProtoParser.Aot;
using YYHEggEgg.ProtoParser.RawData;

namespace YYHEggEgg.ProtoParser;

public static class ExecutableInvoke
{
    private const string TempPathDir = "EggEgg.CSharp-ProtoParser";
    private const string MainfestPrefix = "YYHEggEgg.ProtoParser.go_proto2json.build.";
    private static string GetProto2jsonExecutableName(CommonPlatformDetection.OSKind oskind = default)
    {
        if (oskind == default) oskind = CommonPlatformDetection.GetOSKind();
        return oskind == CommonPlatformDetection.OSKind.Windows ? "go-proto2json.exe" : "go-proto2json";
    }
    private static string GetProto2jsonMainfestName()
    {
        string path = MainfestPrefix;
        var oskind = CommonPlatformDetection.GetOSKind();
        if (oskind == CommonPlatformDetection.OSKind.Unknown)
            throw new PlatformNotSupportedException("Your OS platform is not supported by this program.");
        path += $"{oskind.ToString().ToLower()}_";
        var processArchitecture = CommonPlatformDetection.GetProcessArchitecture();
        if (processArchitecture == CommonPlatformDetection.CpuArchitecture.Unknown)
            throw new PlatformNotSupportedException("Your CPU Architecture is not supported by this program.");
        path += processArchitecture.ToString().ToLower();
        path += $".{GetProto2jsonExecutableName(oskind)}";
        return path;
    }

    /// <summary>
    /// Make proto2json executable available at a path and return it.
    /// </summary>
    /// <returns>The proto2json executable path.</returns>
    public static async Task<string> GetProto2jsonPathAsync()
    {
        var tmpdir = Path.GetTempPath();
        var dir = Path.Combine(tmpdir, "EggEgg.CSharp-ProtoParser");
        Directory.CreateDirectory(dir);
        var executable = Path.Combine(dir, GetProto2jsonExecutableName());
        if (File.Exists(executable))
        {
            // TODO: Checksum validate
            return executable;
        }

        using var mainfestStream = typeof(ExecutableInvoke).Assembly.GetManifestResourceStream(GetProto2jsonMainfestName())
            ?? throw new ApplicationException("Failed to load proto2json executable from the embedded resources.");

        try
        {
            using var fileStream = new FileStream(executable, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            await mainfestStream.CopyToAsync(fileStream);
        }
        catch (Exception ex)
        {
            throw new FileLoadException("Failed to load proto2json executable to the temp folder.", ex);
        }

        // Perform chmod +x
        if (CommonPlatformDetection.GetOSKind() != CommonPlatformDetection.OSKind.Windows)
        {
            var chmodproc = Process.Start("chmod", $"+x \"{executable}\"");
            await chmodproc.WaitForExitAsync();
            if (chmodproc.ExitCode != 0)
                throw new FileLoadException($"Performing chmod +x to proto2json executable exited with {chmodproc.ExitCode}.");
        }

        return executable;
    }

    private static async Task<Process> StartProto2jsonCoreAsync(string argumentList, string? writeToStdin)
    {
        ProcessStartInfo startInfo = new(await GetProto2jsonPathAsync())
        {
            Arguments = argumentList,
            UseShellExecute = false,
            RedirectStandardInput = writeToStdin != null,
            RedirectStandardOutput = true,
            RedirectStandardError = false,
        };
        var proc = Process.Start(startInfo) ?? throw new ApplicationException("The proto2json process failed to launch.");
        if (writeToStdin != null)
        {
            await proc.StandardInput.WriteAsync(writeToStdin);
            proc.StandardInput.Close();
        }
        return proc;
    }

    private static void AssertValidOutputPath(string? outputPath)
    {
        if (outputPath == null) return;
        Directory.CreateDirectory(outputPath);
        if (outputPath == "__<>_stdout")
            throw new ArgumentException("Using a program reserved name.", nameof(outputPath));
    }

    private static async Task<Dictionary<string, Proto?>> ParseProcessStdoutAsync(Process proc)
    {
        Debug.Assert(proc.StartInfo.RedirectStandardOutput);
        Dictionary<string, Proto?> result = new Dictionary<string, Proto?>();
        while (!proc.StandardOutput.EndOfStream)
        {
            var line = await proc.StandardOutput.ReadLineAsync();
            if (string.IsNullOrEmpty(line)) continue;
            var separatorIndex = line.IndexOf("=>");
            if (separatorIndex < 0) continue;

#if NET8_0_OR_GREATER
            result.Add(line[..separatorIndex], JsonSerializer.Deserialize<Proto>(line[(separatorIndex + "=>".Length)..], ProtoContext.Default.Proto));
#else
            result.Add(line[..separatorIndex], JsonSerializer.Deserialize<Proto>(line[(separatorIndex + "=>".Length)..]));
#endif
        }
        await proc.WaitForExitAsync();
        if (proc.ExitCode != 0)
        {
            throw new ApplicationException($"The go-proto2json process exited with code {proc.ExitCode}.");
        }
        return result;
    }

    internal static async Task<Dictionary<string, Proto?>> StartProcessDirAsync(string dirPath, string? outputPath)
    {
        AssertValidOutputPath(outputPath);

        var argumentList = $"--dir \"{Path.GetFullPath(dirPath)}\"";
        if (outputPath != null) argumentList += $" --fout \"{Path.GetFullPath(outputPath)}\"";
        var proc = await StartProto2jsonCoreAsync(argumentList, null);
        return await ParseProcessStdoutAsync(proc);
    }

    internal static async Task<Proto?> StartProcessStdinAsync(string protoText)
    {
        var argumentList = $"--stdin";
        var proc = await StartProto2jsonCoreAsync(argumentList, protoText);
        await proc.WaitForExitAsync();
        if (proc.ExitCode != 0)
        {
            throw new ApplicationException($"The go-proto2json process exited with code {proc.ExitCode}.");
        }
        var resText = await proc.StandardOutput.ReadToEndAsync();
#if NET8_0_OR_GREATER
        return JsonSerializer.Deserialize<Proto>(resText, ProtoContext.Default.Proto);
#else
        return JsonSerializer.Deserialize<Proto>(resText);
#endif
    }

    /// <summary>
    /// The maximum length of creating a process's command line
    /// string. The minimum value ever searched is Windows
    /// cmd.exe's 8191, but here use 7 * 1024.
    /// </summary>
    public const int MAXIMUM_CREATEPROC_LENGTH = 7 * 1024;

    internal static async Task<Dictionary<string, Proto?>> StartProcessFilesAsync(IEnumerable<string> files, string? outputPath)
    {
        AssertValidOutputPath(outputPath);

        StringBuilder argumentList = new StringBuilder();
        string appendOutput = outputPath == null ? string.Empty : $"--fout \"{Path.GetFullPath(outputPath)}\"";
        // TODO: Verify no duplicate file & no duplicate file name (when with output dir).
        List<Process> procs = new();
        foreach (var file in files)
        {
            argumentList.Append($"--file \"{Path.GetFullPath(file)}\" ");
            if (argumentList.Length + appendOutput.Length >= MAXIMUM_CREATEPROC_LENGTH)
            {
                argumentList.Append(appendOutput);
            }
            procs.Add(await StartProto2jsonCoreAsync(argumentList.ToString(), null));
            argumentList = new StringBuilder();
        }

        Dictionary<string, Proto?> result = new Dictionary<string, Proto?>();
        foreach (var proc in procs)
        {
            var dictionary = await ParseProcessStdoutAsync(proc);
            if (dictionary == null || dictionary.Count == 0) continue;

            foreach (var pair in dictionary) result.Add(pair.Key, pair.Value);
        }
        return result;
    }
}