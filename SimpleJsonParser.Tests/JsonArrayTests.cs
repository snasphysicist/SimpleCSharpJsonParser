
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleJsonParser.Tests
{
    [TestClass]
    public class JsonArrayTests
    {
        [DataTestMethod]
        [DataRow("[]")]
        public void ShouldParseEmptyArraySucceed(
            string jsonFragment
        )
        {
            IJsonElement parser = new JsonArray();
            string jsonRemainder;
            Assert.IsTrue(
                parser.Parse(
                    jsonFragment,
                    out jsonRemainder
                )
            );
            IJsonElement[] array = parser.AsArray();
            Assert.IsNotNull(array);
            Assert.AreEqual(
                0,
                array.Length
            );
        }

        [DataTestMethod]
        [DataRow("[2,1.1,\"TestString\",null,true]", 5)]
        [DataRow(" [ \n2\r  ,\t1.1   ,  \t\"TestString\"\t\n  ,\t  null\t\n ,\r  true ]  ", 5)]
        public void ShouldParseWellFormattedArrayNoNestedElementsSucceed(
            string jsonFragment,
            int numberOfElements
        )
        {
            IJsonElement parser = new JsonArray();
            string jsonRemainder;
            Assert.IsTrue(
                parser.Parse(
                    jsonFragment,
                    out jsonRemainder
                )
            );
            IJsonElement[] array = parser.AsArray();
            Assert.IsNotNull(array);
            Assert.AreEqual(
                numberOfElements,
                array.Length
            );
        }

        [DataTestMethod]
        [DataRow("[[2, 1], [3, 2], {\"test\": 1, \"value\": true}]]", 3)]
        public void ShouldParseWellFormattedArrayWithNestedElementsSucceed(
            string jsonFragment,
            int numberOfElements
        )
        {
            IJsonElement parser = new JsonArray();
            string jsonRemainder;
            Assert.IsTrue(
                parser.Parse(
                    jsonFragment,
                    out jsonRemainder
                )
            );
            IJsonElement[] array = parser.AsArray();
            Assert.IsNotNull(array);
            Assert.AreEqual(
                numberOfElements,
                array.Length
            );
        }

        [DataTestMethod]
        [DataRow("[2,1.1,\"TestString\",null  true]")]
        [DataRow("[2,1.1,\"TestString\"null,true]")]
        [DataRow(" 2,1.1,\"TestString\",null,true]")]
        [DataRow("[2,1.1,\"TestString\",null,true ")]
        public void ShouldNotParseIncorrectArraySyntaxFail(
            string jsonFragment
        )
        {
            IJsonElement parser = new JsonArray();
            string jsonRemainder;
            Assert.IsFalse(
                parser.Parse(
                    jsonFragment,
                    out jsonRemainder
                )
            );
        }
    }
}