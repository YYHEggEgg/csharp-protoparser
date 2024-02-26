using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace YYHEggEgg.ProtoParser.RawProtoHandler;

public class CommonResult
{
#if NET7_0_OR_GREATER
    public required string FieldType { get; set; }
#else
    public string FieldType { get; set; } = null!;
#endif
#if NET7_0_OR_GREATER
    public required string FieldName { get; set; }
#else
    public string FieldName { get; set; } = null!;
#endif
    public int FieldNumber { get; set; }
    public bool IsRepeatedField { get; set; } = false;
    public bool IsImportType { get; set; } = false;

    public override string ToString()
    {
        return $"Field Type: {FieldType}, Field Name: {FieldName}, Number: {FieldNumber}, Is Repeated: {IsRepeatedField}, Is Import Type: {IsImportType}";
    }

    // override object.Equals
    /// <summary>
    /// Compare whether the name of <paramref name="obj"/> equals to <see cref="FieldName"/>. <para/>
    /// Only used by Enumerable methods. Don't use it in any code or a typeparam of <see cref="HashSet<T>"/>!
    /// </summary>
    public override bool Equals(object? obj)
    {
        //
        // See the full list of guidelines at
        //   http://go.microsoft.com/fwlink/?LinkID=85237
        // and also the guidance for operator== at
        //   http://go.microsoft.com/fwlink/?LinkId=85238
        //

        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return FieldName.Equals(((CommonResult)obj).FieldName);
    }

    // override object.GetHashCode
    /// <summary>
    /// Get the HashCode of <see cref="FieldName"/>. <para/>
    /// Only used by Enumerable methods. Don't use it in any code or a typeparam of <see cref="HashSet"/>!
    /// </summary>
    public override int GetHashCode()
    {
        // TODO: write your implementation of GetHashCode() here
        return FieldName.GetHashCode();
    }

    public static readonly IEqualityComparer<CommonResult> NameComparer = new CommonResultEqualityComparer();

    #region Comparer
    public class CommonResultEqualityComparer : EqualityComparer<CommonResult>
    {
        public override bool Equals(CommonResult? x, CommonResult? y)
        {
            return x?.FieldName?.Equals(y?.FieldName) ?? false;
        }

        public override int GetHashCode([DisallowNull] CommonResult obj)
        {
            return obj.FieldName.GetHashCode();
        }
    }
    #endregion
}

public class MapResult
{
#if NET7_0_OR_GREATER
    public required string KeyType { get; set; }
#else
    public string KeyType { get; set; } = null!;
#endif
    public bool KeyIsImportType { get; set; } = false;
#if NET7_0_OR_GREATER
    public required string ValueType { get; set; }
#else
    public string ValueType { get; set; } = null!;
#endif
    public bool ValueIsImportType { get; set; } = false;
#if NET7_0_OR_GREATER
    public required string FieldName { get; set; }
#else
    public string FieldName { get; set; } = null!;
#endif
    public int FieldNumber { get; set; }

    public override string ToString()
    {
        return $"Map Field Name: {FieldName}, Number: {FieldNumber}, Key Type: {KeyType}, Is Import Type: {KeyIsImportType}, Value Type: {ValueType}, Is Import Type: {ValueIsImportType}";
    }

    // override object.Equals
    /// <summary>
    /// Compare whether the name of <paramref name="obj"/> equals to <see cref="FieldName"/>. <para/>
    /// Only used by Enumerable methods. Don't use it in any code or a typeparam of <see cref="HashSet<T>"/>!
    /// </summary>
    public override bool Equals(object? obj)
    {
        //
        // See the full list of guidelines at
        //   http://go.microsoft.com/fwlink/?LinkID=85237
        // and also the guidance for operator== at
        //   http://go.microsoft.com/fwlink/?LinkId=85238
        //

        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return FieldName.Equals(((MapResult)obj).FieldName);
    }

    // override object.GetHashCode
    /// <summary>
    /// Get the HashCode of <see cref="FieldName"/>. <para/>
    /// Only used by Enumerable methods. Don't use it in any code or a typeparam of <see cref="HashSet"/>!
    /// </summary>
    public override int GetHashCode()
    {
        // TODO: write your implementation of GetHashCode() here
        return FieldName.GetHashCode();
    }

    public static readonly IEqualityComparer<MapResult> NameComparer = new MapResultEqualityComparer();

    #region Comparer
    public class MapResultEqualityComparer : EqualityComparer<MapResult>
    {
        public override bool Equals(MapResult? x, MapResult? y)
        {
            return x?.FieldName?.Equals(y?.FieldName) ?? false;
        }

