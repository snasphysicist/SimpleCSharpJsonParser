
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleJsonParser.Tests
{
    [TestClass]
    public class JsonObjectTests
    {
        [DataTestMethod]
        [DataRow("{}")]
        public void ShouldParseEmptyObjectSucceed(
            string jsonFragment
        )
        {
            IJsonElement parser = new JsonObject();
            string jsonRemainder;
            Assert.IsTrue(
                parser.Parse(
                    jsonFragment,
                    out jsonRemainder
                )
            );
            Assert.IsNotNull(parser.AsObject());
        }

        [DataTestMethod]
        [DataRow("{\"one\":1,\"two\":2.1234,\"three\":\"3\",\"four\":null,\"five\":false}")]
        [DataRow("\r{\"one\"\t\t:\n\r  \t1,\"two\"\r   \n : \t\t2.1234,\"three\": \"3\"\t  } \n ")]
        public void ShouldParseObjectWithCorrectSyntaxNoNestedElementsSucceed(
            string jsonFragment
        )
        {
            IJsonElement parser = new JsonObject();
            string jsonRemainder;
            Assert.IsTrue(
                parser.Parse(
                    jsonFragment,
                    out jsonRemainder
                )
            );
            Assert.IsNotNull(parser.AsObject());
        }

        [DataTestMethod]
        [DataRow("{\"one\":[1,2.3],\"two\":{\"three\":null,\"four\":false}}")]
        public void ShouldParseObjectWithCorrectSyntaxWithNestedElementsSucceed(
            string jsonFragment
        )
        {
            IJsonElement parser = new JsonObject();
            string jsonRemainder;
            Assert.IsTrue(
                parser.Parse(
                    jsonFragment,
                    out jsonRemainder
                )
            );
            Assert.IsNotNull(parser.AsObject());
        }

        [DataTestMethod]
        [DataRow(" \"one\":1,\"two\":2.1234,\"three\":\"3\",\"four\":null,\"five\":false}")]
        [DataRow("{\"one\":1,\"two\" 2.1234,\"three\":\"3\",\"four\":null,\"five\":false}")]
        [DataRow("{\"one\":1,\"two\":2.1234,\"three\":\"3\",\"four  :null,\"five\":false}")]
        [DataRow("{\"one\":1,\"two\":2.1234,\"three\":\"3\",\"four\":null,\"five\":false ")]
        public void ShouldNotParseObjectWithIncorrectSyntaxFail(
            string jsonFragment
        )
        {
            IJsonElement parser = new JsonObject();
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