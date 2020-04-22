
using System.Collections.Generic;

namespace SimpleJsonParser
{
    public class JsonNull : IJsonElement
    {
        private object value;

        public bool Success
        { get; private set; }

        public bool Parse(
            string jsonFragment,
            out string jsonRemainder
        ) {
            jsonRemainder = StringUtils.StripLeadingJsonWhitespace(
                jsonFragment
            );
            if (jsonRemainder.StartsWith(
                "null"
            ))
            {
                value = null;
                Success = true;
                // Remove value
                jsonRemainder = jsonRemainder.Substring(4);
                return Success;
            } else {
                Success = false;
                jsonRemainder = jsonFragment;
                return Success;
            }
        }

        public bool IsBoolean()
        {
            return false;
        }

        public bool IsNull() 
        {
            return true;
        }

        public bool IsInteger()
        {
            return false;
        }

        public bool IsDouble()
        {
            return false;
        }

        public bool IsString()
        {
            return false;
        }

        public bool IsArray()
        {
            return false;
        }

        public bool IsObject()
        {
            return false;
        }

        public bool? AsBoolean()
        {
            return null;
        }

        public object AsNull()
        {
            return value;
        }

        public int? AsInteger() 
        {
            return null;
        }

        public double? AsDouble() 
        {
            return null;
        }

        public string AsString()
        {
            return null;
        }

        public IJsonElement[] AsArray()
        {
            return null;
        }

        public Dictionary<string, IJsonElement> AsObject()
        {
            return null;
        }

    }
}