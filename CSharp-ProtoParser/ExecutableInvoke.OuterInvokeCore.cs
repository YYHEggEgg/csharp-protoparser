using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Grpc.Core.Internal;

namespace YYHEggEgg.ProtoParser;

public static partial class ExecutableInvoke
{

    private const string TempPathDir = "EggEgg.CSharp-ProtoParser";
    private const string MainfestPrefix = "YYHEggEgg.ProtoParser.go_proto2json.build.";
    private static string GetProto2jsonExecutableName(CommonPlatformDetection.OSKind oskind = default)
    {
        if (oskind == default) oskind = CommonPlatformDetection.GetOSKind();
        return oskind == CommonPlatformDetection.OSKind.Windows ? "go-proto2json.exe" : "go-proto2json";
    }

    [DoesNotReturn]
    private static string ThrowOnUnsupportedOSKind() =>
        throw new PlatformNotSupportedException("Your OS platform is not supported by this program.");
    [DoesNotReturn]
    private static string ThrowOnUnsupportedCpuArchitecture() =>
        throw new PlatformNotSupportedException("Your CPU Architecture is not supported by this program.");

    private static string GetProto2jsonMainfestName()
    {
        var oskind = CommonPlatformDetection.GetOSKind();
        if (oskind == CommonPlatformDetection.OSKind.Unknown)
            ThrowOnUnsupportedOSKind();
        var processArchitecture = CommonPlatformDetection.GetProcessArchitecture();
        if (processArchitecture == CommonPlatformDetection.CpuArchitecture.Unknown)
            ThrowOnUnsupportedCpuArchitecture();
        return $"{MainfestPrefix}{oskind}_{processArchitecture}.{GetProto2jsonExecutableName(oskind)}";
    }

    private static object initProto2jsonLock = "nuget executable pain";

    /// <summary>
    /// Make proto2json executable available at a path and return it.
    /// </summary>
    /// <returns>The proto2json executable path.</returns>
    public static string GetProto2jsonPath()
    {
        var tmpdir = Path.GetTempPath();
        var dir = Path.Combine(tmpdir, "EggEgg.CSharp-ProtoParser");
        Directory.CreateDirectory(dir);
        var executable = Path.Combine(dir, GetProto2jsonExecutableName());
        if (ValidateExecutable(executable)) return executable;

        lock (initProto2jsonLock)
        {
            if (ValidateExecutable(executable)) return executable;

            using var mainfestStream = typeof(ExecutableInvoke).Assembly.GetManifestResourceStream(GetProto2jsonMainfestName())
                ?? throw new ApplicationException($"Failed to load proto2json executable from the embedded resources. Mainfest: {GetProto2jsonMainfestName()}");

            try
            {
                using var fileStream = new FileStream(executable, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                mainfestStream.CopyTo(fileStream);
            }
            catch (Exception ex)
            {
                throw new FileLoadException("Failed to load proto2json executable to the temp folder.", ex);
            }

            // Perform chmod +x
            if (CommonPlatformDetection.GetOSKind() != CommonPlatformDetection.OSKind.Windows)
            {
                var chmodproc = Process.Start("chmod", $"+x \"{executable}\"");
                chmodproc.WaitForExit();
                if (chmodproc.ExitCode != 0)
                    throw new FileLoadException($"Performing chmod +x to proto2json executable exited with {chmodproc.ExitCode}.");
            }
        }

        return executable;
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
            await proc.StandardInput.WriteAsync(writeToStdin.ReplaceLineEndings());
            proc.StandardInput.Dispose();
        }
        return proc;
    }

}