
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleJsonParser;

namespace SimpleJsonParser.Tests
{
    [TestClass]
    public class JsonBooleanTests
    {
        [DataTestMethod]
        [DataRow("true")]
        [DataRow("\n\r \t true")]
        public void ShouldParseTrueLowercaseSucceed(
            string jsonFragment
        )
        {
            IJsonElement parser = new JsonBoolean();
            string jsonRemainder;
            Assert.IsTrue(
                parser.Parse(
                    jsonFragment,
                    out jsonRemainder
                )
            );
        }

        [DataTestMethod]
        [DataRow("True")]
        [DataRow("TRUE")]
        [DataRow("TrUe")]
        [DataRow("tRuE")]
        public void ShouldNotParseTrueNotLowerCaseFail(
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

        [DataTestMethod]
        [DataRow("false")]
        [DataRow("\n\r \t false")]
        public void ShouldParseFalseLowercaseSucceed(
            string jsonFragment
        )
        {
            IJsonElement parser = new JsonBoolean();
            string jsonRemainder;
            Assert.IsTrue(
                parser.Parse(
                    jsonFragment,
                    out jsonRemainder
                )
            );
        }

        [DataTestMethod]
        [DataRow("False")]
        [DataRow("FALSE")]
        [DataRow("FaLsE")]
        [DataRow("fAlSe")]
        public void ShouldNotParseFalseNotLowerCaseFail(
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