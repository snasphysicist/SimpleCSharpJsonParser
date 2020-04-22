
using System.Collections.Generic;

namespace SimpleJsonParser
{
    public class JsonBoolean : IJsonElement
    {
        private bool value;

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
                "true"
            ))
            {
                value = true;
                Success = true;
                // Remove value
                jsonRemainder = jsonRemainder.Substring(4);
                return Success;
            } else if (jsonRemainder.StartsWith(
                "false"
            )) {
                value = false;
                Success = true;
                jsonRemainder = jsonRemainder.Substring(5);
                return Success;
            } else {
                Success = false;
                jsonRemainder = jsonFragment;
                return Success;
            }
        }

        public bool IsBoolean()
        {
            return true;
        }

        public bool IsNull() 
        {
            return false;
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
            if (Success)
            {
                return value;
            } else 
            {
                return null;
            }
        }

        public object AsNull()
        {
            return null;
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