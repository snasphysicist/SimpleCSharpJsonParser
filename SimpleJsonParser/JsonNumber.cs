
using System;
using System.Collections.Generic;

namespace SimpleJsonParser
{
    public class JsonNumber : IJsonElement
    {
        private int valueInteger;
        private double valueDouble;

        private bool isInteger;
        private bool isDouble;

        public bool Success
        { get; private set; }

        public bool Parse(
            string jsonFragment,
            out string jsonRemainder
        ) {
            jsonRemainder = StringUtils.StripLeadingJsonWhitespace(
                jsonFragment
            );

            // Find character to denote end of value
            int i = 0;
            while(
                (i < jsonRemainder.Length)
                && (jsonRemainder.Substring(i, 1) != ",")
                && (jsonRemainder.Substring(i, 1) != "]")
                && (jsonRemainder.Substring(i, 1) != "}")
            ) {
                i++;
            }
            // If no closing character, badly formatted json
            if (i == jsonRemainder.Length)
            {
                Success = false;
                jsonRemainder = jsonFragment;
                return Success;
            }
            string numberString = jsonRemainder.Substring(
                0, i
            );
            try
            {
                valueInteger = Int32.Parse(
                    numberString
                );
                isInteger = true;
                isDouble = false;
                Success = true;
                jsonRemainder = jsonRemainder.Substring(i);
                return Success;
            } catch (Exception e1)
            {
                try
                {
                    valueDouble = Double.Parse(
                        numberString
                    );
                    isInteger = false;
                    isDouble = true;
                    Success = true;
                    jsonRemainder = jsonRemainder.Substring(i);
                    return Success;
                } catch(Exception e2)
                {
                    isInteger = false;
                    isDouble = false;
                    Success = false;
                    jsonRemainder = jsonFragment;
                    return Success;
                }
            }
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
            return isInteger;
        }

        public bool IsDouble()
        {
            return isDouble;
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
            return null;
        }

        public int? AsInteger() 
        {
            if (Success && isInteger)
            {
                return valueInteger;
            } else {
                return null;
            }
        }

        public double? AsDouble() 
        {
            if (Success && isDouble) 
            {
                return valueDouble;
            } else if (Success && isInteger)
            {
                return (double) valueInteger;
            } else
            {
                return null;
            }
        }

        public string AsString()
        {
            if (Success && isInteger)
            {
                return $"{valueInteger}";
            } else if (Success && isDouble)
            {
                return $"{valueDouble}";
            } else
            {
                return null;
            }
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