        public override int GetHashCode([DisallowNull] MapResult obj)
        {
            return obj.FieldName.GetHashCode();
        }
    }
    #endregion
}

public class OneofResult
{
#if NET7_0_OR_GREATER
    public required string OneofEntryName { get; set; }
#else
    public string OneofEntryName { get; set; } = null!;
#endif
    public List<CommonResult> OneofInnerFields { get; set; } = new();

    public override string ToString()
    {
        string result = "";
        result += $"Oneof Field Name: {OneofEntryName}\nStart Oneof Fields:-----------\n";
        foreach (CommonResult Common in OneofInnerFields)
        {
            result += $"{Common}\n";
        }
        result += "End Oneof FIelds-------------------";
        return result;
    }

    public string ToString(bool single_line_output)
    {
        if (!single_line_output) return ToString();
        string result = "";
        result += $"Oneof Field Name: {OneofEntryName}; Oneof Fields: ";
        foreach (CommonResult Common in OneofInnerFields)
        {
            result += $"[ {Common}]; ";
        }
        return result;
    }

    // override object.Equals
    /// <summary>
    /// Compare whether the name of <paramref name="obj"/> equals to <see cref="MessageName"/>. <para/>
    /// Only used by Enumerable methods. Don't use it in any code or a typeparam of <see cref="HashSet<T>"/>!
    /// </summary>
    public override bool Equals(object? obj)
    {
        //
        // See the full list of guidelines at
        //   http://go.microsoft.com/fwlink/?LinkID=85237
        // and also the guidance for operator== at
        //   http://go.microsoft.com/fwlink/?LinkId=85238
        //

        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return OneofEntryName.Equals(((OneofResult)obj).OneofEntryName);
    }

    // override object.GetHashCode
    /// <summary>
    /// Get the HashCode of <see cref="OneofEntryName"/>. <para/>
    /// Only used by Enumerable methods. Don't use it in any code or a typeparam of <see cref="HashSet"/>!
    /// </summary>
    public override int GetHashCode()
    {
        // TODO: write your implementation of GetHashCode() here
        return OneofEntryName.GetHashCode();
    }

    public static readonly IEqualityComparer<OneofResult> NameComparer = new OneofResultEqualityComparer();

    #region Comparer
    public class OneofResultEqualityComparer : EqualityComparer<OneofResult>
    {
        public override bool Equals(OneofResult? x, OneofResult? y)
        {
            return x?.OneofEntryName?.Equals(y?.OneofEntryName) ?? false;
        }

        public override int GetHashCode([DisallowNull] OneofResult obj)
        {
            return obj.OneofEntryName.GetHashCode();
        }
    }
    #endregion
}

public class EnumResult
{
#if NET7_0_OR_GREATER
    public required string EnumName { get; set; }
#else
    public string EnumName { get; set; } = null!;
#endif
    public List<(string name, int number)> EnumNodes { get; set; } = new();
    public List<(string name, string constant)> EnumOptions { get; set; } = new();

    public override string ToString()
    {
        string result = "";
        result += $"Enum: {EnumName}";
        foreach (var tuple in EnumOptions)
        {
            result += $"\nOption: {tuple.name} = {tuple.constant}";
        }
        foreach (var tuple in EnumNodes)
        {
            result += $"\nField Name: {tuple.name} = {tuple.number}";
        }
        return result;
    }

    public string ToString(bool single_line_output)
    {
        if (!single_line_output) return ToString();
        string result = "";
        result += $"Enum: {EnumName}; ";
        foreach (var tuple in EnumOptions)
        {
            result += $"Option: {tuple.name} = {tuple.constant}; ";
        }
        foreach (var tuple in EnumNodes)
        {
            result += $"Node: {tuple.name}; ";
        }
        return result;
    }

    // override object.Equals
    /// <summary>
    /// Compare whether the name of <paramref name="obj"/> equals to <see cref="EnumName"/>. <para/>
    /// Only used by Enumerable methods. Don't use it in any code or a typeparam of <see cref="HashSet<T>"/>!
    /// </summary>
    public override bool Equals(object? obj)
    {
        //
        // See the full list of guidelines at
        //   http://go.microsoft.com/fwlink/?LinkID=85237
        // and also the guidance for operator== at
        //   http://go.microsoft.com/fwlink/?LinkId=85238
        //

        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return EnumName.Equals(((EnumResult)obj).EnumName);
    }

