#!/bin/bash

# 定义变量
WINDOWS_32="build/windows_x86/go-proto2json.exe"
WINDOWS_64="build/windows_x64/go-proto2json.exe"
WINDOWS_ARM_64="build/windows_arm64/go-proto2json.exe"
MACOS_32="build/macosx_x86/go-proto2json"
MACOS_64="build/macosx_x64/go-proto2json"
MACOS_ARM_64="build/macosx_arm64/go-proto2json"
LINUX_32="build/linux_x86/go-proto2json"
LINUX_64="build/linux_x64/go-proto2json"
LINUX_ARM_64="build/linux_arm64/go-proto2json"

# build Windows 32-bit
echo "Building Windows 32-bit executable: $WINDOWS_32"
go env -w GOOS=windows GOARCH=386 
go build -o $WINDOWS_32 ./proto2json

# build Windows 64-bit
echo "Building Windows 64-bit executable: $WINDOWS_64"
go env -w GOOS=windows GOARCH=amd64 
go build -o $WINDOWS_64 ./proto2json

# build Windows ARM 64
echo "Building Windows ARM 64 executable: $WINDOWS_ARM_64"
go env -w GOOS=windows GOARCH=arm64 
go build -o $WINDOWS_ARM_64 ./proto2json

# build macOS 32-bit
echo "Building macOS 32-bit executable: $MACOS_32"
go env -w GOOS=darwin GOARCH=386 
go build -o $MACOS_32 ./proto2json

# build macOS 64-bit
echo "Building macOS 64-bit executable: $MACOS_64"
go env -w GOOS=darwin GOARCH=amd64 
go build -o $MACOS_64 ./proto2json

# build macOS ARM 64
echo "Building macOS ARM 64 executable: $MACOS_ARM_64"
go env -w GOOS=darwin GOARCH=arm64 
go build -o $MACOS_ARM_64 ./proto2json

# build Linux 32-bit
echo "Building Linux 32-bit executable: $LINUX_32"
go env -w GOOS=linux GOARCH=386 
go build -o $LINUX_32 ./proto2json

# build Linux 64-bit
echo "Building Linux 64-bit executable: $LINUX_64"
go env -w GOOS=linux GOARCH=amd64 
go build -o $LINUX_64 ./proto2json

# build Linux 64-bit
echo "Building Linux ARM 64 executable: $LINUX_ARM_64"
go env -w GOOS=linux GOARCH=arm64 
go build -o $LINUX_ARM_64 ./proto2json

echo "Done"
