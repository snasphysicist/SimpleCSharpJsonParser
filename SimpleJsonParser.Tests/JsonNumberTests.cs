
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleJsonParser.Tests
{
    [TestClass]
    public class JsonNumberTests
    {
        [DataTestMethod]
        [DataRow("3,", 3)]
        [DataRow(" \n \r  \t 2  \n \r  \t ,", 2)]
        public void ShouldParsePositiveIntegerSucceed(
            string jsonFragment,
            int expectedValue
        )
        {
            IJsonElement parser = new JsonNumber();
            string jsonRemainder;
            Assert.IsTrue(
                parser.Parse(
                    jsonFragment,
                    out jsonRemainder
                )
            );
            Assert.AreEqual(
                expectedValue,
                parser.AsInteger()
            );
        }

        [DataTestMethod]
        [DataRow("-3,", -3)]
        [DataRow("  \r  \t\n -173  \r  \t\n ,", -173)]
        public void ShouldParseNegativeIntegerSucceed(
            string jsonFragment,
            int expectedValue
        )
        {
            IJsonElement parser = new JsonNumber();
            string jsonRemainder;
            Assert.IsTrue(
                parser.Parse(
                    jsonFragment,
                    out jsonRemainder
                )
            );
            Assert.AreEqual(
                expectedValue,
                parser.AsInteger()
            );
        }

        [DataTestMethod]
        [DataRow("3.14159,", 3.14159)]
        [DataRow(" \n \r  \t 2.265937645  \n \r  \t ,", 2.265937645)]
        public void ShouldParsePositiveDoubleSucceed(
            string jsonFragment,
            double expectedValue
        )
        {
            IJsonElement parser = new JsonNumber();
            string jsonRemainder;
            Assert.IsTrue(
                parser.Parse(
                    jsonFragment,
                    out jsonRemainder
                )
            );
            Assert.AreEqual(
                expectedValue,
                parser.AsDouble()
            );
        }

        [DataTestMethod]
        [DataRow("-3.14159,", -3.14159)]
        [DataRow("  \r  \t\n -173.679649321  \r  \t\n ,", -173.679649321)]
        public void ShouldParseNegativeDoubleSucceed(
            string jsonFragment,
            double expectedValue
        )
        {
            IJsonElement parser = new JsonNumber();
            string jsonRemainder;
            Assert.IsTrue(
                parser.Parse(
                    jsonFragment,
                    out jsonRemainder
                )
            );
            Assert.AreEqual(
                expectedValue,
                parser.AsDouble()
            );
        }
    }
}