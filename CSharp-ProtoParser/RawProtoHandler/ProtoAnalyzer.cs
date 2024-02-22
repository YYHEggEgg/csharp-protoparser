using System.ComponentModel;
using Newtonsoft.Json.Linq;
using YYHEggEgg.ProtoParser.RawData;

#pragma warning disable CS8600, CS8601, CS8602, CS8604, CS8620
namespace YYHEggEgg.ProtoParser.RawProtoHandler;
{
    public static class JsonAnalyzer
    {
        public static readonly string[] nativeTypes = new string[] { "double", "float", "int32", "int64", "uint32", "uint64", "sint32", "sint64", "fixed32", "fixed64", "sfixed32", "sfixed64", "bool", "string", "bytes" };
        public static ProtoJsonResult AnalyzeRawProto(Proto proto)
        {
            var result = new ProtoJsonResult();

            var jsonObj = JObject.Parse(json);
            var protoBodies = jsonObj["ProtoBody"].Children();

            // 筛选出所有具有 MessageName 字段的项
            foreach (var body in protoBodies.Where(b => b["MessageName"] != null))
            {
                var messageResult = Analyze_Message_DispatchWorker(body);
                if (messageResult != null) result.messageBodys.Add(messageResult);
            }

            // 筛选出所有具有 EnumName 字段的项并进行分析
            var enumBodies = protoBodies.Where(b => b["EnumName"] != null);

            foreach (var eb in enumBodies)
            {
                var enumResult = Analyze_EnumWorker(eb);
                if (enumResult != null) result.enumBodys.Add(enumResult);
            }

            return result;
        }

