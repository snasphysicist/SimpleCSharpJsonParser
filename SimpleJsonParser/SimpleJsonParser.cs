using System;

namespace SimpleJsonParser
{
    class SimpleJsonParser
    {

        public bool Success
        { get; private set; }

        public IJsonElement Parsed
        { get; private set; }

        public SimpleJsonParser(
            string fullJsonString
        ) {
            string jsonRemaining;
            Parsed = ParseOne(
                fullJsonString,
                out jsonRemaining
            );
            // For valid json, top level must be a json object
            if (
                (Parsed != null)
                && Parsed.IsObject()
            ) {
                Success = true;
            }
            // If conditions not met, invalid
            Success = false;
        }

        public static IJsonElement ParseOne(
            string jsonFragment,
            out string jsonRemaining
        ) {
            // Firstly, remove leading whitespace
            jsonRemaining = StringUtils.StripLeadingJsonWhitespace(
                jsonFragment
            );
            // Try to parse as all types until one is successful
            IJsonElement parsedElement;
            parsedElement = new JsonBoolean();
            if (parsedElement.Parse(
                jsonRemaining,
                out jsonRemaining
            )) {
                return parsedElement;
            }
            parsedElement = new JsonNull();
            if (parsedElement.Parse(
                jsonRemaining,
                out jsonRemaining
            )) {
                return parsedElement;
            }
            parsedElement = new JsonNumber();
            if (parsedElement.Parse(
                jsonRemaining,
                out jsonRemaining
            )) {
                return parsedElement;
            }
            parsedElement = new JsonString();
            if (parsedElement.Parse(
                jsonRemaining,
                out jsonRemaining
            )) {
                return parsedElement;
            }
            parsedElement = new JsonArray();
            if (parsedElement.Parse(
                jsonRemaining,
                out jsonRemaining
            )) {
                return parsedElement;
            }
            parsedElement = new JsonObject();
            if (parsedElement.Parse(
                jsonRemaining,
                out jsonRemaining
            )) {
                return parsedElement;
            }
            return null;
        }
    }
}
