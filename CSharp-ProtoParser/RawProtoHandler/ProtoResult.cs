using System.Diagnostics.CodeAnalysis;

namespace YYHEggEgg.ProtoParser.RawProtoHandler
{
    public class CommonResult
    {
        public string fieldType { get; set; } = "";
        public string fieldName { get; set; } = "";
        public int fieldNumber { get; set; }
        public bool IsRepeatedField { get; set; } = false;
        public bool isImportType { get; set; } = false;

        public override string ToString()
        {
            return $"Field Type: {fieldType}, Field Name: {fieldName}, Number: {fieldNumber}, Is Repeated: {IsRepeatedField}, Is Import Type: {isImportType}";
        }
    
        // override object.Equals
        /// <summary>
        /// Compare whether the name of <paramref name="obj"/> equals to <see cref="fieldName"/>. <para/>
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
            
            return fieldName.Equals(((CommonResult)obj).fieldName);
        }
        
        // override object.GetHashCode
        /// <summary>
        /// Get the HashCode of <see cref="fieldName"/>. <para/>
        /// Only used by Enumerable methods. Don't use it in any code or a typeparam of <see cref="HashSet"/>!
        /// </summary>
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return fieldName.GetHashCode();
        }
    
        public static readonly IEqualityComparer<CommonResult> NameComparer = new CommonResultEqualityComparer();

        #region Comparer
        public class CommonResultEqualityComparer : EqualityComparer<CommonResult>
        {
            public override bool Equals(CommonResult? x, CommonResult? y)
            {
                return x?.fieldName?.Equals(y?.fieldName) ?? false;
            }

            public override int GetHashCode([DisallowNull] CommonResult obj)
            {
                return obj.fieldName.GetHashCode();
            }
        }
        #endregion
    }

    public class MapResult
    {
        public string keyType { get; set; } = "";
        public bool keyIsImportType { get; set; } = false;
        public string valueType { get; set; } = "";
        public bool valueIsImportType { get; set; } = false;
        public string fieldName { get; set; } = "";
        public int fieldNumber { get; set; }

        public override string ToString()
        {
            return $"Map Field Name: {fieldName}, Number: {fieldNumber}, Key Type: {keyType}, Is Import Type: {keyIsImportType}, Value Type: {valueType}, Is Import Type: {valueIsImportType}";
        }
    
        // override object.Equals
        /// <summary>
        /// Compare whether the name of <paramref name="obj"/> equals to <see cref="fieldName"/>. <para/>
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
            
            return fieldName.Equals(((MapResult)obj).fieldName);
        }
        
        // override object.GetHashCode
        /// <summary>
        /// Get the HashCode of <see cref="fieldName"/>. <para/>
        /// Only used by Enumerable methods. Don't use it in any code or a typeparam of <see cref="HashSet"/>!
        /// </summary>
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return fieldName.GetHashCode();
        }
    
        public static readonly IEqualityComparer<MapResult> NameComparer = new MapResultEqualityComparer();

        #region Comparer
        public class MapResultEqualityComparer : EqualityComparer<MapResult>
        {
            public override bool Equals(MapResult? x, MapResult? y)
            {
                return x?.fieldName?.Equals(y?.fieldName) ?? false;
            }

            public override int GetHashCode([DisallowNull] MapResult obj)
            {
                return obj.fieldName.GetHashCode();
            }
        }
        #endregion
    }

    public class OneofResult
    {
        public string oneofEntryName { get; set; } = "";
        public List<CommonResult> oneofInnerFields { get; set; } = new();

        public override string ToString()
        {
            string result = "";
            result += $"Oneof Field Name: {oneofEntryName}\nStart Oneof Fields:-----------\n";
            foreach (CommonResult common in oneofInnerFields)
            {
                result += $"{common}\n";
            }
            result += "End Oneof FIelds-------------------";
            return result;
        }

        public string ToString(bool single_line_output)
        {
            if (!single_line_output) return ToString();
            string result = "";
            result += $"Oneof Field Name: {oneofEntryName}; Oneof Fields: ";
            foreach (CommonResult common in oneofInnerFields)
            {
                result += $"[ {common}]; ";
            }
            return result;
        }
    
        // override object.Equals
        /// <summary>
        /// Compare whether the name of <paramref name="obj"/> equals to <see cref="messageName"/>. <para/>
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
            
            return oneofEntryName.Equals(((OneofResult)obj).oneofEntryName);
        }
        
        // override object.GetHashCode
        /// <summary>
        /// Get the HashCode of <see cref="oneofEntryName"/>. <para/>
        /// Only used by Enumerable methods. Don't use it in any code or a typeparam of <see cref="HashSet"/>!
        /// </summary>
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return oneofEntryName.GetHashCode();
        }
    
        public static readonly IEqualityComparer<OneofResult> NameComparer = new OneofResultEqualityComparer();

        #region Comparer
        public class OneofResultEqualityComparer : EqualityComparer<OneofResult>
        {
            public override bool Equals(OneofResult? x, OneofResult? y)
            {
                return x?.oneofEntryName?.Equals(y?.oneofEntryName) ?? false;
            }

            public override int GetHashCode([DisallowNull] OneofResult obj)
            {
                return obj.oneofEntryName.GetHashCode();
            }
        }
        #endregion
    }

    public class EnumResult
    {
        public string enumName { get; set; } = "";
        public List<(string name, int number)> enumNodes { get; set; } = new();
        public List<(string name, string constant)> enumOptions { get; set; } = new();

        public override string ToString()
        {
            string result = "";
            result += $"Enum: {enumName}";
            foreach (var tuple in enumOptions)
            {
                result += $"\nOption: {tuple.name} = {tuple.constant}";
            }
            foreach (var tuple in enumNodes)
            {
                result += $"\nField Name: {tuple.name} = {tuple.number}";
            }
            return result;
        }

        public string ToString(bool single_line_output)
        {
            if (!single_line_output) return ToString();
            string result = "";
            result += $"Enum: {enumName}; ";
            foreach (var tuple in enumOptions)
            {
                result += $"Option: {tuple.name} = {tuple.constant}; ";
            }
            foreach (var tuple in enumNodes)
            {
                result += $"Node: {tuple.name}; ";
            }
            return result;
        }
    
        // override object.Equals
        /// <summary>
        /// Compare whether the name of <paramref name="obj"/> equals to <see cref="enumName"/>. <para/>
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
            
            return enumName.Equals(((EnumResult)obj).enumName);
        }
        
        // override object.GetHashCode
        /// <summary>
        /// Get the HashCode of <see cref="enumName"/>. <para/>
        /// Only used by Enumerable methods. Don't use it in any code or a typeparam of <see cref="HashSet"/>!
        /// </summary>
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return enumName.GetHashCode();
        }
    
        public static readonly IEqualityComparer<EnumResult> NameComparer = new EnumResultEqualityComparer();

        #region Comparer
        public class EnumResultEqualityComparer : EqualityComparer<EnumResult>
        {
            public override bool Equals(EnumResult? x, EnumResult? y)
            {
                return x?.enumName?.Equals(y?.enumName) ?? false;
            }

            public override int GetHashCode([DisallowNull] EnumResult obj)
            {
                return obj.enumName.GetHashCode();
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
        public string messageName { get; set; } = "";
        public List<CommonResult> commonFields { get; set; } = new();
        public List<MapResult> mapFields { get; set; } = new();
        public List<OneofResult> oneofFields { get; set; } = new();
        public List<EnumResult> enumFields { get; set; } = new();
        public List<MessageResult> messageFields { get; set; } = new();

        /// <summary>
        /// Convert the instance into a readable string. Don't support single line output.
        /// </summary>
        public override string ToString()
        {
            string result = "";
            result += $"Message: {messageName}";
            foreach (CommonResult common in commonFields)
            {
                result += $"\n{common}";
            }
            foreach (MapResult map in mapFields)
            {
                result += $"\n{map}";
            }
            foreach (OneofResult oneof in oneofFields)
            {
                result += $"\n{oneof}";
            }
            foreach (EnumResult _enum in enumFields)
            {
                result += $"\nInMessage {_enum}";
            }
            foreach (MessageResult innermsg in messageFields)
            {
                result += $"\nInMessage {innermsg}\n---Inner message end---";
            }
            return result;
        }
    
        // override object.Equals
        /// <summary>
        /// Compare whether the name of <paramref name="obj"/> equals to <see cref="messageName"/>. <para/>
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
            
            return messageName.Equals(((MessageResult)obj).messageName);
        }
        
        // override object.GetHashCode
        /// <summary>
        /// Get the HashCode of <see cref="messageName"/>. <para/>
        /// Only used by Enumerable methods. Don't use it in any code or a typeparam of <see cref="HashSet"/>!
        /// </summary>
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return messageName.GetHashCode();
        }

        public static readonly IEqualityComparer<MessageResult> NameComparer = new MessageResultEqualityComparer();

        #region Comparer
        public class MessageResultEqualityComparer : EqualityComparer<MessageResult>
        {
            public override bool Equals(MessageResult? x, MessageResult? y)
            {
                return x?.messageName?.Equals(y?.messageName) ?? false;
            }

            public override int GetHashCode([DisallowNull] MessageResult obj)
            {
                return obj.messageName.GetHashCode();
            }
        }
        #endregion
    }

    public class ProtoJsonResult
    {
        public List<MessageResult> messageBodys { get; set; } = new();
        public List<EnumResult> enumBodys { get; set; } = new();

        public override string ToString()
        {
            string result = "";
            foreach (MessageResult message in messageBodys)
            {
                result += $"{message}\n";
            }
            foreach (EnumResult _enum in enumBodys)
            {
                result += $"{_enum}\n";
            }
            return result;
        }
    }
}