    // override object.GetHashCode
    /// <summary>
    /// Get the HashCode of <see cref="EnumName"/>. <para/>
    /// Only used by Enumerable methods. Don't use it in any code or a typeparam of <see cref="HashSet"/>!
    /// </summary>
    public override int GetHashCode()
    {
        // TODO: write your implementation of GetHashCode() here
        return EnumName.GetHashCode();
    }

    public static readonly IEqualityComparer<EnumResult> NameComparer = new EnumResultEqualityComparer();

    #region Comparer
    public class EnumResultEqualityComparer : EqualityComparer<EnumResult>
    {
        public override bool Equals(EnumResult? x, EnumResult? y)
        {
            return x?.EnumName?.Equals(y?.EnumName) ?? false;
        }

        public override int GetHashCode([DisallowNull] EnumResult obj)
        {
            return obj.EnumName.GetHashCode();
        }
    }

    public class EnumNodeNumberEqualityComparer : EqualityComparer<(string name, int number)>
    {
        public override bool Equals((string name, int number) x, (string name, int number) y)
        {
            return x.number.Equals(y.number);
        }

        public override int GetHashCode([DisallowNull] (string name, int number) obj)
        {
            return obj.number;
        }
    }
    #endregion
}

public class MessageResult
{
#if NET7_0_OR_GREATER
    public required string MessageName { get; set; }
#else
    public string MessageName { get; set; } = null!;
#endif
    public List<CommonResult> CommonFields { get; set; } = new();
    public List<MapResult> MapFields { get; set; } = new();
    public List<OneofResult> OneofFields { get; set; } = new();
    public List<EnumResult> EnumFields { get; set; } = new();
    public List<MessageResult> MessageFields { get; set; } = new();

    /// <summary>
    /// Convert the instance into a readable string. Don't support single line output.
    /// </summary>
    public override string ToString()
    {
        string result = "";
        result += $"Message: {MessageName}";
        foreach (CommonResult Common in CommonFields)
        {
            result += $"\n{Common}";
        }
        foreach (MapResult Map in MapFields)
        {
            result += $"\n{Map}";
        }
        foreach (OneofResult Oneof in OneofFields)
        {
            result += $"\n{Oneof}";
        }
        foreach (EnumResult _Enum in EnumFields)
        {
            result += $"\nInMessage {_Enum}";
        }
        foreach (MessageResult innermsg in MessageFields)
        {
            result += $"\nInMessage {innermsg}\n---Inner Message end---";
        }
        return result;
    }

    // override object.Equals
    /// <summary>
    /// Compare whether the name of <paramref name="obj"/> equals to <see cref="MessageName"/>. <para/>
    /// Only used by Enumerable methods. Don't use it in any code or a typeparam of <see cref="HashSet<T>"/>!
    /// </summary>
    public override bool Equals(object? obj)
    {
        //
        // See the full list of guidelines at
        //   http://go.microsoft.com/fwlink/?LinkID=85237
        // and also the guidance for operator== at
        //   http://go.microsoft.com/fwlink/?LinkId=85238
        //

        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return MessageName.Equals(((MessageResult)obj).MessageName);
    }

    // override object.GetHashCode
    /// <summary>
    /// Get the HashCode of <see cref="MessageName"/>. <para/>
    /// Only used by Enumerable methods. Don't use it in any code or a typeparam of <see cref="HashSet"/>!
    /// </summary>
    public override int GetHashCode()
    {
        // TODO: write your implementation of GetHashCode() here
        return MessageName.GetHashCode();
    }

    public static readonly IEqualityComparer<MessageResult> NameComparer = new MessageResultEqualityComparer();

    #region Comparer
    public class MessageResultEqualityComparer : EqualityComparer<MessageResult>
    {
        public override bool Equals(MessageResult? x, MessageResult? y)
        {
            return x?.MessageName?.Equals(y?.MessageName) ?? false;
        }

        public override int GetHashCode([DisallowNull] MessageResult obj)
        {
            return obj.MessageName.GetHashCode();
        }
    }
    #endregion
}

public class ProtoJsonResult
{
    public List<MessageResult> MessageBodys { get; set; } = new();
    public List<EnumResult> EnumBodys { get; set; } = new();

    public override string ToString()
    {
        string result = "";
        foreach (MessageResult Message in MessageBodys)
        {
            result += $"{Message}\n";
        }
        foreach (EnumResult _Enum in EnumBodys)
        {
            result += $"{_Enum}\n";
        }
        return result;
    }
}
