# csharp-protoparser

csharp-protoparser is a .proto file parser compatiable with go-protoparser.

[![NuGet](https://img.shields.io/nuget/v/EggEgg.CSharp-ProtoParser.svg)](https://www.nuget.org/packages/EggEgg.CSharp-ProtoParser)

## Changelog

### v1.1.0

- Fixed the issue whereby `arm64` CPU arch devices are accidentally recognized as `x64` CPU arch.
- Fixed the issue whereby using multiple versions of this nuget on the same device may conflict with each other.
- Now `ProtoParser.ParseFromDirectoryAsync` support an optional parameter `dirFilter` so as you can filter files with a certain pattern.
- Added representation of `import`, `package` and `option` in `ProtoResult`. You can refer to `ProtoResult` definition for more information. **Notice the `""` in `import` definitions will be wiped out (while in `Parse...AsRaw` it will not).**

#### Breaking Changes

- Due to overloads' probable conflict, these methods of `ProtoParser` are renamed:
  - `ParseFromDirectoryAsync(string dirPath, string outputPath)` -> `ParseFromDirectoryAndOutputAsync(string dirPath, string outputPath, string dirFilter = "*.proto")` and its syncronous version.
  - `ParseFromFilesAsync(IEnumerable<string> fileList, string outputPath)` -> `ParseFromFilesAndOutputAsync(IEnumerable<string> fileList, string outputPath, string dirFilter = "*.proto")` and its syncronous version.

## Usage

Simple:

```cs
using YYHEggEgg.ProtoParser;
using YYHEggEgg.ProtoParser.RawProtoHandler;

string protoText = @"<Fill in with a .proto file content>";
// Very simple parse.
ProtoJsonResult parseres = await ProtoParser.ParseFromTextAsync(protoText);

var files = new string[] { "file1.proto", "file2.proto" };
var filesParsed = await ProtoParser.ParseFromFilesAsync(files);
// The key of dictionary is the FULL PATH of the provided file(s)
// The value of dictionary is ProtoJsonResult
Console.WriteLine(JsonSerializer.Serialize(filesParsed["D:\\test\\protos\\file1.proto"]));

// Or you can let the input be a directory's all .proto files:
var dir = "test/protos";
// Returns a similar dictionary as below.
var dirParsed = await ProtoParser.ParseFromDirectoryAsync(dir, "*.proto");

// You may want to output content to directory, not memory:
await ProtoParser.ParseFromDirectoryAndOutputAsync(dir, "Proto2json_Output");
// But you need to process them yourself.
var dirParsedOne = ProtoJsonRawDataAnalyzer.AnalyzeRawProto(
    JsonSerializer.Deserialize<Proto>(File.ReadAllText("D:\\Proto2json_Output\\file1.proto.json"))
    // If needed, add param: 'ProtoContext.Default.Proto' to Deserialize to make it Aot compatiable (.NET 8.0+ only)
);

// Or you may dislike ProtoJsonResult; it's OK to use the original data structure from go-protoparser.
// Just add 'AsRaw' to methods' name.
Proto unorderedProto = await ProtoParser.ParseFromTextAsRawAsync(protoText);

// There're syncronous methods; but it's recommended to use asyncronous ones.
Proto unorderedProto2 = ProtoParser.ParseFromTextAsRaw(protoText);
```

## Principle

The package simply packs built `go-proto2json` inside, and extract the program from the embedded resources to a temp path when needed.

The package will check whether the program's SHA256 is correct before every run, so don't need to worry potential errors. By the way, invoke less times of methods (e.g. using `ParseFromFilesAsync` with a file list instead of individual `ParseFromTextAsync` calls) can make the performance better!
