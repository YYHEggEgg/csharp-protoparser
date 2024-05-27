using YYHEggEgg.ProtoParser.RawData;

namespace YYHEggEgg.ProtoParser.RawProtoHandler;

public static class ProtoJsonRawDataAnalyzer
{
    public static readonly string[] nativeTypes = new string[] { "double", "float", "int32", "int64", "uint32", "uint64", "sint32", "sint64", "fixed32", "fixed64", "sfixed32", "sfixed64", "bool", "string", "bytes" };
    public static ProtoJsonResult AnalyzeRawProto(Proto? proto)
    {
        if (proto == null) return new();

        var result = new ProtoJsonResult
        {
            Syntax = proto.Syntax.ProtobufVersion,
        };
        var protoBodies = proto.ProtoBody;

        if (protoBodies.Packages != null)
            foreach (var package in protoBodies.Packages)
                result.PackageDefinitions.Add(package.Name);
        if (protoBodies.Imports != null)
            foreach (var import in protoBodies.Imports)
            {
                var location = import.Location;
                if ((location.StartsWith('"') && location.EndsWith('"')) ||
                    (location.StartsWith('\'') && location.EndsWith('\'')))
                    location = location[1..^1];
                result.ImportedFiles.Add(location);
            }
        if (protoBodies.Options != null)
            foreach (var option in protoBodies.Options)
                result.Options.Add(new()
                {
                    OptionName = option.OptionName,
                    Constant = option.Constant,
                });

        if (protoBodies.Messages != null)
            foreach (var body in protoBodies.Messages)
            {
                var messageResult = Analyze_Message_DispatchWorker(body);
                if (messageResult != null) result.MessageBodys.Add(messageResult);
            }

        if (protoBodies.Enums != null)
            foreach (var eb in protoBodies.Enums)
            {
                var enumResult = Analyze_EnumWorker(eb);
                if (enumResult != null) result.EnumBodys.Add(enumResult);
            }

        return result;
    }

    #region Extracted methods
    #region Enum
    private static EnumResult Analyze_EnumWorker(EnumBase eb)
    {
        var enumResult = new EnumResult
        {
            EnumName = eb.EnumName
        };
        if (eb.EnumBody.EnumFields != null)
            foreach (var ei in eb.EnumBody.EnumFields)
            {
                enumResult.EnumNodes.Add((ei.Ident, int.Parse(ei.Number)));
            }
        if (eb.EnumBody.Options != null)
            foreach (var ei in eb.EnumBody.Options)
            {
                enumResult.EnumOptions.Add((ei.OptionName, ei.Constant));
            }
        return enumResult;
    }
    #endregion
    #region Common Field
    private static CommonResult Analyze_CommonFieldWorker(Field mb)
    {
        var commonResult = new CommonResult
        {
            FieldType = mb.Type,
            FieldName = mb.FieldName,
            FieldNumber = int.Parse(mb.FieldNumber),
            IsRepeatedField = mb.IsRepeated
        };

        commonResult.IsImportType = !nativeTypes.Contains(commonResult.FieldType);
        return commonResult;
    }
    #endregion
    #region Oneof Field
    private static OneofResult Analyze_OneofWorker(Oneof mb)
    {
        var oneofResult = new OneofResult
        {
            OneofEntryName = mb.OneofName,
            OneofInnerFields = new List<CommonResult>()
        };

        if (mb.OneofFields != null)
            foreach (var ob in mb.OneofFields)
            {
                var innerCommonResult = Analyze_InOneof_CommonFieldWorker(ob);
                if (innerCommonResult != null)
                    oneofResult.OneofInnerFields.Add(innerCommonResult);
            }
        return oneofResult;
    }
    #region Common Field in Oneof
    private static CommonResult? Analyze_InOneof_CommonFieldWorker(OneofField mb)
    {
        var commonResult = new CommonResult
        {
            FieldType = mb.Type,
            FieldName = mb.FieldName,
            FieldNumber = int.Parse(mb.FieldNumber)
        };

        commonResult.IsImportType = !nativeTypes.Contains(commonResult.FieldType);
        return commonResult;
    }
    #endregion
    #endregion
    #region MapField
    private static MapResult Analyze_MapWorker(MapField mb)
    {
        var mapResult = new MapResult
        {
            KeyType = mb.KeyType,
            ValueType = mb.Type,
            FieldName = mb.MapName,
            FieldNumber = int.Parse(mb.FieldNumber)
        };

        mapResult.KeyIsImportType = !nativeTypes.Contains(mapResult.KeyType);
        mapResult.ValueIsImportType = !nativeTypes.Contains(mapResult.ValueType);
        return mapResult;
    }
    #endregion

    #region Message
    private static MessageResult Analyze_Message_DispatchWorker(Message body)
    {
        var messageResult = new MessageResult
        {
            MessageName = body.MessageName,
            CommonFields = new List<CommonResult>(),
            MapFields = new List<MapResult>(),
            OneofFields = new List<OneofResult>(),
            EnumFields = new List<EnumResult>()
        };

        if (body.MessageBody.Enums != null)
            foreach (var mb in body.MessageBody.Enums)
                messageResult.EnumFields.Add(Analyze_EnumWorker(mb));
        if (body.MessageBody.Fields != null)
            foreach (var mb in body.MessageBody.Fields)
                messageResult.CommonFields.Add(Analyze_CommonFieldWorker(mb));
        if (body.MessageBody.Oneofs != null)
            foreach (var mb in body.MessageBody.Oneofs)
                messageResult.OneofFields.Add(Analyze_OneofWorker(mb));
        if (body.MessageBody.Maps != null)
            foreach (var mb in body.MessageBody.Maps)
                messageResult.MapFields.Add(Analyze_MapWorker(mb));
        if (body.MessageBody.Messages != null)
            foreach (var mb in body.MessageBody.Messages)
                messageResult.MessageFields.Add(Analyze_Message_DispatchWorker(mb));

        return messageResult;
    }
    #endregion
    #endregion
}