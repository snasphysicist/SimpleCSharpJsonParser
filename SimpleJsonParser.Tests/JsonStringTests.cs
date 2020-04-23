

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleJsonParser.Tests
{
    [TestClass]
    public class JsonStringTests
    {
        [DataTestMethod]
        [DataRow("\"TestString\"", "TestString")]
        [DataRow("\n \t \r \"TestString\"", "TestString")]
        public void ShouldParseProperlyDelimitedUnescapedStringSucceed(
            string jsonFragment,
            string stringContent
        )
        {
            IJsonElement parsed = new JsonString();
            string jsonRemainder;
            Assert.IsTrue(
                parsed.Parse(
                    jsonFragment,
                    out jsonRemainder
                )
            );
            Assert.AreEqual(
                stringContent,
                parsed.AsString()
            );
        }

        [DataTestMethod]
        [DataRow("\"Test\\\\String\"", "Test\\String")]
        [DataRow("\"Test\\\\\\\"String\"", "Test\\\"String")]
        [DataRow("\"Test String\\\\\"", "Test String\\")]
        public void ShouldParseProperlyDelimitedWithEscapesStringSucceed(
            string jsonFragment,
            string stringContent
        )
        {
            IJsonElement parsed = new JsonString();
            string jsonRemainder;
            Assert.IsTrue(
                parsed.Parse(
                    jsonFragment,
                    out jsonRemainder
                )
            );
            Assert.AreEqual(
                stringContent,
                parsed.AsString()
            );
        }

        [DataTestMethod]
        [DataRow("\"TestString")]
        [DataRow("\n \t \r TestString\"")]
        public void ShouldNotParseImproperlyDelimitedUnescapedStringFail(
            string jsonFragment
        )
        {
            IJsonElement parsed = new JsonString();
            string jsonRemainder;
            Assert.IsFalse(
                parsed.Parse(
                    jsonFragment,
                    out jsonRemainder
                )
            );
        }

        [DataTestMethod]
        [DataRow("\"TestString\\\"")]
        [DataRow("\n \t \r \\\"TestString\"")]
        public void ShouldNotParseImproperlyDelimitedWithEscapesStringFail(
            string jsonFragment
        )
        {
            IJsonElement parsed = new JsonString();
            string jsonRemainder;
            Assert.IsFalse(
                parsed.Parse(
                    jsonFragment,
                    out jsonRemainder
                )
            );
        }
    }
}