using System.Text.Json;
using YYHEggEgg.Logger;
using YYHEggEgg.ProtoParser;

// See https://aka.ms/new-console-template for more information
Log.Initialize(new LoggerConfig
{
    Max_Output_Char_Count = -1,
});

Log.Info("Embedded Resources List:");
var assembly = typeof(ExecutableInvoke).Assembly;
foreach (var res in assembly.GetManifestResourceNames())
    Log.Info($"  {res}");
Log.Info("");

Log.Info($"Made proto2json available at: '{ExecutableInvoke.GetProto2jsonPath()}'");

Log.Info("Please give the directory path to .proto files:");
var protoPath = Console.ReadLine() ?? throw new ArgumentNullException();

Log.Info("Now directory test...");
await ProtoParser.ParseFromDirectoryAsync(protoPath, "logs");

var dictFrmDir = await ProtoParser.ParseFromDirectoryAsync(protoPath);
foreach (var pair in dictFrmDir)
{
    Log.Info($"Processed file: '{pair.Key}', output: \n{JsonSerializer.Serialize(pair.Value)}");
}
Log.Info("");

Log.Info("Now files test...");
var fileList = Directory.EnumerateFiles(protoPath, $"*.proto", SearchOption.AllDirectories);
var dictFrmFiles = await ProtoParser.ParseFromFilesAsync(fileList);
foreach (var pair in dictFrmFiles)
{
    Log.Info($"Processed file: '{pair.Key}', output: \n{JsonSerializer.Serialize(pair.Value)}");
}
Log.Info("");

Log.Info("Now stdin test...");
var testfile = fileList.First();
var output = await ProtoParser.ParseFromTextAsync(File.ReadAllText(testfile));
Log.Info($"Selected file: '{testfile}', output: \n{JsonSerializer.Serialize(output)}");
Log.Info("");

Log.Info($"Congratulations! Finished.");