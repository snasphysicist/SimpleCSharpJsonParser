
using System.Collections.Generic;

namespace SimpleJsonParser
{
    public class JsonArray : IJsonElement
    {
        private IJsonElement[] values;

        public bool Success
        { get; private set; }

        public bool Parse(
            string jsonFragment,
            out string jsonRemainder
        ) {
            jsonRemainder = StringUtils.StripLeadingJsonWhitespace(
                jsonFragment
            );
            // The first character must be [ for an array
            if (
                StringUtils.GetFirstCharacter(
                    jsonRemainder
                ) != "["
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
            LinkedList<IJsonElement> elements = new LinkedList<IJsonElement>();
            IJsonElement nextElement;
            while(
                (jsonRemainder.Length > 0)
                && (
                    StringUtils.GetFirstCharacter(
                        jsonRemainder
                    ) != "]"
                )
            ) {
                jsonRemainder = StringUtils.StripLeadingJsonWhitespace(
                    jsonRemainder
                );
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
                // Parsed successfully -> add to list of parsed elements
                elements.AddLast(
                    nextElement
                );
                int closingIndex = nextClosingCharacterIndex(
                    jsonRemainder
                );
                if (closingIndex == jsonRemainder.Length)
                {
                    // Could not find end of element, thus badly formatted json
                    Success = false;
                    jsonRemainder = jsonFragment;
                    return Success;
                }
                // Otherwise, move to after the closing element character
                jsonRemainder = jsonRemainder.Substring(
                    closingIndex + 1
                );
                // Remove leading whitespace
                jsonRemainder = StringUtils.StripLeadingJsonWhitespace(
                    jsonRemainder
                );
            }
            // Landing here should mean success
            Success = true;
            // Move linked list elements to values array
            values = new IJsonElement[elements.Count];
            elements.CopyTo(values, 0);
            return Success;
        }

        /*
         * Look for the next element closing character - , or ]
         * and return its index, or the string length if cannot be found
         */
        private int nextClosingCharacterIndex(
            string jsonFragment
        ) {
            int i = 0;
            while (
                (i < jsonFragment.Length)
                && (jsonFragment.Substring(i, 1) != ",")
                && (jsonFragment.Substring(i, 1) != "]")
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
            return true;
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
            return values;
        }

        public Dictionary<string, IJsonElement> AsObject()
        {
            return null;
        }

    }
}