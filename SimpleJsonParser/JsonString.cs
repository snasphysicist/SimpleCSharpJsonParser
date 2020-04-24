
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
             * Conditions to continue loop
             * Haven't reached the end of the string
             * ^ checked in while loop condition
             * also
             *   we haven't reached a quotation mark
             *   or 
             *   we have reached a quotation mark and it is escaped
             *   ^ these two checked in while loop body, use break statement
             */
            while (i < jsonRemainder.Length)
            {
                if (jsonRemainder.Substring(i, 1) == "\\")
                {
                    // When there is a slash, flip whether we are escaped
                    isEscaped = !isEscaped;
                } else {
                    // If unescaped " , done, exit loop
                    if(
                        !isEscaped
                        && jsonRemainder.Substring(i, 1) == "\""
                    ) {
                        break;
                    }
                    // If not, reset escaped count
                    isEscaped = false;
                }
                i++;
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
            // Check all escape sequences, replace if valid
            if (
                !ReplaceEscaped(
                    value,
                    out value
                ) 
            ) {
                // If any escape sequences were invalid
                Success = false;
                jsonRemainder = jsonFragment;
                return Success;
            }
            // Return the rest of the json string after the closing "
            jsonRemainder = jsonRemainder.Substring(i + 1);
            Success = true;
            return Success;
        }

        private bool ReplaceEscaped(
            string jsonFragment,
            out string jsonRemainder
        ) {
            jsonRemainder = jsonFragment;
            int i = 0;
            // Go through the entire string
            while (i < jsonRemainder.Length)
            {
                // If we find a slash
                if (
                    jsonRemainder.Substring(i, 1) == "\\"
                ) {
                    // There should be another character after this
                    if ((i + 1) > jsonRemainder.Length)
                    {
                        // If not, then string is not valid
                        jsonRemainder = jsonFragment;
                        return false;
                    }
                    // Replace this and next character if permissible escape sequence
                    char escapeSequence = jsonRemainder.ToCharArray()[i + 1];
                    string replaceCharacter = "";
                    switch (escapeSequence)
                    {
                        case 'b':
                        {
                            replaceCharacter = "\b";
                            break;
                        }
                        case 'f':
                        {
                            replaceCharacter = "\f";
                            break;
                        }
                        case 'n':
                        {
                            replaceCharacter = "\n";
                            break;
                        }
                        case 'r':
                        {
                            replaceCharacter = "\r";
                            break;
                        }
                        case 't':
                        {
                            replaceCharacter = "\t";
                            break;
                        }
                        case '\\':
                        {
                            replaceCharacter = "\\";
                            break;
                        }
                        case '"':
                        {
                            replaceCharacter = "\"";
                            break;
                        }
                    }
                    // If not one of the approved escape characters, fail
                    if (replaceCharacter == "")
                    {
                        jsonRemainder = jsonFragment;
                        return false;
                    }
                    jsonRemainder = jsonRemainder.Substring(0, i)
                        + replaceCharacter
                        + jsonRemainder.Substring(i + 2);
                    
                }
                i++;
            }
            // If we reached here, all of the escape characters were valid & replaced
            return true;
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