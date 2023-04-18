using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace JasonPereira84.Helpers.Web.Tests
{
    namespace Extensions
    {
        using JasonPereira84.Helpers.Extensions;

        using Microsoft.Extensions.Primitives;

        [TestClass]
        public class Test_StringValues
        {
            [TestMethod]
            public void AsStrings()
            {
                //happy path
                {
                    Assert.IsTrue(
                        new[] { "hello", "world" }
                            .SequenceEqual(new StringValues(new[] { "hello", "world" }).AsStrings()));
                }
                
                //testing trim
                {
                    Assert.IsTrue(
                        new[] { "hello", "world" }
                            .SequenceEqual(new StringValues(new[] { "hello ", " world" }).AsStrings()));
                }
                
                //testing IsNotNullOrEmptyOrWhiteSpace
                {
                    //NULL
                    {
                        Assert.IsTrue(
                            new[] { "hello", "world" }
                                .SequenceEqual(new StringValues(new[] { "hello", "world", (string)null }).AsStrings()));
                    }

                    //EMPTY
                    {
                        Assert.IsTrue(
                            new[] { "hello", "world" }
                                .SequenceEqual(new StringValues(new[] { "hello", "world", String.Empty }).AsStrings()));
                    }

                    //WHITESPACE
                    {
                        Assert.IsTrue(
                            new[] { "hello", "world" }
                                .SequenceEqual(new StringValues(new[] { "hello", "world", " " }).AsStrings()));
                    }
                }
            }

            [TestMethod]
            public void AsString()
            {
                var stringValues = new StringValues(new[] { "1", "2", "3" });

                Assert.AreEqual(
                    expected: "1, 2, 3",
                    actual: stringValues.AsString(", "));

                Assert.AreEqual(
                    expected: "123",
                    actual: stringValues.AsString());

                Assert.AreEqual(
                    expected: "1,2,3",
                    actual: stringValues.AsString(','));
            }

        }
    }
}
