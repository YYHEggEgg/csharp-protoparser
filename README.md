# csharp-protoparser

csharp-protoparser is a .proto file parser compatiable with go-protoparser.

[![NuGet](https://img.shields.io/nuget/v/EggEgg.CSharp-ProtoParser.svg)](https://www.nuget.org/packages/EggEgg.CSharp-ProtoParser)

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
var dirParsed = await ProtoParser.ParseFromDirectoryAsync(dir);

// You may want to output content to directory, not memory:
await ProtoParser.ParseFromDirectoryAsync(dir, "Proto2json_Output");
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
