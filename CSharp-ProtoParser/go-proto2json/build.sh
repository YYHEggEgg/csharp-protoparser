#!/bin/bash

output_windows_x86="build/Windows_X86/go-proto2json.exe"
output_windows_x64="build/Windows_X64/go-proto2json.exe"
output_windows_arm64="build/Windows_Arm64/go-proto2json.exe"
output_macosx_x86="build/MacOSX_X86/go-proto2json"
output_macosx_x64="build/MacOSX_X64/go-proto2json"
output_macosx_arm64="build/MacOSX_Arm64/go-proto2json"
output_linux_x86="build/Linux_X86/go-proto2json"
output_linux_x64="build/Linux_X64/go-proto2json"
output_linux_arm64="build/Linux_Arm64/go-proto2json"

# Compile for Windows x86
go env -w GOOS=windows GOARCH=386
go build -o $output_windows_x86 ./proto2json

# Calculate SHA256 for Windows x86
sha256_windows_x86=$(shasum -a 256 $output_windows_x86 | awk '{print $1}')

# Compile for Windows x64
go env -w GOOS=windows GOARCH=amd64
go build -o $output_windows_x64 ./proto2json

# Calculate SHA256 for Windows x64
sha256_windows_x64=$(shasum -a 256 $output_windows_x64 | awk '{print $1}')

# Compile for Windows arm64
go env -w GOOS=windows GOARCH=arm64
go build -o $output_windows_arm64 ./proto2json

# Calculate SHA256 for Windows arm64
sha256_windows_arm64=$(shasum -a 256 $output_windows_arm64 | awk '{print $1}')

# Compile for MacOSX x86
go env -w GOOS=darwin GOARCH=386
go build -o $output_macosx_x86 ./proto2json

# Calculate SHA256 for MacOSX x86
sha256_macosx_x86=$(shasum -a 256 $output_macosx_x86 | awk '{print $1}')

# Compile for MacOSX x64
go env -w GOOS=darwin GOARCH=amd64
go build -o $output_macosx_x64 ./proto2json

# Calculate SHA256 for MacOSX x64
sha256_macosx_x64=$(shasum -a 256 $output_macosx_x64 | awk '{print $1}')

# Compile for MacOSX arm64
go env -w GOOS=darwin GOARCH=arm64
go build -o $output_macosx_arm64 ./proto2json

# Calculate SHA256 for MacOSX arm64
sha256_macosx_arm64=$(shasum -a 256 $output_macosx_arm64 | awk '{print $1}')

# Compile for Linux x86
go env -w GOOS=linux GOARCH=386
go build -o $output_linux_x86 ./proto2json

# Calculate SHA256 for Linux x86
sha256_linux_x86=$(shasum -a 256 $output_linux_x86 | awk '{print $1}')

# Compile for Linux x64
go env -w GOOS=linux GOARCH=amd64
go build -o $output_linux_x64 ./proto2json

# Calculate SHA256 for Linux x64
sha256_linux_x64=$(shasum -a 256 $output_linux_x64 | awk '{print $1}')

# Compile for Linux arm64
go env -w GOOS=linux GOARCH=arm64
go build -o $output_linux_arm64 ./proto2json

# Calculate SHA256 for Linux arm64
sha256_linux_arm64=$(shasum -a 256 $output_linux_arm64 | awk '{print $1}')

# Update Info.cs file with SHA256 values
cat <<EOT > ../ExecutableInvoke.Checksum.Output.cs
namespace YYHEggEgg.ProtoParser;

public static partial class ExecutableInvoke
{
    private static readonly string SHA256_WINDOWS_X86 = "$sha256_windows_x86".ToUpperInvariant();
    private static readonly string SHA256_WINDOWS_X64 = "$sha256_windows_x64".ToUpperInvariant();
    private static readonly string SHA256_WINDOWS_ARM64 = "$sha256_windows_arm64".ToUpperInvariant();
    private static readonly string SHA256_MACOSX_X86 = "$sha256_macosx_x86".ToUpperInvariant();
    private static readonly string SHA256_MACOSX_X64 = "$sha256_macosx_x64".ToUpperInvariant();
    private static readonly string SHA256_MACOSX_ARM64 = "$sha256_macosx_arm64".ToUpperInvariant();
    private static readonly string SHA256_LINUX_X86 = "$sha256_linux_x86".ToUpperInvariant();
    private static readonly string SHA256_LINUX_X64 = "$sha256_linux_x64".ToUpperInvariant();
    private static readonly string SHA256_LINUX_ARM64 = "$sha256_linux_arm64".ToUpperInvariant();
}
EOT
