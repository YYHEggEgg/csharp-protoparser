using System.Security.Cryptography;
using Grpc.Core.Internal;

namespace YYHEggEgg.ProtoParser;

public static partial class ExecutableInvoke
{
    private static bool ValidateExecutable(string filePath)
    {
        if (!File.Exists(filePath)) return false;

        using var sha256 = SHA256.Create();
        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        byte[] hash = sha256.ComputeHash(fileStream);
        var sha256Value = BitConverter.ToString(hash).Replace("-", "").ToUpperInvariant();
        return sha256Value == GetProto2jsonSHA256();
    }

    /// <summary>
    /// Get the SHA256 hash of the packed executable.
    /// </summary>
    /// <returns>The capitalized SHA256 of the file.</returns>
    public static string GetProto2jsonSHA256() =>
        GetProto2jsonSHA256(CommonPlatformDetection.GetOSKind(), CommonPlatformDetection.GetProcessArchitecture());

    private static string GetProto2jsonSHA256(CommonPlatformDetection.OSKind oskind, CommonPlatformDetection.CpuArchitecture cpuArchitecture)
    {
        return oskind switch
        {
            CommonPlatformDetection.OSKind.Windows => cpuArchitecture switch
            {
                CommonPlatformDetection.CpuArchitecture.X86 => SHA256_WINDOWS_X86,
                CommonPlatformDetection.CpuArchitecture.X64 => SHA256_WINDOWS_X64,
                CommonPlatformDetection.CpuArchitecture.Arm64 => SHA256_WINDOWS_ARM64,
                _ => ThrowOnUnsupportedCpuArchitecture(),
            },
            CommonPlatformDetection.OSKind.MacOSX => cpuArchitecture switch
            {
                CommonPlatformDetection.CpuArchitecture.X86 => SHA256_MACOSX_X86,
                CommonPlatformDetection.CpuArchitecture.X64 => SHA256_MACOSX_X64,
                CommonPlatformDetection.CpuArchitecture.Arm64 => SHA256_MACOSX_ARM64,
                _ => ThrowOnUnsupportedCpuArchitecture(),
            },
            CommonPlatformDetection.OSKind.Linux => cpuArchitecture switch
            {
                CommonPlatformDetection.CpuArchitecture.X86 => SHA256_LINUX_X86,
                CommonPlatformDetection.CpuArchitecture.X64 => SHA256_LINUX_X64,
                CommonPlatformDetection.CpuArchitecture.Arm64 => SHA256_LINUX_ARM64,
                _ => ThrowOnUnsupportedCpuArchitecture(),
            },
            _ => ThrowOnUnsupportedOSKind(),
        };
    }
}