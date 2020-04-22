
using System.Collections.Generic;

namespace SimpleJsonParser
{
    public interface IJsonElement
    {
        bool Success
        { get; }
        
        bool Parse(
            string jsonFragment,
            out string jsonRemainder
        );

        bool IsBoolean();

        bool IsNull();

        bool IsInteger();

        bool IsDouble();

        bool IsString();

        bool IsArray();

        bool IsObject();

        bool? AsBoolean();

        object AsNull();

        int? AsInteger();

        double? AsDouble();

        string AsString();

        IJsonElement[] AsArray();

        Dictionary<string, IJsonElement> AsObject();
    }
}