        #region Extracted methods
        #region Enum
        // Input part json example:
        /*
    {
      "EnumName": "AbilityInvokeArgument",
      "EnumBody": [
        {
          "Ident": "ABILITY_INVOKE_ARGUMENT_NONE",
          "Number": "0",
          "EnumValueOptions": null,
          "Comments": null,
          "InlineComment": null,
          "Meta": {
            "Pos": {
              "Filename": "",
              "Offset": 902,
              "Line": 22,
              "Column": 3
            },
            "LastPos": {
              "Filename": "",
              "Offset": 0,
              "Line": 0,
              "Column": 0
            }
          }
        }
      ]
    }
         */
        private static EnumResult? Analyze_EnumWorker(JToken? eb)
        {
            if (eb["EnumName"] != null)
            {
                var enumResult = new EnumResult();
                enumResult.enumName = (string)eb["EnumName"];
                var enumIdents = eb["EnumBody"].Children();
                foreach (var ei in enumIdents)
                {
                    if (ei["Ident"] != null)
                    {
                        enumResult.enumNodes.Add(((string)ei["Ident"], (int)ei["Number"]));
                    }
                    else if (ei["OptionName"] != null)
                    {
                        enumResult.enumOptions.Add(((string)ei["OptionName"], (string)ei["Constant"]));
                    }
                }
                return enumResult;
            }
            else return null;
        }
        #endregion
        #region Common Field
        // Input part json example:
        /*
        {
          "IsRepeated": false,
          "IsRequired": false,
          "IsOptional": false,
          "Type": "int32",
          "FieldName": "retcode",
          "FieldNumber": "1",
          "FieldOptions": null,
          "Comments": null,
          "InlineComment": null,
          "Meta": {
            "Pos": {
              "Filename": "",
              "Offset": 997,
              "Line": 26,
              "Column": 3
            },
            "LastPos": {
              "Filename": "",
              "Offset": 0,
              "Line": 0,
              "Column": 0
            }
          }
        }
         */
        private static CommonResult? Analyze_CommonFieldWorker(JToken? mb)
        {
            if (mb["FieldName"] != null)
            {
                var commonResult = new CommonResult();
                commonResult.fieldType = (string)mb["Type"];
                commonResult.fieldName = (string)mb["FieldName"];
                commonResult.fieldNumber = (int)mb["FieldNumber"];
                commonResult.IsRepeatedField = (bool)mb["IsRepeated"];

                commonResult.isImportType = !nativeTypes.Contains(commonResult.fieldType);
                return commonResult;
            }
            else return null;
        }
        #endregion
        #region Oneof Field
        // Input example:
        /*
        {
          "OneofFields": [
            {
              "Type": "ForceUpdateInfo",
              "FieldName": "force_update",
              "FieldNumber": "4",
              "FieldOptions": null,
              "Comments": null,
              "InlineComment": null,
              "Meta": {
                "Pos": {
                  "Filename": "",
                  "Offset": 1214,
                  "Line": 33,
                  "Column": 5
                },
                "LastPos": {
                  "Filename": "",
                  "Offset": 0,
                  "Line": 0,
                  "Column": 0
                }
              }
            },
            {
              "Type": "StopServerInfo",
              "FieldName": "stop_server",
              "FieldNumber": "5",
              "FieldOptions": null,
              "Comments": null,
              "InlineComment": null,
              "Meta": {
                "Pos": {
                  "Filename": "",
                  "Offset": 1252,
                  "Line": 34,
                  "Column": 5
                },
                "LastPos": {
                  "Filename": "",
                  "Offset": 0,
                  "Line": 0,
                  "Column": 0
                }
              }
            }
          ],
          "OneofName": "detail",
          "Options": null,
          "Comments": null,
          "InlineComment": null,
          "InlineCommentBehindLeftCurly": null,
          "Meta": {
            "Pos": {
              "Filename": "",
              "Offset": 1195,
              "Line": 32,
              "Column": 3
            },
            "LastPos": {
              "Filename": "",
              "Offset": 1286,
              "Line": 35,
              "Column": 3
            }
          }
        }
         */
        private static OneofResult? Analyze_OneofWorker(JToken? mb)
        {
            if (mb["OneofName"] != null)
            {
                var oneofResult = new OneofResult();
                oneofResult.oneofEntryName = (string)mb["OneofName"];
                oneofResult.oneofInnerFields = new List<CommonResult>();
                var oneofFields = mb["OneofFields"].Children();

                foreach (var ob in oneofFields)
                {
                    var innerCommonResult = Analyze_InOneof_CommonFieldWorker(ob);
                    if (innerCommonResult != null)
                        oneofResult.oneofInnerFields.Add(innerCommonResult);
                }
                return oneofResult;
            }
            else return null;
        }
        #region Common Field in Oneof
        // Input example:
        /*
        {
          "Type": "uint32",
          "FieldName": "mist_trial_avatar_id",
          "FieldNumber": "8",
          "FieldOptions": null,
          "Comments": null,
          "InlineComment": null,
          "Meta": {
            "Pos": {
              "Filename": "",
              "Offset": 401,
              "Line": 16,
              "Column": 13
            },
            "LastPos": {
              "Filename": "",
              "Offset": 0,
              "Line": 0,
              "Column": 0
            }
          }
        }
         */
        private static CommonResult? Analyze_InOneof_CommonFieldWorker(JToken? mb)
        {
            if (mb["FieldName"] != null)
            {
                var commonResult = new CommonResult();
                commonResult.fieldType = (string)mb["Type"];
                commonResult.fieldName = (string)mb["FieldName"];
                commonResult.fieldNumber = (int)mb["FieldNumber"];

                commonResult.isImportType = !nativeTypes.Contains(commonResult.fieldType);
                return commonResult;
            }
            else return null;
        }
        #endregion
        #endregion
        #region MapField
        // Input example:
        /*
        {
          "KeyType": "uint32",
          "Type": "BlockInfo",
          "MapName": "block_info_map",
          "FieldNumber": "571",
          "FieldOptions": null,
          "Comments": null,
          "InlineComment": null,
          "Meta": {
            "Pos": {
              "Filename": "",
              "Offset": 1168,
              "Line": 33,
              "Column": 3
            },
            "LastPos": {
              "Filename": "",
              "Offset": 0,
              "Line": 0,
              "Column": 0
            }
          }
        }
         */
        private static MapResult? Analyze_MapWorker(JToken? mb)
        {
            if (mb["MapName"] != null)
            {
                var mapResult = new MapResult();
                mapResult.keyType = (string)mb["KeyType"];
                mapResult.valueType = (string)mb["Type"];
                mapResult.fieldName = (string)mb["MapName"];
                mapResult.fieldNumber = (int)mb["FieldNumber"];

                mapResult.keyIsImportType = !nativeTypes.Contains(mapResult.keyType);
                mapResult.valueIsImportType = !nativeTypes.Contains(mapResult.valueType);
                return mapResult;
            }
            else return null;
        }
        #endregion

        #region Message
        private static MessageResult? Analyze_Message_DispatchWorker(JToken? body)
        {
            if (body["MessageName"] != null)
            {
                var messageResult = new MessageResult();
                messageResult.commonFields = new List<CommonResult>();
                messageResult.mapFields = new List<MapResult>();
                messageResult.oneofFields = new List<OneofResult>();
                messageResult.enumFields = new List<EnumResult>();

                var protoname = (string)body["MessageName"];
                messageResult.messageName = protoname;
                var messageBodies = body["MessageBody"].Children();

                // 对每个 MessageBody 进行分析
                foreach (var mb in messageBodies)
                {
                    var enumResult = Analyze_EnumWorker(mb);
                    if (enumResult != null) messageResult.enumFields.Add(enumResult);
                    var commonResult = Analyze_CommonFieldWorker(mb);
                    if (commonResult != null) messageResult.commonFields.Add(commonResult);
                    var oneofResult = Analyze_OneofWorker(mb);
                    if (oneofResult != null) messageResult.oneofFields.Add(oneofResult);
                    var mapResult = Analyze_MapWorker(mb);
                    if (mapResult != null) messageResult.mapFields.Add(mapResult);
                    var innerMessageResult = Analyze_Message_DispatchWorker(mb);
                    if (innerMessageResult != null) messageResult.messageFields.Add(innerMessageResult);
                }
                return messageResult;
            }
            else return null;
        }
        #endregion
        #endregion
    }
}

#pragma warning restore CS8600, CS8601, CS8602, CS8604, CS8620