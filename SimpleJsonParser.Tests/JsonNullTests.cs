
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleJsonParser.Tests
{
    [TestClass]
    public class JsonNullTests
    {
        [DataTestMethod]
        [DataRow("null")]
        [DataRow("\n\r \t null")]
        public void ShouldParseNullLowercaseSucceed(
            string jsonFragment
        )
        {
            IJsonElement parser = new JsonNull();
            string jsonRemainder;
            Assert.IsTrue(
                parser.Parse(
                    jsonFragment,
                    out jsonRemainder
                )
            );
        }

        [DataTestMethod]
        [DataRow("Null")]
        [DataRow("NULL")]
        [DataRow("NuLl")]
        [DataRow("nUlL")]
        public void ShouldNotParseNullNotLowerCaseFail(
            string jsonFragment
        )
        {
            IJsonElement parser = new JsonBoolean();
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