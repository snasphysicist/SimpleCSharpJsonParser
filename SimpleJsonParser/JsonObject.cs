
using System.Collections.Generic;

namespace SimpleJsonParser
{
    public class JsonObject : IJsonElement
    {
        private Dictionary<string, IJsonElement> values;

        public bool Success
        { get; private set; }

        public bool Parse(
            string jsonFragment,
            out string jsonRemainder
        ) {
            jsonRemainder = StringUtils.StripLeadingJsonWhitespace(
                jsonFragment
            );
            // The first character must be { for an array
            if (
                StringUtils.GetFirstCharacter(
                    jsonRemainder
                ) != "{"
            )
            {
                Success = false;
                jsonRemainder = jsonFragment;
                return Success;
            }
            // Remove the bracket character
            jsonRemainder = StringUtils.StripFirstCharacter(
                jsonRemainder
            );
            /* 
             * Now should have a series of json elements follow by commas
             * except for the last json element which is followed by ]
             *
             * Conditions
             * string still has some characters left in it
             * we haven't reached the array closing bracket
             */
            values = new Dictionary<string, IJsonElement>();
            IJsonElement nextKey;
            IJsonElement nextElement;
            while(
                (jsonRemainder.Length > 0)
                && (
                    StringUtils.GetFirstCharacter(
                        jsonRemainder
                    ) != "}"
                )
            ) {
                jsonRemainder = StringUtils.StripLeadingJsonWhitespace(
                    jsonRemainder
                );
                // First try to read in the next key
                nextKey = SimpleJsonParser.ParseOne(
                    jsonRemainder,
                    out jsonRemainder
                );
                // This must be a string, or invalid json
                if (
                    (nextKey == null)
                    || !nextKey.IsString()
                )
                {
                    Success = false;
                    jsonRemainder = jsonFragment;
                    return Success;
                }
                // Should be whitespace : whitespace between key & value
                jsonRemainder = StringUtils.StripLeadingJsonWhitespace(
                    jsonRemainder
                );
                // If no colon, invalid json, fail
                if (
                    StringUtils.GetFirstCharacter(
                        jsonRemainder
                    ) != ":"
                ) {
                    Success = false;
                    jsonRemainder = jsonFragment;
                    return Success;
                }
                // Step over colon & whitespace after colon
                jsonRemainder = StringUtils.StripFirstCharacter(
                    jsonRemainder
                );
                jsonRemainder = StringUtils.StripLeadingJsonWhitespace(
                    jsonRemainder
                );
                // Try to extract element
                nextElement = SimpleJsonParser.ParseOne(
                    jsonRemainder,
                    out jsonRemainder
                );
                // A returned null means that the json was not parseable
                if (nextElement == null)
                {
                    Success = false;
                    jsonRemainder = jsonFragment;
                    return Success;
                }
                // Parsed successfully -> add to dictionary of parsed elements
                values[
                    nextKey.AsString()
                ] = nextElement;
                // Remove leading whitespace
                jsonRemainder = StringUtils.StripLeadingJsonWhitespace(
                    jsonRemainder
                );
                // Next character should be either comma or bracket }
                if (
                    (jsonRemainder.Length > 0)
                    && (
                        StringUtils.GetFirstCharacter(
                            jsonRemainder
                        ) == ","
                    )
                ) {
                    // If it's a comma, remove it
                    jsonRemainder = StringUtils.StripFirstCharacter(
                        jsonRemainder
                    );
                } else if (
                    (jsonRemainder.Length > 0)
                    && (
                        StringUtils.GetFirstCharacter(
                            jsonRemainder
                        ) == "}"
                    )
                ) {
                    /* 
                     * For a closing bracket do nothing
                     * It will be caught on next loop condition check
                     */
                } else {
                    // If it's anything else, bad formatting
                    Success = false;
                    jsonRemainder = jsonFragment;
                    return Success;
                }
            }
            // Landing here should mean success
            Success = true;
            // Remember to remove closing bracket
            jsonRemainder = StringUtils.StripFirstCharacter(
                jsonRemainder
            );
            return Success;
        }

        /*
         * Look for the next element closing character - , or }
         * and return its index, or the string length if cannot be found
         */
        private int nextClosingCharacterIndex(
            string jsonFragment
        ) {
            int i = 0;
            while (
                (i < jsonFragment.Length)
                && (jsonFragment.Substring(i, 1) != ",")
                && (jsonFragment.Substring(i, 1) != "}")
            ) {
                i++;
            }
            return i;
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
            return false;
        }

        public bool IsArray()
        {
            return false;
        }

        public bool IsObject()
        {
            return true;
        }

        public bool? AsBoolean()
        {
            return null;
        }

        public object AsNull()
        {
            return false;
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
            return values;
        }

    }
}