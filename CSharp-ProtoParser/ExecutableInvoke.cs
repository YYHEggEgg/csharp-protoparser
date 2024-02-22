using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Grpc.Core.Internal;
using YYHEggEgg.ProtoParser.Aot;
using YYHEggEgg.ProtoParser.RawData;

namespace YYHEggEgg.ProtoParser;

public static class ExecutableInvoke
{
    public static string GetProto2jsonPath()
    {
        string path = "build/";
        var oskind = CommonPlatformDetection.GetOSKind();
        if (oskind == CommonPlatformDetection.OSKind.Unknown)
            throw new PlatformNotSupportedException("Your OS platform is not supported by this program.");
        path += $"{oskind.ToString().ToLower()}_";
        var processArchitecture = CommonPlatformDetection.GetProcessArchitecture();
        if (processArchitecture == CommonPlatformDetection.CpuArchitecture.Unknown)
            throw new PlatformNotSupportedException("Your CPU Architecture is not supported by this program.");
        path += processArchitecture.ToString().ToLower();
        path += "/go-proto2json";
        if (oskind == CommonPlatformDetection.OSKind.Windows)
            path += ".exe";
        return path;
    }

    private static async Task<Process> StartProto2jsonCoreAsync(string argumentList, string? writeToStdin)
    {
        ProcessStartInfo startInfo = new(GetProto2jsonPath())
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
        if (outputPath != null) argumentList += $"--fout \"{Path.GetFullPath(outputPath)}\"";
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