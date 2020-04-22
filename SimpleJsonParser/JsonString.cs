
using System.Collections.Generic;

namespace SimpleJsonParser
{
    public class JsonString : IJsonElement
    {
        private string value;

        public bool Success
        { get; private set; }

        public bool Parse(
            string jsonFragment,
            out string jsonRemainder
        ) {
            jsonRemainder = StringUtils.StripLeadingJsonWhitespace(
                jsonFragment
            );
            // First character now should be "
            if (
                !(
                    StringUtils.GetFirstCharacter(
                        jsonRemainder
                    ) 
                    == "\""
                )
            ) {
                // If it is not, then fail (bad formatting)
                Success = false;
                jsonRemainder = jsonFragment;
                return Success;
            }
            // Remove "
            jsonRemainder = StringUtils.StripFirstCharacter(
                jsonRemainder
            );
            /* 
             * Find closing quotation mark
             * Note that it can be escaped with a \
             * so we need to check for this also
             */
            bool isEscaped = false;
            int i = 0;
            /*
             * Conditions
             * Haven't reached the end of the string
             * also
             *   we haven't reached a quotation mark
             *   or 
             *   we have reached a quotation mark and it is escaped
             */
            while(
                (i < jsonRemainder.Length)
                && (
                    (jsonRemainder.Substring(i, 1) != "\"")
                    || (
                        jsonRemainder.Substring(i, 1) == "\""
                        && isEscaped
                    )
                )
            ) {
                i++;
                if (jsonRemainder.Substring(i, 1) == "\\")
                {
                    // When there is a slash, flip whether we are escaped
                    isEscaped = !isEscaped;
                } else {
                    isEscaped = false;
                }
            }
            // If we couldn't find the closing quote
            if (i == jsonRemainder.Length)
            {
                Success = false;
                jsonRemainder = jsonFragment;
                return Success;
            }
            // Extract the string up to just before the "
            value = jsonRemainder.Substring(0, i);
            // Return the rest of the json string after the closing "
            jsonRemainder = jsonRemainder.Substring(i + 1);
            Success = true;
            return Success;
        }

        public bool IsBoolean()
        {
            return false;
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
            return true;
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
            return value;
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